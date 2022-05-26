using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject settingsMenuUI;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        mainUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        mainUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }
}
