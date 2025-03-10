using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeChecker : MonoBehaviour
{
    [SerializeField] TMP_InputField InputField; // Input per il codice
    [SerializeField] TMP_Text OutputText;      
    [SerializeField] GameObject Door;          
    [SerializeField] Material OpenMaterial;    

    public string code;        // Codice segreto da validare
    public static bool isOpen = false;
    public GameObject EndDoor;
    private Animator animator;
    public AudioSource src;
    public AudioClip Sfx;

    void Start()
    {
        isOpen = false;
        InputField.text = "...";
        animator = EndDoor.GetComponentInChildren<Animator>();
    }

    public void ValidateInput()
    {
        if (!isOpen) // Controlla se la porta non � gi� aperta
        {
            OutputText.text = string.Empty;
            string input = InputField.text; // Input dell'utente
            Debug.Log(input); 

            if (!string.IsNullOrEmpty(input) && input == code)
            {
                OutputText.text = "     >: Valid input..\n          Now opening the door..";
                OutputText.color = Color.green; // Testo di feedback in verde
                isOpen = true;
                OpenTheDoor(Door, OpenMaterial);
            }
            else
            {
                OutputText.text = "     >: Invalid input..\n          Please try again..";
                OutputText.color = Color.red; // Testo di feedback in rosso
            }
        }
        else    // La porta � gi� aperta
        { 
            OutputText.text = "     >: The door has already been opened!";
            OutputText.color = Color.yellow;
        }
    }

    private void OpenTheDoor(GameObject Door, Material OpenMaterial)
    {
        // Cambia il materiale della porta
        Renderer renderer = Door.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = OpenMaterial; // Applica il materiale "aperto"
        }

        // Rende il collider della porta un trigger
        Collider collider = Door.GetComponent<Collider>();
        if (collider != null)
        {
            src.clip = Sfx;
            src.Play();
            animator.SetBool("isOpening", true);
            collider.isTrigger = true; // Attiva la modalit� trigger
        }

        Debug.Log("The Tower is now accessible!");
    }
}
