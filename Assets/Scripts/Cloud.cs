using UnityEngine;

public class Cloud : MonoBehaviour
{
    float speed;
    private void Start()
    {
        speed = (Random.value * 3) + 1;
    }
    void Update()
    {
        if (transform.position.x > 80f)
        {
            GameObject.Find("Clouds").GetComponent<CloudManager>().InstantiateCloud();
            Destroy(gameObject);
        }
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
