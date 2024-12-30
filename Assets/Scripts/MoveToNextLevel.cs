using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //Controlla se è il player a toccare il trigger
        {
            if(SceneManager.GetActiveScene().buildIndex == 5)   //Se completo l'ultimo livello (5 è l'int del livello 3)
            {
                Debug.Log("HAI VINTO!!");
                SceneManager.LoadScene(1);  //Quando completi il gioco PER ORA carica la selezione dei livelli
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
}
