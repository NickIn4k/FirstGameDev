using System;
using System.Collections.Generic;
using AIScripts.Friendly.GOAP.Behaviours;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Settings.CharacterSelection.Moves
{
    public class Move3 : MonoBehaviour
    {
        // Pointer gameObject reference
        GameObject point;
        Transform cameraTransform;
        
        Inputs inputs;
        InputAction confirmLocation;

        public DependencyInjector injector;
        public event Action OnUpdate;

        private List<Material> originalMaterials;
        public GeneralData generalData;

        private Collider hitCollider;

        private void Start()
        {
            originalMaterials = new();
        }

        void OnEnable()
        {
            inputs = new Inputs();
            confirmLocation = inputs.Gameplay.ClickToMove;
            confirmLocation.Enable();
            confirmLocation.performed += NotifyMoveTo;
            
            point = Object.Instantiate(GeneralMethods.GetGameObjectByName("Point"));
            cameraTransform = GeneralMethods.GetCamera().GetComponentsInChildren<Transform>()[1];
            GeneralMethods.GetPlayer().GetComponent<PlayerDitherSettings>().SetDither(true);
            GeneralMethods.GetRotator().GetComponent<Rotator>().SetInvert(true);
        }

        private void NotifyMoveTo(InputAction.CallbackContext obj)
        {
            OnUpdate?.Invoke(); // Invoke
            UpdateInjector(injector);
            
            // Log the world position or do something with it
            Debug.Log("Mouse World Position: " + point.transform.position);

            Destroy(point);
            GeneralMethods.GetPlayer().GetComponent<PlayerDitherSettings>().SetDither(false);
            GeneralMethods.GetRotator().GetComponent<Rotator>().SetInvert(false);
            confirmLocation.Disable();
            inputs.Disable();
            this.enabled = false;
        }
        
        void UpdateInjector(DependencyInjector injector)
        {
            injector.moveToPosition = point.transform.position;
        }

        private void Update()
        {
            if (this.enabled && point)
            {
                var ray = new Ray(cameraTransform.position, cameraTransform.forward);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

                if (Physics.Raycast(ray, out var hit, 100f, ~GeneralVariables.PLAYER))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("NpcInteractible"))
                    {
                        hitCollider = hit.collider;
                        foreach (var renderer in hitCollider.GetComponentsInChildren<MeshRenderer>())
                        {
                            originalMaterials.Add(renderer.material);
                            renderer.material = generalData.highlightMaterial;
                        }
                        
                        point.SetActive(false);
                    }
                    else
                    {
                        if (hitCollider)
                        {
                            int i = 0;
                            foreach (var renderer in hitCollider.GetComponentsInChildren<MeshRenderer>())
                            {
                                renderer.material = originalMaterials[i];
                                i++;
                            }
                            
                            hitCollider = null;
                        }
                        
                        originalMaterials.Clear();
                        point.SetActive(true);
                    }
                    
                    point.transform.position = hit.point + Vector3.up * 0.1f;
                    point.transform.rotation = Quaternion.LookRotation(hit.normal);
                    //point.transform.rotation = Quaternion.Euler(point.transform.rotation.eulerAngles.x - 90, point.transform.rotation.eulerAngles.y, point.transform.rotation.eulerAngles.z);
                }
                else
                {
                    // Restore materials
                }
            }
        }
    }
}