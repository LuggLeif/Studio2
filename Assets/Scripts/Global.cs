using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public GameObject menuScreen;
    public bool startPaused;
    public bool busy = false, disabled = false;

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        if (startPaused)
             Pause();
    }
    
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;
        
        if (!menuScreen.activeSelf)
            Pause();
        else
            Play();
    }
    
    public void Pause()
    {
        menuScreen.SetActive(true);
        Time.timeScale = 0;
        busy = true;
        disabled = true;
    }
    
    public void Play()
    {
        menuScreen.SetActive(false);
        Time.timeScale = 1;
        busy = false;
        disabled = false;
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("BetaBuild");
    }
    
    public void LoadLoop()
    {
        SceneManager.LoadScene("LoopScene"); //, LoadSceneMode.Additive);
    }
}