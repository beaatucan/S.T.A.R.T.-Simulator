using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene (replace "GameScene" with your actual scene name)
        SceneManager.LoadScene("SelectLevel");
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quit Game"); // This will only show in the editor
        Application.Quit();
    }

    public void StartTutorial()
    {
        // Load the game scene (replace "GameScene" with your actual scene name)
        SceneManager.LoadScene("TutorialScene");
    }

    public void StartBack()
    {
        // Load the options scene (replace "OptionsScene" with your actual scene name)
        SceneManager.LoadScene("StartMenu");
    }

    public void StartForest()
    {
        // Load the options scene (replace "OptionsScene" with your actual scene name)
        SceneManager.LoadScene("GameScene 1");
    }

    public void StartCave()
    {
        // Load the options scene (replace "OptionsScene" with your actual scene name)
        SceneManager.LoadScene("GameScene 2");
    }

    public void StartCastle()
    {
        // Load the options scene (replace "OptionsScene" with your actual scene name)
        SceneManager.LoadScene("GameScene 3");
    }

}
