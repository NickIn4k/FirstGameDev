using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaryManager : MonoBehaviour
{
    //Array di stringhe da visualizzare sul pannello
    public string[] testi;

    //Riferimento al componente Text del pannello
    public TMP_Text panelText;

    //Riferimenti ai bottoni per navigare tra i testi
    public Button bottonePrecedente;
    public Button bottoneSuccessivo;

    //Indice corrente nel array
    private int indiceCorrente = 0;

    void Start()
    {
        //Verifica che l'array contenga almeno un testo
        if (testi == null || testi.Length == 0)
        {
            Debug.LogError("L'array di testi è vuoto o non impostato!");
            return;
        }

        //Imposta il testo iniziale e aggiorna i bottoni
        indiceCorrente = 0;
        AggiornaPannello();
    }

    //Metodo da collegare all'evento OnClick del bottone precedente
    public void TestoPrecedente()
    {
        if (indiceCorrente > 0)
        {
            indiceCorrente--;
            AggiornaPannello();
        }
    }

    //Metodo da collegare all'evento OnClick del bottone successivo
    public void TestoSuccessivo()
    {
        if (indiceCorrente < testi.Length - 1)
        {
            indiceCorrente++;
            AggiornaPannello();
        }
    }

    //Aggiorna il testo del pannello e lo stato (visibilità/interattività) dei bottoni
    private void AggiornaPannello()
    {
        //Aggiorna il testo sul pannello
        panelText.text = testi[indiceCorrente];

        //Se siamo sul primo testo, disabilita e nascondi il bottone precedente
        if (indiceCorrente == 0)
        {
            bottonePrecedente.interactable = false;
            bottonePrecedente.gameObject.SetActive(false);
        }
        else
        {
            bottonePrecedente.interactable = true;
            bottonePrecedente.gameObject.SetActive(true);
        }

        //Se siamo sull'ultimo testo, disabilita e nascondi il bottone successivo
        if (indiceCorrente == testi.Length - 1)
        {
            bottoneSuccessivo.interactable = false;
            bottoneSuccessivo.gameObject.SetActive(false);
        }
        else
        {
            bottoneSuccessivo.interactable = true;
            bottoneSuccessivo.gameObject.SetActive(true);
        }
    }
}