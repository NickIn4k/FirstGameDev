using TMPro; // namespace per gestire TextMeshPro
using UnityEngine; 
using UnityEngine.SceneManagement; 

public class LevelSelector : MonoBehaviour
{
    public int SceneIndex; //da caricare
    public Canvas canvas;
    public TextMeshProUGUI hoverText;       // Testo da far apparire durante l'hover
    public string message;

    void Start()
    {
        hoverText.text = "Use <- or -> to move\naround the map";   //aggiorna il testo
    }

    private void OnMouseDown()  //onclick
    {
        Debug.Log($"Hai cliccato su: {gameObject.name}. Caricamento scena numero: {SceneIndex}");

        SceneManager.LoadScene(SceneIndex);
    }

    private void OnMouseEnter() //Mouse over
    {
        hoverText.text = message;   //aggiorna il testo
        hoverText.gameObject.SetActive(true); // Rendi visibile
    }

    private void OnMouseExit()  //Il mouse non è più sopra
    {
        hoverText.text = "Use <- or -> to move\naround the map";   //aggiorna il testo
    }
}
