using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SwapScene : MonoBehaviour
{
    [SerializeField]
    public VideoPlayer VideoPlayer;
    public int SceneIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VideoPlayer.loopPointReached += OnVideoFinished;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
