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
    public float stoppingDistance = 5f; // Do later, will draw out a sphere from the target and check if the transform is in it??? Or just check if transform is in target + or - stopping distance 
    public float age;
    //public NavMeshAgent agent;
    public GameObject chicken;
    Transform target;
    List<Collider> landColliders = new List<Collider>();


    void Start()
    {
        waterWant = Range(1, 10);
        foodWant = Range(1, 10);
        reproductiveUrge = Range(10, 30);
        target = transform; // Set the target equal to the transform so that it generates a new one
        //agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        
        foodWant += Time.deltaTime * 2.5f;
        waterWant += Time.deltaTime * 4.5f;
        reproductiveUrge += foodWant > 40 ? Time.deltaTime * -1f : Time.deltaTime * 1.5f;

        foodWant = Mathf.Clamp(foodWant, 0, 100);
        waterWant = Mathf.Clamp(foodWant, 0, 100);
        reproductiveUrge = Mathf.Clamp(reproductiveUrge, 0, 100);

        if (foodWant >= 100 || waterWant >= 100)
        {
            Die();
        }

        if (waterWant > 50)
        {
          // Drink();
        }
        else if (foodWant > 50)
        {
          // Eat();
        } else if (reproductiveUrge > 70)
        {
            Reproduce();
        }

        if (transform.position != target.position)
        {
            transform.position = target.position;
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
        
        
        //float distance = Vector3.Distance(target.position, transform.position);
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
        //Instantiate(chicken);
    }
}
