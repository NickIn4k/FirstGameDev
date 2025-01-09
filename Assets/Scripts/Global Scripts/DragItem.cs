using UnityEngine;

public class DragItem : MonoBehaviour
{
    private Vector3 MouseOffset;
    private float mZCoord;

    private void OnMouseDown()
    {
        // Calcola la distanza Z dell'oggetto rispetto alla telecamera
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;

        // Calcola l'offset tra la posizione dell'oggetto e quella del mouse
        MouseOffset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        // Aggiorna la posizione dell'oggetto in base al movimento del mouse
        transform.position = GetMouseWorldPos() + MouseOffset;
    }

    private Vector3 GetMouseWorldPos()
    {
        // Ottieni la posizione del mouse in coordinate dello schermo
        Vector3 MousePos = Input.mousePosition;

        // Aggiungi la distanza Z dell'oggetto rispetto alla telecamera
        MousePos.z = mZCoord;

        // Converti le coordinate dello schermo in coordinate del mondo 3D
        return Camera.main.ScreenToWorldPoint(MousePos);
    }
}
