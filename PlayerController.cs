using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int speed;
    public float shipBoundaryHeight = 3f;
    public float shipBoundaryWidth = 10f;
    private Vector3 deltaPosition;
    // Use this for initialization
    void Start () {
        deltaPosition = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

        //player movement logic
        
        deltaPosition.x = Input.GetAxis("Horizontal"); 
        deltaPosition.y = Input.GetAxis("Vertical");
        deltaPosition *= speed;   
        deltaPosition *= Time.deltaTime;


        //Stay on screen!
        Vector3 pos = gameObject.GetComponent<Transform>().position + deltaPosition;
       
        if (pos.y + shipBoundaryHeight > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize - shipBoundaryHeight;
        }
        //Check Vertical bounds
        if (pos.y - shipBoundaryHeight < -Camera.main.orthographicSize)
        {
            pos.y = -Camera.main.orthographicSize + shipBoundaryHeight;
        }

        //Check Horizontal bounds
        float orthoWidth = ((float)Screen.width / (float)Screen.height)* Camera.main.orthographicSize;

        if (pos.x + shipBoundaryWidth > orthoWidth)
        {
            pos.x = orthoWidth - shipBoundaryWidth;
        }

        if (pos.x - shipBoundaryWidth < -orthoWidth)
        {
            pos.x = -orthoWidth + shipBoundaryWidth;
        }

        gameObject.GetComponent<Transform>().position = pos;
    }
}
