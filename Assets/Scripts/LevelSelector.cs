using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int SceneIndex;
    public Canvas canvas;
    public TextMeshProUGUI hoverText; 
    public string message;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hoverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log($"Hai cliccato su: {gameObject.name}. Caricamento scena numero: {SceneIndex}");
        SceneManager.LoadScene(SceneIndex);
    }

    private void OnMouseEnter()
    {
        hoverText.text = message;
        hoverText.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoverText.gameObject.SetActive(false);
    }
}
