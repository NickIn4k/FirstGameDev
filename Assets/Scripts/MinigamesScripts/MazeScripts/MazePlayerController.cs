using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MazePlayerController : MonoBehaviour
{
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
    public AudioSource Src;
    public AudioClip Sfx_Door;
    public AudioClip MoveSfx;
    public float yRotationOffset = 180f;
    private Vector3 initialEuler;
    private Vector3 initialPosition;

    void Awake()
    {
        initialEuler = transform.rotation.eulerAngles;
        initialPosition = transform.position; //Salva la posizione iniziale
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; //Impedisce la rotazione sugli assi X e Z
    }

    void Update()
    {
        if (transform.position.y <= -1f)
            Respawn(); //Se la posizione Y è sotto -1, il giocatore viene respawnato

        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            movement = Vector3.forward;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            movement = Vector3.back;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            movement = Vector3.left;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            movement = Vector3.right;

        if (movement != Vector3.zero)
        {
            if (!Src.isPlaying)
            {
                Src.clip = MoveSfx;
                Src.Play(); //Riproduce il suono di movimento se non è già in esecuzione
            }

            transform.position += movement * moveSpeed * Time.deltaTime; //Muove il giocatore nella direzione scelta

            Quaternion fullTargetRotation = Quaternion.LookRotation(movement, Vector3.up) *
                                            Quaternion.Euler(0, yRotationOffset, 0);
            float targetY = fullTargetRotation.eulerAngles.y;
            float currentY = transform.rotation.eulerAngles.y;
            float newY = Mathf.MoveTowardsAngle(currentY, targetY, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(initialEuler.x, newY, initialEuler.z);
        }
        else
        {
            Src.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Maze End"))
            Resume(); //Attiva la funzione di fine livello quando il giocatore raggiunge l'uscita del labirinto
    }

    private void Respawn()
    {
        transform.position = initialPosition; //Riporta il giocatore alla posizione iniziale
        transform.rotation = Quaternion.Euler(initialEuler);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero; //Resetta la velocità per evitare movimenti indesiderati
        rb.angularVelocity = Vector3.zero;
    }

    private void Resume()
    {
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
            StartCoroutine(AttendiAnimazione());
            Src.clip = Sfx_Door;
            Src.Play(); //Riproduce il suono della porta che si apre
        }
    }

    public IEnumerator AttendiAnimazione()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
