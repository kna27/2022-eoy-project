using System.Collections;
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

    public GameObject foodPrefab;
    public int foodSpawnChance = 50;
    private int foodCount;
    GameObject[] grassTiles;

    RaycastHit hit;
    Ray ray;

    GameObject selectedObject;
    void Start()
    {
        pauseMenu.SetActive(false);
        animalInfo.SetActive(false);
        timeScale = 1f;
        Time.timeScale = timeScale;

        grassTiles = GameObject.FindGameObjectsWithTag("Grass");
        foreach (GameObject tile in grassTiles)
        {
            if (Random.Range(0, 100) <= foodSpawnChance)
            {
                Instantiate(foodPrefab, tile.transform.position, Quaternion.identity);
            }
        }
        foodCount = GameObject.FindGameObjectsWithTag("Food").Length;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject go = hit.transform.gameObject;
                if (go.GetComponent<Bunny>() != null || go.GetComponent<Fox>() != null)
                {
                    selectedObject = go;
                    animalInfo.SetActive(true);
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
        if (selectedObject != null)
        {
            if (selectedObject.GetComponent<Bunny>() != null)
            {
                animalName.text = "Bunny" + " (" + selectedObject.GetComponent<Bunny>().status + ")";
                animalHunger.text = "Hunger: " + Mathf.Round(selectedObject.GetComponent<Bunny>().foodWant);
                animalThirst.text = "Thirst: " + Mathf.Round(selectedObject.GetComponent<Bunny>().waterWant);
                animalReproductiveUrge.text = "Reproductive Urge: " + Mathf.Round(selectedObject.GetComponent<Bunny>().reproductiveUrge);
                animalAge.text = "Age: " + Mathf.Round(selectedObject.GetComponent<Bunny>().age);
            }
            else
            {
                animalInfo.SetActive(true);
                animalName.text = "Fox";
                animalHunger.text = "Hunger: ";
                animalThirst.text = "Thirst: ";
                animalReproductiveUrge.text = "Reproductive Urge: ";
                animalAge.text = "Age: ";
            }
        }
        else
        {
            animalInfo.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && timeScale > 0f)
        {
            timeScale -= 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && timeScale < 30f)
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

        if (GameObject.FindGameObjectsWithTag("Food").Length - 2 < foodCount)
        {
            StartCoroutine(SpawnFood());
        }
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

    IEnumerator SpawnFood()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] currentFood = GameObject.FindGameObjectsWithTag("Food");
        int diff = foodCount - currentFood.Length;
        foreach (GameObject tile in grassTiles)
        {
            if (diff <= 0)
            {
                break;
            }
            bool availableTile = true;
            foreach (GameObject food in currentFood)
            {
                if (food.transform.position == tile.transform.position)
                {
                    availableTile = false;
                    break;
                }
            }
            if (availableTile)
            {
                Instantiate(foodPrefab, tile.transform.position, Quaternion.identity);
                diff--;
            }
        }
    }
}
