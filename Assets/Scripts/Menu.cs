using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGameMario()
    {
        SceneManager.LoadScene("MarioTrialsScene");
    }
    public void PlayGameUndeadSurvivor()
    {
        SceneManager.LoadScene("UndeadSurvivorScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
