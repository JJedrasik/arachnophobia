using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller_script : MonoBehaviour {

   
    private SteamVR_TrackedObject trackedObj;
    
    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    // Update is called once per frame
    void Update () {

       
    }
}
