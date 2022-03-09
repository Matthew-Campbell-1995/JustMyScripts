using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMover : MonoBehaviour {

    public Vector3 velocity;
    private Vector3 moveTowardsTarget;
    // Use this for initialization
    void Start () {
        moveTowardsTarget = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        moveTowardsTarget = velocity * Time.deltaTime;

        //rotation must be multiplied by vector3, rather than the other way
        moveTowardsTarget = transform.rotation * moveTowardsTarget;

        transform.position += moveTowardsTarget;
    }
}
