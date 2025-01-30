using UnityEngine;
using UnityEngine.EventSystems; //Importa il sistema eventi di Unity
using System.Collections;

public class HexagonRotationUI : MonoBehaviour, IPointerClickHandler
{
    public float rotationAngle = 60f;  //Angolo di rotazione per ogni click
    public float rotationSpeed = 10f;  //Velocità della rotazione
    private bool isRotating = false;   //Blocca la rotazione mentre è in corso

    private void Start()
    {
        //Ruota casualmente l'esagono all'inizio
        int randomRotations = Random.Range(0, 6); //0-5 rotazioni (multipli di 60°)
        float randomAngle = randomRotations * rotationAngle;
        transform.rotation = Quaternion.Euler(0, 0, randomAngle);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isRotating) // Se non sta già ruotando
        {
            if (eventData.button == PointerEventData.InputButton.Right) //Click destro
            {
                StartCoroutine(RotateSmoothly(-rotationAngle));
            }
            else if (eventData.button == PointerEventData.InputButton.Left) //Click sinistro
            {
                StartCoroutine(RotateSmoothly(rotationAngle));
            }
        }
    }

    IEnumerator RotateSmoothly(float angle)
    {
        isRotating = true; //Blocca altre rotazioni finché non finisce
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed; //Interpola nel tempo
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        transform.rotation = targetRotation; //Assicura il valore finale esatto
        isRotating = false; //Sblocca per altre rotazioni
    }
}
