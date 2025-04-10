using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject optionsMenu;
    [SerializeField] public GameObject pauseMenuImage;
    public bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenu.activeSelf)
            {
                CloseOptions();
            }
            else
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        pauseMenuImage.SetActive(isPaused); // Show or hide the PNG image
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
        pauseMenuImage.SetActive(false); // Hide the PNG image when options are open
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        pauseMenuImage.SetActive(true); // Show the PNG image when returning to the pause menu
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        pauseMenuImage.SetActive(false); // Hide the PNG image when resuming
        Time.timeScale = 1;
    }

    public void QuitToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}