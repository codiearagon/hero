using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyCount = 0;

    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float health;

    private int eggCollisionCount = 0;
    private static int destroyedEnemies = 0;

    void Start()
    {
        health = maxHealth;
        enemyCount++;
        UIManager.UpdateEnemyCountText(enemyCount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamageByMaxPercent(float percent)
    {
        eggCollisionCount++;
        health -= maxHealth * (percent / 100);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a * 0.80f);

        if (health <= 0 || eggCollisionCount >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        enemyCount--;
        destroyedEnemies++;
        UIManager.UpdateEnemyCountText(enemyCount);
        UIManager.UpdateDestroyedEnemiesText(destroyedEnemies);
    }
}
