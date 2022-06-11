using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.Random;
using UnityEngine.AI;

public class Bunny : MonoBehaviour
{
    public float waterWant;
    public float foodWant;
    public float reproductiveUrge;
    public float lookRadius = 10f;
    public float stoppingDistance = 5f;
    public float age;
    public GameObject chicken;
    Transform target;
    NavMeshAgent agent;
    List<Collider> landColliders = new List<Collider>();


    void Start()
    {
        waterWant = Range(1, 10);
        foodWant = Range(1, 10);
        reproductiveUrge = Range(10, 30);
        target = transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {

        foodWant += Time.deltaTime * 2.5f;
        waterWant += Time.deltaTime * 4.5f;
        reproductiveUrge += foodWant > 40 ? Time.deltaTime * -1f : Time.deltaTime * 1.5f;

        foodWant = Mathf.Clamp(foodWant, 0, 100);
        waterWant = Mathf.Clamp(waterWant, 0, 100);
        reproductiveUrge = Mathf.Clamp(reproductiveUrge, 0, 100);

        if (foodWant >= 100 || waterWant >= 100)
        {
            Die();
        }

        if (waterWant > 50)
        {
            Drink();
        }
        else if (foodWant > 50)
        {
            Eat();
        }
        else if (reproductiveUrge > 70)
        {
            Reproduce();
        }

        if (transform.position != target.position)
        {
            agent.destination = target.position;
            agent.updateRotation = false;
            List<Collider> landColliders = new List<Collider>();
        }
        else
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, lookRadius);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].transform.gameObject.layer == LayerMask.NameToLayer("Land"))
                {
                    landColliders.Add(hitColliders[i]);
                }
            }
            target = landColliders[Range(0, landColliders.Count)].transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Reproduce()
    {
        Instantiate(chicken);
        reproductiveUrge = 0;
    }

    public void Eat()
    {
        foodWant = 0;
    }

    public void Drink()
    {
        waterWant = 0;
    }
}
