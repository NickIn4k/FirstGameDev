using Unity.Mathematics;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform lookAt;
    public Transform lookAtMoving;

    public float pLerp = .1f; // Speed of easing
    public float rLerp = .5f; // Speed of easing

    // Update is called once per frame
    void Update()
    {
        // Move camera
        transform.position = Vector3.Lerp(transform.position, lookAt.position, pLerp);

        Vector3 eulerRotation = new Vector3(lookAt.eulerAngles.x, lookAt.eulerAngles.y, lookAt.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerRotation), rLerp);
    }
}