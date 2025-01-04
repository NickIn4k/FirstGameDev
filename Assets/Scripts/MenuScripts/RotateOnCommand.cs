using UnityEngine;

public class RotateOnCommand : MonoBehaviour
{
    public float Speed;
    private Quaternion Rotazione; //Quaternion => oggetto per le rotazioni di qualsiasi tipo
    private bool InRotazione;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InRotazione = false;
        Rotazione = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (InRotazione)
        {
            AspettaRotazione();
            return;
        }      
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Ruota(45); 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            Ruota(-45);
    }
    private void Ruota(int angolo)
    {
        Rotazione = Quaternion.Euler(0, transform.eulerAngles.y + angolo, 0);
        InRotazione = true;
    }

    private void AspettaRotazione()
    {        
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotazione, Time.deltaTime * Speed);   // Interpola verso la rotazione target

        if (Quaternion.Angle(transform.rotation, Rotazione) < 0.5f)
        {
            transform.rotation = Rotazione; // Imposta esattamente la rotazione target
            InRotazione = false;
        }
    }
}
