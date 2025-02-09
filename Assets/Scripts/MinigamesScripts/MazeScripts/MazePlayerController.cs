using UnityEngine;
using UnityEngine.UI;

public class MazePlayerController : MonoBehaviour
{
    //Velocità di movimento
    public float moveSpeed = 5f; //Velocità di movimento del giocatore
    public float rotationSpeed = 360f; //Velocità di rotazione (gradi al secondo)
    
    public Items Item;
    public GameObject MainCamera;
    public GameObject Maze;
    public GameObject Rotator;
    public GameObject MazePlayer;
    public GameObject Player;
    public GameObject Door;
    public Animator animator;
    
    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    
    void Update()
    {
        //Determina la direzione basata sui tasti WASD per muoversi solo in quattro direzioni
        Vector3 movement = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            movement = Vector3.forward; //Nord (asse Z positivo)
        }
        else if(Input.GetKey(KeyCode.S))
        {
            movement = Vector3.back; //Sud (asse Z negativo)
        }
        else if(Input.GetKey(KeyCode.A))
        {
            movement = Vector3.left; //Ovest (asse X negativo)
        }
        else if(Input.GetKey(KeyCode.D))
        {
            movement = Vector3.right; //Est (asse X positivo)
        }
        
        //Se c'è input, muovi e ruota il modello
        if(movement != Vector3.zero)
        {
            //Muove il giocatore nella direzione scelta
            transform.position += movement * moveSpeed * Time.deltaTime;
            //Calcola la rotazione target con up = Vector3.up per evitare rotolamenti
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            //Ruota gradualmente verso la direzione target
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Maze End"))
        {
            Resume();
        }
    }
    
    private void Resume()
    {
        //Riattiva la grafica di base
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
        if(Door != null)
        {
            Door.SetActive(false);
            animator.SetBool("isOpening", true);
        }
    }
}




/*
using GLTF.Schema;
using Unity.Mathematics;
using UnityEngine;

public class MazePlayerController : MonoBehaviour
{
    //Velocità di movimento
    public float moveSpeed = 5f; //Velocità di movimento del giocatore
    public float rotationSpeed = 360f; //Velocità di rotazione (gradi al secondo)

    public Items Item;
    public GameObject MainCamera;
    public GameObject Maze;
    public GameObject Rotator;
    public GameObject MazePlayer;
    public GameObject Player;
    public GameObject Door;
    public Animator animator;
    
    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        //Input WASD per il movimento
        float moveX = Input.GetAxisRaw("Horizontal"); //Input orizzontale
        float moveY = Input.GetAxisRaw("Vertical"); //Input verticale
       
        //Calcola la direzione di movimento sul piano XZ
        Vector3 movement = new Vector3(moveX, 0f, moveY).normalized;

        //Se c'è input, ruota gradualmente verso la direzione di movimento
        if(movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.right);   
            Quaternion targetRotation1 = Quaternion.LookRotation(movement, Vector3.back);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation1, rotationSpeed * Time.deltaTime);          
        }

        //Sposta il giocatore nella direzione di movimento
        transform.position += movement * moveSpeed * Time.deltaTime;
        
    }

    //Controlla le collisioni con l'oggetto "Maze End"
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Maze End"))
        {
            Resume();
        }
    }

    private void Resume()
    {
        //Riattiva la grafica di base
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
        //Rende il collider della porta un trigger

        if (Door != null)
        {
            Door.SetActive(false);
            animator.SetBool("isOpening", true);
        }
    }
}
*/