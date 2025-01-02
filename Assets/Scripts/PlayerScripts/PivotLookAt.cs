using UnityEngine;

public class PivotLookAt : MonoBehaviour
{
    public Transform lookAt;
    public int maxDistance = 5; // Maximum distance from player
    public int minDistance = 2; // Minimum distance from player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Turn camera
        Vector3 direction = lookAt.position - transform.position;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);

        // Distance camera with scroll wheel
        float distance = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(transform.localPosition.x + distance) <= maxDistance && Mathf.Abs(transform.localPosition.x + distance) >= minDistance)
            transform.localPosition = transform.localPosition + new Vector3(distance, 0);
    }
}
