using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGun : MonoBehaviour
{
    public int fireRate;
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
            shot.transform.rotation = gameObject.transform.rotation;
            //shot.GetComponent<ShotMover>().velocity = new Vector2(0, projectileSpeed);
            timeSinceLastShot = 0f;
        }
    }
}