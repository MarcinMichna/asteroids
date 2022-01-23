using UnityEngine;
using Mirror;

public class HealthBar : NetworkBehaviour
{
    private Transform bar;
    private ReceiveDamage receiveDamage;

    private ScoreController scoreController;
    private SpawnEnemies enemySpawner;

    [SyncVar] public float health;

    private float maxHealth;

    void Start()
    {
        if (isServer)
        {
            enemySpawner = GameObject.Find("EnemySpawner").GetComponent<SpawnEnemies>();
        }

        maxHealth = health;
        scoreController = GameObject.Find("Score").GetComponent<ScoreController>();
    }

    void Update()
    {
        receiveDamage = GetComponent<ReceiveDamage>();
        bar = transform.Find("Bar");
        SetSize(health / maxHealth);

        if (isServer && health <= 0)
        {
            
            health = maxHealth;
            scoreController.handleDeath();
            enemySpawner.enemySpeed = 1.0f;

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
        }

        
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}