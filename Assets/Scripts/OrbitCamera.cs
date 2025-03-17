using UnityEngine;

public class AutoOrbitCamera : MonoBehaviour
{
    [Header("Target e Distanza")]
    public Transform target;              // L'oggetto attorno a cui orbitare
    public float distance = 10f;            // Distanza dalla target

    [Header("Velocità di Rotazione")]
    public float horizontalSpeed = 10f;     // Velocità di rotazione orizzontale
    public float verticalSpeed = 5f;        // Velocità di rotazione verticale

    [Header("Limiti Verticali (Pitch)")]
    public float minPitch = 0f;             // Il pitch minimo: la camera non scende sotto la base del target
    public float maxPitch = 80f;            // Limite superiore per il pitch

    private float yaw = 0f;               // Angolo orizzontale
    private float pitch = 0f;             // Angolo verticale iniziale: 0 significa che la camera parte alla base del target
    private bool verticalDirectionUp = true; // Controlla la direzione verticale

    void Start()
    {
        if (target != null)
        {
            // Inizializza yaw in base all'orientamento attuale della camera
            yaw = transform.eulerAngles.y;
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Rotazione orizzontale automatica
            yaw += horizontalSpeed * Time.deltaTime;

            // Rotazione verticale automatica con effetto "rimbalzo" tra minPitch e maxPitch
            if (verticalDirectionUp)
            {
                pitch += verticalSpeed * Time.deltaTime;
                if (pitch >= maxPitch)
                {
                    pitch = maxPitch;
                    verticalDirectionUp = false;
                }
            }
            else
            {
                pitch -= verticalSpeed * Time.deltaTime;
                if (pitch <= minPitch)
                {
                    pitch = minPitch;
                    verticalDirectionUp = true;
                }
            }

            // Calcola la posizione della camera in base a coordinate sferiche
            float radPitch = pitch * Mathf.Deg2Rad;
            float radYaw = yaw * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(
                distance * Mathf.Cos(radPitch) * Mathf.Sin(radYaw),
                distance * Mathf.Sin(radPitch),
                distance * Mathf.Cos(radPitch) * Mathf.Cos(radYaw)
            );

            // La camera parte alla base del target: si aggiunge l'offset alla posizione del target
            transform.position = target.position + offset;
            transform.LookAt(target.position);
        }
    }
}
