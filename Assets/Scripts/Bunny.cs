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
    public NavMeshAgent agent;
    Transform target;
    

    void Start()
    {
        //target = ;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        
        foodWant = Time.deltaTime * 3;
        waterWant = Time.deltaTime * 5;
        //if(transform == target)
        //{
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, lookRadius);
            List<Collider> landColliders = new List<Collider>();
            for (int i = 0; i < hitColliders.Length; i++)
            {
                //Debug.Log(hitColliders[i].transform.name);
                if (hitColliders[i].transform.gameObject.layer == LayerMask.NameToLayer("Land"))
                {
                    landColliders.Add(hitColliders[i]);
                }
            }

            Debug.Log(landColliders.Count);
            //Random random = new Random();
        //}
        
        //target =;
        //float distance = Vector3.Distance(target.position, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
