/*
using UnityEngine;

public class HexagonClickRotate : MonoBehaviour
{
    public float rotationAngle = 60f;  //Gradi di rotazione per ogni click
    public float rotationSpeed = 10f;  //Velocità di rotazione

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))  //Click sinistro (0) o destro (1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Hexagon"))  //Controlla se l'oggetto ha il tag "Hexagon"
                {
                    if (Input.GetMouseButtonDown(0)) //Click sinistro --> Ruota a sinistra
                    {
                        RotateHexagon(hit.transform, -rotationAngle);
                    }
                    else if (Input.GetMouseButtonDown(1)) //Click destro --> Ruota a destra
                    {
                        RotateHexagon(hit.transform, rotationAngle);
                    }
                }
            }
        }
    }

    void RotateHexagon(Transform hex, float angle)
    {
        Quaternion targetRotation = Quaternion.Euler(hex.eulerAngles.x + angle, hex.eulerAngles.y, hex.eulerAngles.z);
        StartCoroutine(RotateSmoothly(hex, targetRotation));
    }

    System.Collections.IEnumerator RotateSmoothly(Transform hex, Quaternion targetRotation)
    {
        while (Quaternion.Angle(hex.rotation, targetRotation) > 0.1f)
        {
            hex.rotation = Quaternion.Lerp(hex.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
        hex.rotation = targetRotation;  //Assicura il valore finale esatto
    }
}
*/


using UnityEngine;

public class HexagonClickRotate : MonoBehaviour
{
    public float rotationAngle = 60f;  // Gradi di rotazione per ogni click
    public float rotationSpeed = 10f;  // Velocità di rotazione (più alto è, più veloce ruota)

    private void Update()
    {
        // 🔍 Debug per verificare se i click vengono rilevati
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Click sinistro rilevato!");
        if (Input.GetMouseButtonDown(1))
            Debug.Log("Click destro rilevato!");

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))  // Click sinistro (0) o destro (1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast ha colpito: " + hit.transform.name); // 🔍 Stampa l'oggetto colpito

                if (hit.transform.CompareTag("Hexagon"))  // Controlla se l'oggetto ha il tag "Hexagon"
                {
                    Debug.Log("Esagono cliccato: " + hit.transform.name); // 🔍 Conferma che è un esagono

                    if (Input.GetMouseButtonDown(0)) // Click sinistro -> Ruota a sinistra
                    {
                        Debug.Log("Ruotiamo a SINISTRA di 60°");
                        RotateHexagon(hit.transform, -rotationAngle);
                    }
                    else if (Input.GetMouseButtonDown(1)) // Click destro -> Ruota a destra
                    {
                        Debug.Log("Ruotiamo a DESTRA di 60°");
                        RotateHexagon(hit.transform, rotationAngle);
                    }
                }
                else
                {
                    Debug.Log("L'oggetto colpito NON è un esagono!");
                }
            }
            else
            {
                Debug.Log("Nessun oggetto colpito dal Raycast!");
            }
        }
    }

    void RotateHexagon(Transform hex, float angle)
    {
        Quaternion targetRotation = Quaternion.Euler(hex.eulerAngles.x + angle, hex.eulerAngles.y, hex.eulerAngles.z);
        StartCoroutine(RotateSmoothly(hex, targetRotation));
    }

    System.Collections.IEnumerator RotateSmoothly(Transform hex, Quaternion targetRotation)
    {
        while (Quaternion.Angle(hex.rotation, targetRotation) > 0.1f)
        {
            hex.rotation = Quaternion.Lerp(hex.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
        hex.rotation = targetRotation;  // Assicura il valore finale esatto
        Debug.Log("Rotazione completata!");
    }
}
