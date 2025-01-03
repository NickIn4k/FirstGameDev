using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SkipVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer; //Il componente VideoPlayer
    public Image progressCircle;   //Il cerchio di progresso UI
    public float holdTime = 3f;    //Tempo necessario per saltare
    public int SceneIndex;

    private float holdDuration = 0f; //Contatore per il tempo tenuto premuto

    void Update()
    {
        //Controlla se il tasto Enter è premuto
        if (Input.GetKey(KeyCode.Return))
        {
            holdDuration += Time.deltaTime;

            //Aggiorna il cerchio di progresso
            if (progressCircle != null)
                progressCircle.fillAmount = holdDuration / holdTime;

            //Se il tempo necessario è raggiunto, salta il video
            if (holdDuration >= holdTime)
                SkipToNextScene();
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            //Resetta quando il tasto è rilasciato
            holdDuration = 0f;

            if (progressCircle != null)
                progressCircle.fillAmount = 0f;
        }
    }

    private void SkipToNextScene()
    {
        videoPlayer.Stop(); //Ferma il video
        SceneManager.LoadScene(SceneIndex); //Carica la scena successiva
    }
}
