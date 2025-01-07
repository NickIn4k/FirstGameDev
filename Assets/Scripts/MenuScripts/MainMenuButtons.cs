using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuButtons : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex); // Carica la scena corrispondente all'indice specificato
    }

    public void DeleteGame()
    {
        //PlayerPrefs => classe di salvataggio dati del gioco
        PlayerPrefs.SetInt("levelAt", 3);   // Chiave "levelAt" a 3 => reset del gioco alla scena 3 (Livello 1)
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
