using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject[] clouds;

    private void Start()
    {
        InstantiateCloud();
        InstantiateCloud();
        InstantiateCloud();
    }
    public void InstantiateCloud()
    {
        Instantiate(clouds[Random.Range(0, clouds.Length)], new Vector3(-30, 30, Random.value*-40), Quaternion.Euler(new Vector3(-90, 0, 0)));
    }
}
