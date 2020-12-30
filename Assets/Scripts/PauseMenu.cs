using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject audioSourcePause;
    public GameObject audioSourceGame;

    public TMP_Text musicTextComponent;

    public bool _isPaused;

    public bool _playMusic;

    // Start is called before the first frame update
    void Start()
    {
        _playMusic = true;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }

    public void ToggleMusic()
    {
        if (_playMusic)
        {
            musicTextComponent.text = "MUSIC: OFF";
        }
        else
        {
            musicTextComponent.text = "MUSIC: ON";
        }
        _playMusic = !_playMusic;
        audioSourcePause.SetActive(_playMusic);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        // Play menu music if it's active
        audioSourcePause.SetActive(_playMusic);
        audioSourceGame.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        // Pause menu music on resume game
        audioSourcePause.SetActive(false);
        audioSourceGame.SetActive(_playMusic);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
