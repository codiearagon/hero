using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float xBounds;
    [SerializeField] private float yBounds;

    //private int destroyedEnemies = 0;

    void Start()
    {
        // 90% of screen bounds
        xBounds = (cam.orthographicSize * Screen.width / Screen.height) * 0.9f;
        yBounds = cam.orthographicSize *  0.9f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Enemy.enemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, RandomPos(), Quaternion.identity);
    }

    Vector2 RandomPos()
    {
        return new Vector2(Random.Range(-xBounds, xBounds), Random.Range(-yBounds, yBounds));
    }
}
