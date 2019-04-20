using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class laser_script : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    public GameObject laserPrefab;
    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    private GameObject reticle;
    private Transform teleportReticleTransform;
    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;
    private bool shouldTeleport;

    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

   
    // Use this for initialization
    void Start () {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
    }
    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    // Update is called once per frame
    void Update () {
      
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
            RaycastHit hit;
            //int layerMask = 1 << 9;
            //layerMask = ~layerMask;
            if ((Physics.Raycast(trackedObj.transform.position + transform.forward * 0.1f, transform.forward, out hit, 20))) {
               
                if(hit.collider.tag == "teleportable" || hit.collider.tag == "shrine" || hit.collider.tag == "Player") {
                    hitPoint = hit.point;
                    ShowLaser(hit);
                    reticle.SetActive(true);
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                    shouldTeleport = true;
                }
            }
        }
        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport) {
            Teleport();
        }
    }
    private void ShowLaser(RaycastHit hit) {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }
    private void Teleport() {
        // 1
        shouldTeleport = false;
        // 2
        reticle.SetActive(false);
        // 3
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        // 4
        difference.y = 0;
        // 5
        cameraRigTransform.position = hitPoint + difference;
    }
}
