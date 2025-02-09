using UnityEngine;
using UnityEngine.UI;

public class MazePlayerController : MonoBehaviour
{
    // Velocità di movimento e rotazione
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f;

    public Items Item;
    public GameObject MainCamera;
    public GameObject Maze;
    public GameObject Rotator;
    public GameObject MazePlayer;
    public GameObject Player;
    public GameObject Door;
    public Animator animator;

    // Salva la rotazione iniziale (impostata nell'editor)
    private Vector3 initialEuler;

    // Offset per correggere l'orientamento del modello sull'asse Y (es. 180 se il modello è capovolto)
    public float yRotationOffset = 180f;

    void Awake()
    {
        // Salva la rotazione iniziale (X, Y, Z) impostata nell'editor
        initialEuler = transform.rotation.eulerAngles;

        // Impedisce al Rigidbody di ruotare sugli assi X e Z
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // Determina la direzione basata sui tasti WASD (movimento nelle 4 direzioni)
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement = Vector3.forward; // Nord (asse Z positivo)
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement = Vector3.back;    // Sud (asse Z negativo)
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement = Vector3.left;    // Ovest (asse X negativo)
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement = Vector3.right;   // Est (asse X positivo)
        }

        if (movement != Vector3.zero)
        {
            // Muove il giocatore nella direzione scelta
            transform.position += movement * moveSpeed * Time.deltaTime;

            // Calcola la rotazione target basata sul movimento
            // Applichiamo un offset sull'asse Y per correggere la direzione del modello
            Quaternion fullTargetRotation = Quaternion.LookRotation(movement, Vector3.up) *
                                            Quaternion.Euler(0, yRotationOffset, 0);
            float targetY = fullTargetRotation.eulerAngles.y;
            float currentY = transform.rotation.eulerAngles.y;
            // Ruota gradualmente l'asse Y verso il target
            float newY = Mathf.MoveTowardsAngle(currentY, targetY, rotationSpeed * Time.deltaTime);

            // Imposta la nuova rotazione mantenendo gli assi X e Z della rotazione iniziale
            transform.rotation = Quaternion.Euler(initialEuler.x, newY, initialEuler.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Maze End"))
        {
            Resume();
        }
    }

    private void Resume()
    {
        // Riattiva la grafica di base e altre impostazioni di fine livello
        Cursor.lockState = CursorLockMode.Locked;
        Player.SetActive(true);
        Rotator.SetActive(true);
        MainCamera.SetActive(true);

        Maze.SetActive(false);
        MazePlayer.SetActive(false);

        Time.timeScale = 1f;
        OpenTheDoor();
    }

    private void OpenTheDoor()
    {
        if (Door != null)
        {
            Door.SetActive(false);
            animator.SetBool("isOpening", true);
        }
    }
}