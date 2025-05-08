using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject scoreManagerPrefab;

    private void InitializeManagers()
    {
        // Create GameManager if it doesn't exist
        if (GameManager.Instance == null && gameManagerPrefab != null)
        {
            Instantiate(gameManagerPrefab);
        }

        // Create ScoreManager if it doesn't exist
        if (ScoreManager.Instance == null && scoreManagerPrefab != null)
        {
            Instantiate(scoreManagerPrefab);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void StartTutorial()
    {
        InitializeManagers();
        SceneManager.LoadScene("TutorialScene");
    }

    public void StartBack()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void StartForest()
    {
        InitializeManagers();
        SceneManager.LoadScene("GameScene 1");
    }

    public void StartCave()
    {
        InitializeManagers();
        SceneManager.LoadScene("GameScene 2");
    }

    public void StartCastle()
    {
        InitializeManagers();
        SceneManager.LoadScene("GameScene 3");
    }
}
