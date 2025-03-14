using UnityEngine;

public class ElectricPlayerController : MonoBehaviour
{
    public GameObject MinigameCamera;
    public GameObject MainCamera;
    public GameObject Door;
    public GameObject UI;
    public Animator Animator;

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
    public ElectricManagerV2 minigameManagerV2;

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
            if (minigameManagerV2 != null)
                minigameManagerV2.OnGameOver();
        }
        else if (other.CompareTag("ElectricMinigameEnd"))
        {
            if (minigameManager != null)
                minigameManager?.OnWin();
            if(minigameManagerV2 != null)
                minigameManagerV2?.OnWin();
            OpenTheDoor();
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

    private void OpenTheDoor()
    {
        //Rende il collider della porta un trigger

        if (Door != null && Animator != null) 
        {
            Door.SetActive(false);
            Animator.SetBool("isOpening", true);
        }
    }
}
