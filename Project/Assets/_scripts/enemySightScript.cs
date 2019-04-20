using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySightScript : MonoBehaviour {

    public Transform playerPosition;
    private Transform lastSeenPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other) {
        
        if(other.tag == "Player" || other.tag == "Weapon") {    
            transform.parent.GetComponent<agentControl>().canSee = true;
        }
    }

    void OnTriggerExit(Collider other) {
        transform.parent.GetComponent<agentControl>().canSee = false;

    }
}
