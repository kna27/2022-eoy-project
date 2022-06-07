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
    //public NavMeshAgent agent;
    Transform target;
    List<Collider> landColliders = new List<Collider>();


    void Start()
    {
        target = transform; // Set the target equal to the transform so that it generates a new one
        //agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        
        foodWant = Time.deltaTime * 3;
        waterWant = Time.deltaTime * 5;
        if (transform.position != target.position) // When the transform is not equal to the target, move to it
        {
            transform.position = target.position;
            Debug.Log(transform == target); // HOW IS THIS FALSE BUT THE TRASNFORM.POSITION EQUAL EACHOTHER????
            List<Collider> landColliders = new List<Collider>();
        }
        else // When the transform is equal to the target, generate a new random target based on the tiles
        {
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, lookRadius);
            //List<Collider> landColliders = new List<Collider>();
            for (int i = 0; i < hitColliders.Length; i++)
            {
                //Debug.Log(hitColliders[i].transform.name);
                if (hitColliders[i].transform.gameObject.layer == LayerMask.NameToLayer("Land"))
                {
                    landColliders.Add(hitColliders[i]);
                    //Debug.Log(hitColliders[i]);
                }
            }

            //Debug.Log(landColliders.Count);
            //Debug.Log(transform.position);
            //Debug.Log(target.position);
            target = landColliders[Random.Range(0, landColliders.Count)].transform;
            
        }
        
        
        //float distance = Vector3.Distance(target.position, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
