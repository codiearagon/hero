using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyCount = 0;
    public GameObject currentWaypointDir;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float health;
    [SerializeField] private float speed;

    private int eggCollisionCount = 0;
    private static int destroyedEnemies = 0;

    void Start()
    {
        EnemySpawnManager.enemies.Add(this);

        health = maxHealth;
        enemyCount++;
        uiManager.UpdateEnemyCountText(enemyCount);

        UpdatePath(WaypointManager.GetNextDestination(currentWaypointDir));
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.name == "PlaneDetector" && collision.transform.parent.name == currentWaypointDir.name)
        {
            UpdatePath(WaypointManager.GetNextDestination(currentWaypointDir));
        }
    }

    public void UpdatePath(GameObject dir)
    {
        currentWaypointDir = dir;
        RotateTo(currentWaypointDir);
    }

    public void SetUIManager(UIManager uiM)
    {
        uiManager = uiM;
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

    private void RotateTo(GameObject direction)
    {
        Vector3 target = direction.transform.position;
        transform.up = target - transform.position;
    }

    private void OnDestroy()
    {
        enemyCount--;
        destroyedEnemies++;
        EnemySpawnManager.enemies.Remove(this);
        uiManager.UpdateEnemyCountText(enemyCount);
        uiManager.UpdateDestroyedEnemiesText(destroyedEnemies);
    }
}
