using Settings;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CursorUnlockedScenes 
{
    MapSelector = 1,
}

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
                Debug.Log("HAI VINTO!!");
            else    //Se ci sono ancora livelli da sbloccare
            {
                if (Enum.IsDefined(typeof(CursorUnlockedScenes), nextSceneLoad)) //Sblocca il cursore se non è un livello
                    CursorSettings.Unlock();
                //SceneManager.LoadScene(nextSceneLoad);  //Carico il livello successivo

                if(nextSceneLoad > PlayerPrefs.GetInt("levelAt"))   //Legge l'ultimo livello sbloccato
                {   //Se ho sbloccato un nuovo livello lo salvo
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
            }

            CursorSettings.Unlock();
            SceneManager.LoadScene(1);
        }
    }
}