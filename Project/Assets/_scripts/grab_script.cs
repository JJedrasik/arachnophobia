using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab_script : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;
    public bool hasBook = false;
    
    public GameObject fireball;
    private float speed = 3;
   

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }


    

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("horror_text");
                    foreach(GameObject go in gos)
                    {
                        go.GetComponent<Canvas>().enabled = false;
                    }
    }

    private void SetCollidingObject(Collider col) {
        if (collidingObject || !col.GetComponent<Rigidbody>()) {
            return;
        }
        collidingObject = col.gameObject;
    }


    void Update () {
        
        if (Controller.GetHairTriggerDown()) {
            if (collidingObject != null) {
                GrabObject();
            } else {
                
                GameObject ball = Instantiate(fireball, transform.GetChild(0).transform.position, Quaternion.identity);
                fireball.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).transform.forward * 100000);
                Destroy(ball, 5f);
            }
        }

        
        if (Controller.GetHairTriggerUp()) {
            if (objectInHand) {
                ReleaseObject();
            }
        }
    }

    public void OnTriggerEnter(Collider other) {
        SetCollidingObject(other);
        if(other.tag == "shrine" && hasBook == true) {
            print("Game is over");
        }
    }

    public void OnTriggerStay(Collider other) {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other) {
        if (!collidingObject) {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject() {
        if(collidingObject.tag == "magic_book")
                {
          
                this.transform.parent.GetComponent<player_script>().hasBook = true;
                Destroy(collidingObject);
                    
                    GameObject[] gos = GameObject.FindGameObjectsWithTag("horror_text");
                    foreach(GameObject go in gos)
                    {
                        go.GetComponent<Canvas>().enabled = true;
                    }
                }
        objectInHand = collidingObject;
        collidingObject = null;
   
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    
 
    private FixedJoint AddFixedJoint() {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject() {
        // 1
        if (GetComponent<FixedJoint>()) {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        // 4
        objectInHand = null;
    }


}
