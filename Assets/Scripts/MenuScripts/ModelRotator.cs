using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    public float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Speed * Time.deltaTime, 0);
    }
}
