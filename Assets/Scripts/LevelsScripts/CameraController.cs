using UnityEngine;
//FUNZIONAMENTO:
//-Con WASD ci si muove
//-Con space e ctrl ci si alza/abbassa
//-Con shift si aumenta la velocità di movimento
//-Con il mouse si ruota la visuale

//SCEGLIERE SE SI VUOLE CHE CI SI POSSA MUOVERE SULLA "Y" SOLO CON space/control O ANCHE IN BASE A DOVE GUARDA LA CAMERA


/*
 * //CODICE CHE PERMETTE IL MOVIMENTO SULLE "Y" ANCHE IN BASE A DOVE GUARDA L'OGGETTO
public class CameraController : MonoBehaviour
{
    public int movementSpeed = 10;
    public int verticalSpeed = 5;
    public int mouseSensitivity = 100;
    public int sprintMultiplier = 2;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Determina la velocità di movimento
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? movementSpeed * sprintMultiplier : movementSpeed;

        // Movimento
        float moveX = Input.GetAxis("Horizontal"); // A e D
        float moveZ = Input.GetAxis("Vertical");   // W e S
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * currentSpeed * Time.deltaTime;

        // Salita e discesa
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position -= Vector3.up * verticalSpeed * Time.deltaTime;
        }

        // Rotazione con il mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        rotationY += mouseX;

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
*/


//CODICE CHE PERMETTE IL MOVIMENTO SULLE "Y" SOLO CON SPACE E CONTROL
public class CameraController : MonoBehaviour
{
    public int movementSpeed = 10;
    public int verticalSpeed = 5;
    public int mouseSensitivity = 100;
    public int sprintMultiplier = 2;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Calcola la velocità attuale
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? movementSpeed * sprintMultiplier : movementSpeed;

        // Ottieni gli input per movimento orizzontale
        float moveX = Input.GetAxis("Horizontal"); // A e D
        float moveZ = Input.GetAxis("Vertical");   // W e S

        // Calcola il vettore di movimento, escludendo la componente verticale
        Vector3 move = (transform.right * moveX + transform.forward * moveZ);
        move.y = 0;

        // Se c'è input, muovi l'oggetto immediatamente
        if (move != Vector3.zero)
        {
            transform.position += move.normalized * currentSpeed * Time.deltaTime;
        }

        // Movimento verticale
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position -= Vector3.up * verticalSpeed * Time.deltaTime;
        }

        // Rotazione con il mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        rotationY += mouseX;

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
