using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class agentControl : MonoBehaviour {

   
    public GameObject partEffect;

   
    private Transform marchingOrders;
    public Transform player;
    private NavMeshAgent agent;
    private SphereCollider col;
    
    //Lets try correct movement
    private Transform sight;
    public bool canSee;
    private double range = 100.00;
    private int index;
   
    private float time = 0.0f;
    private float changeUpPeriod = 10f;
    
	// Use this for initialization
    void Awake() {
            
    }

	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("building");
        index = Random.Range (0, gos.Length);
        marchingOrders = gos[index].transform;
    }

    void Update() {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("building");
        time += Time.deltaTime;

        if (time >= changeUpPeriod)
        {
            time = time - changeUpPeriod;
            index = Random.Range (0, gos.Length);
            marchingOrders = gos[index].transform;
        }

        if (canSee == true) {
           
            agent.SetDestination(player.position);  
        }
        else {
            agent.SetDestination(marchingOrders.position);
        }
    }

    void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "weapon")
        {
            float timeToLive = 5f;
            //Instantiate Prefab of Particle Effect
            GameObject partEffectInstance = Instantiate(partEffect,transform.position,transform.rotation);
            Destroy(partEffectInstance, timeToLive);
            Destroy(this.gameObject);
            
        }
		
	}

    
}