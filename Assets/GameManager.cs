using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject statsPanel;
    public GameObject controlsPanel;
    public float timeScale;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI chickenPopulation;
    public TextMeshProUGUI foxPopulation;
    public TextMeshProUGUI food;
    public int UIMode = 0;
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
        if (Input.GetKeyDown(KeyCode.RightArrow) && timeScale < 20f)
        {
            timeScale += 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIMode += 1;
            if (UIMode > 2)
            {
                UIMode = 0;
            }
        }
        if (UIMode == 0)
        {
            statsPanel.SetActive(true);
            controlsPanel.SetActive(true);
        }
        else if (UIMode == 1)
        {
            statsPanel.SetActive(true);
            controlsPanel.SetActive(false);
        }
        else if (UIMode == 2)
        {
            statsPanel.SetActive(false);
            controlsPanel.SetActive(false);
        }

        Time.timeScale = pauseMenu.activeInHierarchy ? 0f : timeScale;
        timeText.text = "Speed: " + (timeScale * 100) + "%";
        chickenPopulation.text = "Chicken Population: " + GameObject.FindGameObjectsWithTag("Chicken").Length;
        foxPopulation.text = "Fox Population: " + GameObject.FindGameObjectsWithTag("Fox").Length;
        food.text = "Food: " + GameObject.FindGameObjectsWithTag("Food").Length;
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
