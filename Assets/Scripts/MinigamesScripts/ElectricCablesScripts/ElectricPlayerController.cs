using UnityEngine;

public class ElectricPlayerController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float laneSwitchSpeed = 5f;

    //I valori Y delle lane
    private float bottomLaneY = 15.20116f;
    private float middleLaneY = 21.20116f;
    private float topLaneY = 27.20116f;

    private float[] laneYPositions = new float[3];
    private int currentLane = 1; //Lane in cui spawna (centralmente)
    private float targetY;

    public ElectricMinigameManager minigameManager;

    void Start()
    {
        //Imposta i valori Y delle corsie
        laneYPositions[0] = bottomLaneY;
        laneYPositions[1] = middleLaneY;
        laneYPositions[2] = topLaneY;
        
        //Imposta la posizione iniziale del player sulla lane centrale
        transform.position = new Vector3(transform.position.x, laneYPositions[currentLane], transform.position.z);
        targetY = laneYPositions[currentLane];
    }

    void Update()
    {
        //Movimento verso destra
        transform.position += Vector3.right * horizontalSpeed * Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentLane > 0) //S sposta il player verso il basso
            {
                currentLane--;
                targetY = laneYPositions[currentLane];
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentLane < laneYPositions.Length - 1) //W sposta il player verso l'alto
            {
                currentLane++;
                targetY = laneYPositions[currentLane];
            }
        }
        
        //Spostamento in Y
        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, targetY, laneSwitchSpeed * Time.deltaTime);
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InvisibleWall"))
        {
            if (minigameManager != null)
                minigameManager.OnGameOver();
        }
        else if (other.CompareTag("ElectricMinigameEnd"))
        {
            Debug.Log("Hai vinto");
        }
    }
    
    //Metodo per resettare il player alla lane centrale
    public void ResetPlayerLane()
    {
        currentLane = 1;
        targetY = laneYPositions[currentLane];
        Vector3 pos = transform.position;
        pos.y = targetY;
        transform.position = pos;
    }
}
