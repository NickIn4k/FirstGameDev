using UnityEngine;

public class ElectricCameraScroller : MonoBehaviour
{
    //Velocità di scorrimento della camera
    public float scrollSpeed = 5f;
    //Punto finale per la camera (da assegnare tramite Inspector)
    public Transform cameraEndPos;

    //Variabile per salvare la posizione finale fissa (target) della camera
    private Vector3 targetCameraPos;

    void Awake()
    {
        //Se è stato assegnato un punto finale, salviamo la sua posizione nel mondo
        if (cameraEndPos != null)
        {
            targetCameraPos = cameraEndPos.position;
        }
    }

    void Update()
    {
        if (cameraEndPos != null)
        {
            // Costruiamo la posizione target mantenendo le coordinate y e z correnti della camera
            Vector3 targetPos = new Vector3(targetCameraPos.x, transform.position.y, transform.position.z);
            // Muoviamo la telecamera verso il target e una volta raggiunto non si sposterà oltre
            transform.position = Vector3.MoveTowards(transform.position, targetPos, scrollSpeed * Time.deltaTime);
        }
        else
        {
            // Se non è stato definito un endpoint, la camera si muove continuamente
            transform.position += Vector3.right * scrollSpeed * Time.deltaTime;
        }
    }
}
