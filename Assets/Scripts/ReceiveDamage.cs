﻿using UnityEngine;
using Mirror;

public class ReceiveDamage : NetworkBehaviour
{
    [SerializeField]
    private int maxHealth = 10;
    
    private HealthBar healthBarScript;

    [SyncVar]
    private int currentHealth;

    private float normalizedHealth;

    [SerializeField]
    private string enemyTag;
 
    [SerializeField]
    private bool destroyOnDeath;

    private Vector2 initialPosition;
    
    // Use this for initialization
    void Start ()
    {   
        currentHealth = maxHealth;
        initialPosition = transform.position;

        normalizedHealth = currentHealth/10.0f;
        
        if(isLocalPlayer){
            healthBarScript = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            healthBarScript.SetSize(normalizedHealth);
        }  
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.CompareTag(enemyTag))
        {
            TakeDamage(1);
            Destroy(collider.gameObject);
        }
    }

    void TakeDamage (int amount)
    {
        if(isServer)
        {
            currentHealth -= amount;
           

            if(currentHealth <= 0)
            {
                if(destroyOnDeath)
                {
                    Destroy(gameObject);
                }
                else
                {
                    currentHealth = maxHealth;
                    RpcRespawn();
                }
            }
        }
        if(isLocalPlayer){
            normalizedHealth = currentHealth / 10.0f;
            healthBarScript.SetSize(normalizedHealth);
        }
    }

    [ClientRpc]
    void RpcRespawn ()
    {
        if(isLocalPlayer){
            transform.position = initialPosition;
            currentHealth = maxHealth;
            normalizedHealth = currentHealth/10.0f;
            healthBarScript.SetSize(normalizedHealth);
        }
       
    }
}