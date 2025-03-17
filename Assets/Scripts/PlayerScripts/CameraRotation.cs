using Unity.Mathematics;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    Transform plr;
    Transform lookAtCamera;
    
    public float pLerp = .1f; // Speed of easing
    public float rLerp = .5f; // Speed of easing

    private void Start()
    {
        plr = GeneralMethods.GetPlayer().transform;
        lookAtCamera = GeneralMethods.GetRotator().GetComponentsInChildren<Transform>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        // Move camera
        var ray = new Ray(plr.position, lookAtCamera.position - plr.position);
        
        // If it finds an obstacle, get the hitPoint and lerp the position to that of the hitPoint
        float maxDistance = (float)Mathf.Sqrt(Mathf.Pow(lookAtCamera.position.x - plr.position.x, 2) +
                                              Mathf.Pow(lookAtCamera.position.y - plr.position.y, 2) +
                                              Mathf.Pow(lookAtCamera.position.z - plr.position.z, 2)) + 1f;
        
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
        
        transform.position = Vector3.Lerp(
            transform.position, 
            Physics.Raycast(ray, out var hit, maxDistance) ? hit.point : lookAtCamera.position, 
            pLerp);

        // Rotate camera
        Vector3 eulerRotation = new Vector3( 
            lookAtCamera.eulerAngles.x, 
            lookAtCamera.eulerAngles.y, 
            lookAtCamera.eulerAngles.z);
        
        transform.rotation = Quaternion.Lerp(
            transform.rotation, 
            Quaternion.Euler(eulerRotation),
            rLerp);
    }
}