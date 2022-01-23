using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthBar : NetworkBehaviour 
{
    private Transform bar;
    private ReceiveDamage receiveDamage;

    [SyncVar]
    public int health;

    private float maxHealth;
    
    void Start()
    {
        maxHealth = health;
    }
    void Update(){
        receiveDamage = GetComponent<ReceiveDamage>();
        bar = transform.Find("Bar");
        SetSize(health / maxHealth);
    }
    public void SetSize(float sizeNormalized){
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

}
