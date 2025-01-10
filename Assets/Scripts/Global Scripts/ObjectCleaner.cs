using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    void Start()
    {
        // Trova tutti i GameObject in scena (inclusi quelli in DontDestroyOnLoad)
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in allObjects)
        {
            // Verifica se l'oggetto è in DontDestroyOnLoad (non appartiene ad alcuna scena caricata)
            if (obj.scene.name == null)
                Destroy(obj); // Distruggi l'oggetto
            
        }
    }
}
