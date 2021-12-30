using UnityEngine;
using Mirror;

public class ReceiveDamage : NetworkBehaviour
{
    [SerializeField]
    private int maxHealth = 10;

    [SyncVar]
    private int currentHealth;

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
    }

    [ClientRpc]
    void RpcRespawn ()
    {
        transform.position = initialPosition;
    }
}