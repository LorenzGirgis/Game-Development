using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startScreenCanvas;

    void Start()
    {
        // Ensure the game starts with the start screen visible
        Time.timeScale = 0;
        startScreenCanvas.SetActive(true);
    }

    public void StartGame()
    {
        // Start the game by setting time scale to 1 and hiding the start screen canvas
        Time.timeScale = 1;
        startScreenCanvas.SetActive(false);
    }

    public void RestartGame()
    {
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
