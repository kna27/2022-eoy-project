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

    public GameObject animalInfo;
    public TextMeshProUGUI animalName;
    public TextMeshProUGUI animalHunger;
    public TextMeshProUGUI animalThirst;
    public TextMeshProUGUI animalReproductiveUrge;
    public TextMeshProUGUI animalAge;

    RaycastHit hit;
    Ray ray;
    void Start()
    {
        pauseMenu.SetActive(false);
        animalInfo.SetActive(false);
        timeScale = 1f;
        Time.timeScale = timeScale;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject go = hit.transform.gameObject;
                if (go.GetComponent<Bunny>() != null)
                {
                    animalInfo.SetActive(true);
                    animalName.text = "Bunny";
                    animalHunger.text = "Hunger: " + Mathf.Round(go.GetComponent<Bunny>().foodWant);
                    animalThirst.text = "Thirst: " + Mathf.Round(go.GetComponent<Bunny>().waterWant);
                    animalReproductiveUrge.text = "Reproductive Urge: " + Mathf.Round(go.GetComponent<Bunny>().reproductiveUrge);
                    animalAge.text = "Age: " + Mathf.Round(go.GetComponent<Bunny>().age);

                }
                else if (go.GetComponent<Fox>() != null)
                {
                    animalInfo.SetActive(true);
                    animalName.text = "Fox";
                    animalHunger.text = "Hunger: ";
                    animalThirst.text = "Thirst: ";
                    animalReproductiveUrge.text = "Reproductive Urge: ";
                    animalAge.text = "Age: ";
                }
                else
                {
                    animalInfo.SetActive(false);
                }
            }
            else
            {
                animalInfo.SetActive(false);
            }
        }

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
        switch (UIMode)
        {
            case 0:
                statsPanel.SetActive(true);
                controlsPanel.SetActive(true);
                break;
            case 1:
                statsPanel.SetActive(true);
                controlsPanel.SetActive(false);
                break;
            case 2:
                statsPanel.SetActive(false);
                controlsPanel.SetActive(false);
                break;
            default:
                break;
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
