using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshBakeOnStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GeneralMethods.BakeMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
