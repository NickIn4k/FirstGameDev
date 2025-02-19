using Unity.Mathematics;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    Transform plr;
    Transform lookAt;

    public float pLerp = .1f; // Speed of easing
    public float rLerp = .5f; // Speed of easing

    private void Start()
    {
        plr = GeneralMethods.GetPlayer().transform;
        lookAt = GeneralMethods.GetRotator().GetComponentsInChildren<Transform>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        // Move camera
        var ray = new Ray(plr.position, lookAt.position - plr.position);

        // If it finds an obstacle, get the hitPoint and lerp the position to that of the hitPoint
        if (Physics.Raycast(ray, out var hit, (float)Mathf.Sqrt(Mathf.Pow(lookAt.position.x - plr.position.x, 2) + Mathf.Pow(lookAt.position.y - plr.position.y, 2) + Mathf.Pow(lookAt.position.z - plr.position.z, 2)) + 1f)) 
            transform.position = Vector3.Lerp(transform.position, hit.point, pLerp);
        else
            transform.position = Vector3.Lerp(transform.position, lookAt.position, pLerp);

        // Rotate camera
        Vector3 eulerRotation = new Vector3(lookAt.eulerAngles.x, lookAt.eulerAngles.y, lookAt.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerRotation), rLerp);
    }
}