using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class BunnyBehavior : MonoBehaviour
{
    public float waterWant;
    public float foodWant;
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
        if(transform == target)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2);
            List<Collider> list = new List<Collider>();
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].transform.gameObject.layer == LayerMask.NameToLayer("land"))
                {
                    hitColliders[i].;
                }
            }
        }
        
        //target =;
        float distance = Vector3.Distance(target.position, transform.position);
    }
}
