using UnityEngine;

public class Teleport : MonoBehaviour {
    //Idea from here https://scholarslab.lib.virginia.edu/blog/teleporting-in-Unity3D/
    public Transform teleportTarget;
    public GameObject thePlayer;
    public Camera previousCam;
    public Camera nextCam;

    private void OnTriggerEnter2D(Collider2D other) {
        thePlayer.transform.position = teleportTarget.transform.position;
        nextCam.enabled = true;
        previousCam.enabled = false;
    }
}
