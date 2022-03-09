using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHoming : MonoBehaviour
//WARNING: Will not play well with built-in physics. In particular, velocity
{

    public Transform target;
    public float speed = 10;
    public float accelerationRate = 1;
    public bool retargetable = false;
    //public float rotSpeed = 90;

    // Use this for initialization
    void Start()
    {
        //most things with homing will be homing in on the player
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        //if something else killed the target, find a new one
        if (target == null)
        {
            if (!retargetable)
            {
                Destroy(gameObject);
                return;
            }

            else
            {
                target = GetNearestEnemy();
                if (target == null)
                {
                    Destroy(gameObject);
                    return;
                }
            }       
        }

        //faces the target
        Vector3 dir = target.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);
        transform.rotation = desiredRot;

        //moves forward, towards the target
        Vector3 moveTowardsTarget = new Vector3(0, Time.deltaTime * speed, 0);
        moveTowardsTarget = transform.rotation * moveTowardsTarget;

        transform.position += moveTowardsTarget;

        speed += Time.deltaTime * accelerationRate;

    }

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
