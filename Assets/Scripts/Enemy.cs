using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float health;

    private int eggCollisionCount = 0;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.ReduceEnemyCount();
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
            gameManager.ReduceEnemyCount();
            Destroy(gameObject);
        }
    }
}
