using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        transform.Rotate(0, Speed * Time.deltaTime, 0);
    }
}
