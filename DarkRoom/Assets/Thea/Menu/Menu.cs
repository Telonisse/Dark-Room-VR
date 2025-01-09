using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Credits()
    {
        SceneManager.LoadScene(2); // to do : Change numbers to actual scenes
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}
