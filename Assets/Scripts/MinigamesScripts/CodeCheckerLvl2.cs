using TMPro;
using UnityEngine;

public class CodeCheckerLvl2 : MonoBehaviour
{
    [SerializeField] TMP_InputField InputField; // Input per il codice
    [SerializeField] TMP_Text OutputText;
    
    public GameObject Door;
    public string code;        // Codice segreto da validare
    public static bool isOpen = false;
    public Animator animator;

    public AudioSource src;
    public AudioClip clip;

    void Start()
    {
        isOpen = false;
        InputField.text = "...";
    }

    public void ValidateInput()
    {
        if (!isOpen) // Controlla se la porta non è già aperta
        {
            OutputText.text = string.Empty;
            string input = InputField.text; // Input dell'utente
            Debug.Log(input);

            if (!string.IsNullOrEmpty(input) && input == code)
            {
                OutputText.text = "     >: Valid input..\n          Now opening the door..";
                OutputText.color = Color.green; // Testo di feedback in verde
                isOpen = true;
                OpenTheDoor();
            }
            else
            {
                OutputText.text = "     >: Invalid input..\n          Please try again..";
                OutputText.color = Color.red; // Testo di feedback in rosso
            }
        }
        else    // La porta è già aperta
        {
            OutputText.text = "     >: The door has already been opened!";
            OutputText.color = Color.yellow;
        }
    }
    private void OpenTheDoor()
    {
        if (Door != null)
        {
            Door.SetActive(false);
            animator.SetBool("isOpening", true);
            src.PlayOneShot(clip);
        }
    }
}
