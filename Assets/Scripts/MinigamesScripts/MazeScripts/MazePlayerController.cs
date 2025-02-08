using UnityEngine;

public class MazePlayerController : MonoBehaviour
{
    //Velocità di movimento
    public float moveSpeed = 5f; //Velocità di movimento del giocatore

    void Update()
    {
        //Input WASD per il movimento
        float moveX = Input.GetAxisRaw("Horizontal"); //Movimento orizzontale
        float moveY = Input.GetAxisRaw("Vertical"); //Movimento verticale

        //Calcola la direzione di movimento (assumiamo movimento sul piano XZ per un labirinto visto dall'alto)
        Vector3 movement = new Vector3(moveX, 0f, moveY).normalized;
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    //Verifica collisione con l'oggetto "Maze End"
    private void OnTriggerEnter(Collider other)
    {
        //Se il player colpisce un oggetto con il tag "Maze End", stampa "Hai vinto"
        if (other.CompareTag("Maze End"))
        {
            Debug.Log("Hai vinto");
        }
    }
}
