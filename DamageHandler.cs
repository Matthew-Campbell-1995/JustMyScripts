using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {

    public int damageToInflict;
    public int health;

    public int salvage;

    //public GameObject explosion;

    public bool invulnerable;
    public bool friendly;
    public const int unityBoundaryLayer = 12;
    //private float secondsOfContact=0;

    // Use this for initialization
    void Start () {
		
	}

    //automatically called by unity when 2 colliders collide
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision!");
        //get the DamageHandler component from the object you crashed into
        DamageHandler otherDmgH = collision.gameObject.GetComponent<DamageHandler>();
        if (otherDmgH == null)
        {
            //ingame collisions are supposed to be between two DamageHandlers
            Debug.Log("collision not between two damageHandlers");
            return;
        }

        if (otherDmgH.gameObject.layer==unityBoundaryLayer)
        {
            //Debug.Log("Boundary collision");
            Die("Boundary");
            return;
        }
        if (friendly==otherDmgH.friendly)
        {
           // Debug.Log("Collision between allied DamageHandlers");
            return;
        }
        if (! invulnerable)
        {
            TakeDamage(otherDmgH.damageToInflict);
        }
    }

   /* private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Collision! Still!");
        DamageHandler otherDmgH = collision.gameObject.GetComponent<DamageHandler>();
        if (otherDmgH == null)
        {
            Debug.Log("collision not between two damageHandlers");
            return;
        }
        if (!(invulnerable || otherDmgH.invulnerable))
            {
            gameObject.GetComponent<DamageHandler>().TakeDamage(otherDmgH.damageToInflict);
            }
    }*/

    public virtual void TakeDamage(int incomingDamage)
    {      
            health -= incomingDamage;
            if (health <= 0)
            {
                //Explode();
                Die();
            }
    }

    /*void Explode()
    {
       GameObject ex = Instantiate(explosion);
       ex.transform.position = gameObject.transform.position;
    } */

    void Die()
    {
        Die("unknown");
    }

    public virtual void Die(string cause)
    {
        if (cause!="Boundary") //enemies killed by the boundary don't give salvage
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<DamageHandler>().salvage += salvage;
        }
        Destroy(gameObject);
    }
    // Update is called once per frame

    
    void Update () {
		
	}
}
