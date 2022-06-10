using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public float timeScale;
    public TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        timeScale = 1f;
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && timeScale > 0f)
        {
            timeScale -= 0.5f;
        }
        if (Input.GetKeyDown (KeyCode.RightArrow) && timeScale < 10f)
        {
            timeScale += 0.5f;
        }
        Time.timeScale = pauseMenu.activeInHierarchy ? 0f : timeScale;
        timeText.text = "Speed: " + (timeScale * 100) + "%";
        
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = timeScale;
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
