using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public GameObject menuScreen;
    public bool startPaused;
    public bool busy = false, disabled = false;
    [SerializeField] private GameObject gameHUD;

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        gameHUD.SetActive(false);

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
        gameHUD.SetActive(true);
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Graybox");
    }

    public void EasterEgg()
    {
        SceneManager.LoadScene("BetaBuild");
    }

    public void LoadLoop()
    {
        SceneManager.LoadScene("LoopScene"); //, LoadSceneMode.Additive);
    }
}