using UnityEngine;

public class RotateOnCommand : MonoBehaviour
{
    public float Speed; 
    private Quaternion Rotazione; // Rotazione target (usando Quaternion per supportare tutte le rotazioni)
    private bool InRotazione; 

    void Start()
    {
        InRotazione = false; 
        Rotazione = transform.rotation; // Imposta l'orientamento iniziale come target iniziale
    }

    void Update()
    {
        if (InRotazione)
        {
            AspettaRotazione(); // Aggiorna la rotazione
            return;
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
            Ruota(45); //a destra
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            Ruota(-45); //a sinistra
    }

    private void Ruota(int angolo)
    {
        // Nuova rotazione target aggiungendo l'angolo specificato all'attuale orientamento Y
        Rotazione = Quaternion.Euler(0, transform.eulerAngles.y + angolo, 0);
        InRotazione = true;
    }

    private void AspettaRotazione()
    {
        // Interpola gradualmente dalla rotazione corrente a quella target
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotazione, Time.deltaTime * Speed);

        // Controlla se la rotazione corrente è abbastanza vicina alla rotazione target
        if (Quaternion.Angle(transform.rotation, Rotazione) < 0.5f)
        {
            transform.rotation = Rotazione; // Imposta il nuovo orientamento
            InRotazione = false;
        }
    }
}
