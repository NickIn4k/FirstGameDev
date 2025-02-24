using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    // GameObject da spostare (assegnabile tramite l'Inspector)
    public GameObject objectToMove;

    // Velocità di movimento
    public float speed = 5f;

    // Posizione Z di destinazione
    public float targetZ = 10f;

    // Posizione iniziale dell'oggetto
    private Vector3 originalPosition;

    void Start()
    {
        if (objectToMove != null)
        {
            // Salva la posizione iniziale dell'oggetto
            originalPosition = objectToMove.transform.position;
        }
        else
        {
            Debug.LogError("Nessun oggetto assegnato a 'objectToMove'!");
        }
    }

    void Update()
    {
        if (objectToMove == null)
            return;

        // Sposta l'oggetto lungo l'asse Z
        objectToMove.transform.position += Vector3.forward * speed * Time.deltaTime;

        // Se la posizione Z dell'oggetto raggiunge o supera il target, resetta la posizione
        if (objectToMove.transform.position.z >= targetZ)
        {
            objectToMove.transform.position = originalPosition;
        }
    }
}
