using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int index;

    public void LoadScenes(int index)
    {
        Time.timeScale = 1f;        // Tempo di gioco a velocit� normale
        SceneManager.LoadScene(index);
    }
}
