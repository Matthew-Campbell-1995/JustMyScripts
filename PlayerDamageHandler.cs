using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageHandler : DamageHandler
{
    //Texts are Unity objects that appear as textboxes while the scene is running
    public Text HPDisplay;
    public Text SPDisplay;
    public Text SalDisplay;
    public Text OSDisplay;

    public GameObject shieldGraphic;

    public bool shieldUp = true;

    public float shieldRechargeRate = 0.05f;

    public float maxShieldHealth = 5f;
    public float shieldHealth = 5f;

    public bool shieldInvul = false;

    public float OScooldown;
    private float timeSinceOSUse;
    public float OSduration;
    public int OSCountdown = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceOSUse += Time.deltaTime;

        //Overshield ability cooldown/activation
        if (Input.GetKeyDown("9") && timeSinceOSUse >= OScooldown)
        {
            shieldInvul = true;
            shieldUp = true;
            shieldGraphic.GetComponent<SpriteRenderer>().color = new Color(255, 0, 255, 255);
            timeSinceOSUse = 0;
        }

        if (timeSinceOSUse >= OSduration)
        {
            shieldInvul = false;
            shieldUp = false;
            shieldGraphic.GetComponent<SpriteRenderer>().color = Color.white;
        }


        shieldHealth += Time.deltaTime * shieldRechargeRate;
        if (shieldHealth > maxShieldHealth)
        {
            shieldHealth = maxShieldHealth;
        }
        if (shieldHealth >= 1)
        {
            shieldUp = true;
        }

        UpdateShieldDisplay();
        UpdateSalvageDisplay();
        UpdateOSDisplay();
    }

    public override void TakeDamage(int incomingDamage)
    {
        if (! shieldUp)
        {
            base.TakeDamage(incomingDamage);
            UpdateHPDisplay();
            return;
        }

        else if (! shieldInvul)
        { 
            shieldHealth -= incomingDamage;
        }
        if (shieldHealth <= 0)
        {
            shieldHealth = 0;
            shieldUp = false;
        }

        UpdateShieldDisplay();
    }

    public override void Die(string cause)
    {
        base.Die(cause);
        //gameOver();
    }

    void UpdateHPDisplay()
    {
        HPDisplay.text = "Hull: " + ((int)health).ToString();
    }

    void UpdateShieldDisplay()
    {
        SPDisplay.text = "Shield: " + ((int)shieldHealth).ToString();
        shieldGraphic.SetActive(shieldUp);
    }

    void UpdateSalvageDisplay()
    {
        SalDisplay.text = "Salvage: " + salvage.ToString();
    }

    void UpdateOSDisplay()
    {
        OSCountdown = (int)(OScooldown - timeSinceOSUse);
        if (OSCountdown < 0)
        {
            OSCountdown = 0;
        }

        OSDisplay.text = "Overshield cooldown: " +  OSCountdown.ToString();
    }
}
