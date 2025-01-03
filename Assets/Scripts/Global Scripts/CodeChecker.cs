using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeChecker : MonoBehaviour
{
    [SerializeField] TMP_InputField InputField;
    [SerializeField] TMP_Text OutputText;

    private string code;
    public static bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        code = "143652";
    }
    
    public void ValidateInput()
    {
        if (!isOpen)
        {
            OutputText.text = string.Empty;
            string input = InputField.text.Split(' ', System.StringSplitOptions.RemoveEmptyEntries)[2];
            Debug.Log(input);
            if (!string.IsNullOrEmpty(input) && input == code)
            {
                OutputText.text = "     >: Valid input..\n          Now opening the door..";
                OutputText.color = Color.green;
                isOpen = true;
                //codice per cambiare le impostazioni della porta
            }
            else
            {
                OutputText.text = "     >: Invalid input..\n          Please try again..";
                OutputText.color = Color.red;
            }
        }
        else
        {
            OutputText.text = "     >: The door has already been opened!";
            OutputText.color = Color.green;
        }
        
    }
}
