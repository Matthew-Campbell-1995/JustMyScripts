using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFireController : MonoBehaviour
{
    public float fireRate;
    public GameObject projectile;
    public Vector3 gunOffset;
    private float fireWait;
    private float timeSinceLastShot = 0f;

    // Use this for initialization
    void Start()
    {
        fireWait = 1f / fireRate;
        //gunOffset = new Vector3(0, 0.55f, 0);
    }

    // Update is called once per frame
    void Update()
    {
       timeSinceLastShot += Time.deltaTime;
       if (timeSinceLastShot > fireWait)
       { 
            GameObject shot = Instantiate(projectile);
            shot.transform.position = gameObject.transform.position + gunOffset;
            //shot.GetComponent<ShotMover>().velocity = new Vector2(0, projectileSpeed);
            timeSinceLastShot = 0f;
       }
    }

    //returns the nearest transform of gameObject with tag "Enemy"
    Transform GetNearestEnemy() 
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        if (closest == null)
        {
            return null;
        }
        return closest.transform;
    }
}

