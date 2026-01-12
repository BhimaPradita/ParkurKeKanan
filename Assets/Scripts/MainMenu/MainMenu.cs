using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Quit()
    {
        Debug.Log("Exiting Game");
        Application.Quit();

        // For the Editor only
        UnityEditor.EditorApplication.isPlaying = false;

    }
}