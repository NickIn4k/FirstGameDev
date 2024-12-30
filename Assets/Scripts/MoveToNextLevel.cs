using UnityEngine;
using UnityEngine.SceneManagement;

//SCRIPT DA ASSEGNARE ALL'OGGETTO CON L'HITBOX PER ANDARE AL LIVELLO SUCCESSIVO
public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    //
    //
    //AGGIUNGERE FUNZIONE RICHIAMATA QUANDO SI COMPLETA UN LIVELLO
    //
    //
    public void nextLevel()    //MODIFICARE CON UN TRIGGER O ALTRO CHE SCATTA QUANDO SI FINISCE UN LIVELLO
    {
        if(SceneManager.GetActiveScene().buildIndex == 5)   //Se completo l'ultimo livello (5 è l'int del livello 3)
        {
            Debug.Log("HAI VINTO!!");
        }
        else    //Se ci sono ancora livelli da sbloccare
        {
            SceneManager.LoadScene(nextSceneLoad);  //Carico il livello successivo

            if(nextSceneLoad > PlayerPrefs.GetInt("levelAt"))   //Legge l'ultimo livello sbloccato
            {   //Se ho sbloccato un nuovo livello lo salvo
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }
}
