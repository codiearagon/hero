using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static List<Enemy> enemies = new List<Enemy>();

    [SerializeField] private UIManager uiManager;
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
        if (Enemy.enemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, RandomPos(), Quaternion.identity);
        enemy.GetComponent<Enemy>().SetUIManager(uiManager);
    }

    Vector2 RandomPos()
    {
        return new Vector2(Random.Range(-xBounds, xBounds), Random.Range(-yBounds, yBounds));
    }
}
