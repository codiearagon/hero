using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static TextMeshProUGUI eggsCountText;
    private static TextMeshProUGUI enemyCountText;
    private static TextMeshProUGUI enemiesDestroyedText;
    private static TextMeshProUGUI heroModeText;

    private void Awake()
    {
        eggsCountText = GameObject.Find("EggsCountText").GetComponent<TextMeshProUGUI>();
        enemyCountText = GameObject.Find("EnemyCountText").GetComponent<TextMeshProUGUI>();
        enemiesDestroyedText = GameObject.Find("EnemiesDestroyedText").GetComponent<TextMeshProUGUI>();
        heroModeText = GameObject.Find("HeroModeText").GetComponent<TextMeshProUGUI>();
    }

    public static void UpdateDestroyedEnemiesText(int count)
    {
        enemiesDestroyedText.text = "Enemies Destroyed: " + count;
    }

    public static void UpdateEnemyCountText(int count)
    {
        enemyCountText.text = "Enemy Count: " + count;
    }

    public static void UpdateEggsCountText(int count)
    {
        eggsCountText.text = "Eggs Count: " + count;
    }

    public static void updateHeroModeText(string mode)
    {
        heroModeText.text = "Hero Mode: " + mode;
    }
}
