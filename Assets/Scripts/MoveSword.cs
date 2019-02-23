using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSword : MonoBehaviour {

    public GameObject sword;
    private float rotationY = 0;

    Quaternion rotationLast; //The value of the rotation at the previous update
    Quaternion rotationDelta; //The difference in rotation between now and the previous update

    // Use this for initialization
    void Start() {
        sword.transform.rotation = Quaternion.identity;
        rotationLast = sword.transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate() {
        // Get mouse position, compute sword orientation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -(sword.transform.position.x - Camera.main.transform.position.x);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.y = 3 - 6 * (mousePos.y / Screen.height);
        worldPos.x = 10 - 20 * (mousePos.x / Screen.width);
        sword.transform.LookAt(worldPos);
        sword.transform.rotation *= Quaternion.Euler(0, 0, 180);

        // Tilt sword based on mouse scrolls
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            rotationY += 2;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            rotationY -= 2;

        sword.transform.rotation *= Quaternion.Euler(0, 0, rotationY);

        // Compute angular velocity
        var deltaRot = sword.transform.rotation * Quaternion.Inverse(rotationLast);
        var eulerRot = new Vector3(Mathf.DeltaAngle(0, deltaRot.eulerAngles.x), Mathf.DeltaAngle(0, deltaRot.eulerAngles.y), Mathf.DeltaAngle(0, deltaRot.eulerAngles.z));
        float angularVelocity = (eulerRot / Time.fixedDeltaTime).magnitude;
        
        // Play slash animation based on velocity
        if(angularVelocity > 1000) {
            sword.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        }
        else {
            sword.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
        }

        // Update last rotation
        rotationLast = sword.transform.rotation;
    }
}
