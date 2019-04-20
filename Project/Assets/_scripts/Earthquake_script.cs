using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
public class Earthquake_script : MonoBehaviour {
	private bool hasShook;
	private GameObject[] gos;
	// Use this for initialization
	void Start () {
		
	}

	void Awake()
	{
		gos = GameObject.FindGameObjectsWithTag("horror_text");
		hasShook = false;
		
                    foreach(GameObject go in gos)
                    {
                        go.GetComponent<Canvas>().enabled = false;
                    }
	}
	
	// Update is called once per frame
	void Update () {
		print("has it shook?" + hasShook);
		
        if(gos[0].GetComponent<Canvas>().enabled == true && hasShook == false) {
			//Make that earth shake like a bum bum bum ah 
			Vector3 originalPosition = this.transform.position;
			float elapsed = 0f;
			hasShook = true;
			while (elapsed < 1000f)
			{
				print("GO CRAZY");
				float x = Random.Range(-1f,1f) * 5;
				float y = Random.Range(-1f,1f) * 5;
				this.transform.position = new Vector3(x,y,-10f);
				elapsed += Time.deltaTime;
			}
			
			this.transform.position = originalPosition;
		}
					
	}
}
*/