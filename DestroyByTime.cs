using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this script to a GameObject you want to die within lifespan
public class DestroyByTime : MonoBehaviour {

    public float lifespan;
    private float timealive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timealive += Time.deltaTime;
        if (timealive > lifespan)
        {
            Destroy(gameObject);
        }
	}
}
