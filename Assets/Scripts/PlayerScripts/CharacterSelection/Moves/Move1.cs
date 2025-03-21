using System;
using AIScripts.Friendly.GOAP.Behaviours;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Settings.CharacterSelection.Moves
{
    public class Move1 : MonoBehaviour
    {
        // Pointer gameObject reference
        GameObject pointer;
        Transform cameraTransform;
        
        Inputs inputs;
        InputAction confirmLocation;

        public DependencyInjector injector;
        public event Action OnUpdate;
        
        void OnEnable()
        {
            inputs = new Inputs();
            confirmLocation = inputs.Gameplay.ClickToMove;
            confirmLocation.Enable();
            confirmLocation.performed += NotifyMoveTo;
            
            pointer = Object.Instantiate(GeneralMethods.GetGameObjectByName("Pointer"));
            cameraTransform = GeneralMethods.GetCamera().GetComponentsInChildren<Transform>()[1];
            GeneralMethods.GetPlayer().GetComponent<PlayerDitherSettings>().SetDither(true);
            GeneralMethods.GetRotator().GetComponent<Rotator>().SetInvert(true);
        }

        private void NotifyMoveTo(InputAction.CallbackContext obj)
        {
            OnUpdate?.Invoke(); // Invoke
            UpdateInjector(injector);
            
            // Log the world position or do something with it
            Debug.Log("Mouse World Position: " + pointer.transform.position);

            Destroy(pointer);
            GeneralMethods.GetPlayer().GetComponent<PlayerDitherSettings>().SetDither(false);
            GeneralMethods.GetRotator().GetComponent<Rotator>().SetInvert(false);
            confirmLocation.Disable();
            inputs.Disable();
            this.enabled = false;
        }
        
        void UpdateInjector(DependencyInjector injector)
        {
            injector.moveToPosition = pointer.transform.position;
        }

        private void Update()
        {
            if (this.enabled && pointer)
            {
                var ray = new Ray(cameraTransform.position, cameraTransform.forward);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
                pointer.transform.position = Physics.Raycast(ray, out RaycastHit hit, 100f, GeneralVariables.GROUND) ? hit.point + Vector3.up : pointer.transform.position;
                pointer.transform.rotation = Quaternion.LookRotation(new Vector3(transform.rotation.x - 90, -ray.direction.y, transform.rotation.z));
            }
        }
    }
}