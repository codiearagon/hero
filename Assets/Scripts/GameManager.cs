using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eggsCountText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI enemiesDestroyedText;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private int currentEnemies = 0;
    [SerializeField] private float xBounds;
    [SerializeField] private float yBounds;

    private int destroyedEnemies = 0;

    void Start()
    {
        // 90% of screen bounds
        xBounds = (cam.orthographicSize * Screen.width / Screen.height) * 0.9f;
        yBounds = cam.orthographicSize *  0.9f;

        enemiesDestroyedText.text = "Enemies Destroyed: " + destroyedEnemies;
    }

    void Update()
    {
        enemyCountText.text = "Enemy Count: " + currentEnemies;
        eggsCountText.text = "Eggs Count: " + Projectile.globalCount;

        if (currentEnemies < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        currentEnemies++;
        Instantiate(enemyPrefab, RandomPos(), Quaternion.identity);
    }

    Vector2 RandomPos()
    {
        return new Vector2(Random.Range(-xBounds, xBounds), Random.Range(-yBounds, yBounds));
    }

    public void ReduceEnemyCount()
    {
        destroyedEnemies++;
        enemiesDestroyedText.text = "Enemies Destroyed: " + destroyedEnemies;
        currentEnemies--;
    }
}
