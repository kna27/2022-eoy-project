using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using static UnityEngine.Random;
using UnityEngine.AI;

public class Fox : MonoBehaviour
{
    enum States
    {
        IDLE = 0,
        LOOKINGFORWATER = 1,
        LOOKINGFORFOOD = 2,
        DRINKING = 3,
        EATING = 4,
        REPRODUCING = 5,
        DYING = 6
    }

    public float waterWant;
    public float foodWant;
    public float reproductiveUrge;
    public float lookRadius = 10f;
    public float stoppingDistance = 5f;
    public float age;
    public GameObject fox;
    public string status;
    private int state;
    Transform target;
    NavMeshAgent agent;
    List<Collider> landColliders = new List<Collider>();
    Collider[] hitColliders;

    bool waterFound;
    bool foodFound;
    public int maxAge = 50;


    void Start()
    {
        waterWant = Range(1, 10);
        foodWant = Range(1, 10);
        reproductiveUrge = Range(0, 10);
        age = 0;
        target = transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        foodWant += Time.deltaTime * 2.5f;
        waterWant += Time.deltaTime * 4.5f;
        reproductiveUrge += foodWant > 40 ? Time.deltaTime * -1f : Time.deltaTime * 1.5f;
        age += Time.deltaTime / 2.5f;
        float size = 0.4f + (1.5f - 0.4f) * (age / maxAge);
        transform.localScale = new Vector3(size, size, size);

        foodWant = Mathf.Clamp(foodWant, 0, 100);
        waterWant = Mathf.Clamp(waterWant, 0, 100);
        reproductiveUrge = Mathf.Clamp(reproductiveUrge, 0, 100);

        if (foodWant >= 100 || waterWant >= 100 || age > maxAge)
        {
            state = (int)States.DYING;
        }
        else if (waterFound)
        {
            state = (int)States.DRINKING;
        }
        else if (foodFound)
        {
            state = (int)States.EATING;
        }
        else if (waterWant > 50)
        {
            state = (int)States.LOOKINGFORWATER;
        }
        else if (foodWant > 50)
        {
            state = (int)States.LOOKINGFORFOOD;
        }
        else if (reproductiveUrge > 70)
        {
            state = (int)States.REPRODUCING;
        }
        else
        {
            state = (int)States.IDLE;
        }
        hitColliders = Physics.OverlapSphere(transform.position, lookRadius);
        switch (state)
        {
            case (int)States.IDLE:
                status = "Idle";
                landColliders.Clear();
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].transform.gameObject.layer == LayerMask.NameToLayer("Land") || hitColliders[i].transform.gameObject.layer == LayerMask.NameToLayer("Sand"))
                    {
                        landColliders.Add(hitColliders[i]);
                    }
                }
                target = landColliders[Range(0, landColliders.Count)].transform;
                break;
            case (int)States.LOOKINGFORWATER:
                status = "Looking for water";
                Collider[] touchingCol = Physics.OverlapSphere(transform.position, 1);
                for (int i = 0; i < touchingCol.Length; i++)
                {
                    if (touchingCol[i].transform.gameObject.layer == LayerMask.NameToLayer("Sand"))
                    {
                        waterFound = true;
                        break;
                    }
                }
                landColliders.Clear();
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].transform.gameObject.layer == LayerMask.NameToLayer("Sand"))
                    {
                        landColliders.Add(hitColliders[i]);
                    }
                }
                target = landColliders[Range(0, landColliders.Count)].transform;
                break;
            case (int)States.LOOKINGFORFOOD:
                status = "Looking for food";
                Collider[] touchingCols = Physics.OverlapSphere(transform.position, 1);
                for (int i = 0; i < touchingCols.Length; i++)
                {
                    if (touchingCols[i].transform.gameObject.tag == "Food")
                    {
                        foodFound = true;
                        break;
                    }
                }
                landColliders.Clear();
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].transform.gameObject.tag == "Food")
                    {
                        landColliders.Add(hitColliders[i]);
                    }
                }
                target = landColliders[Range(0, landColliders.Count)].transform;
                break;
            case (int)States.DRINKING:
                status = "Drinking";
                StartCoroutine(Drink());
                break;
            case (int)States.EATING:
                status = "Eating";
                StartCoroutine(Eat());
                break;
            case (int)States.REPRODUCING:
                status = "Reproducing";
                Reproduce();
                break;
            case (int)States.DYING:
                status = "Dying";
                Die();
                break;
        }
        if (target != null && agent != null)
        {
            agent.destination = target.position;
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
        Instantiate(fox, transform.position, Quaternion.identity);
        reproductiveUrge = 0;
    }

    IEnumerator Drink()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        waterWant = 0;
        waterFound = false;
        agent.isStopped = false;

    }
    IEnumerator Eat()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        Collider[] touchingCols = Physics.OverlapSphere(transform.position, 1);
        for (int i = 0; i < touchingCols.Length; i++)
        {
            if (touchingCols[i].transform.gameObject.CompareTag("Food"))
            {
                Destroy(touchingCols[i].gameObject);
            }
        }
        foodWant = 0;
        foodFound = false;
        agent.isStopped = false;
    }
}
