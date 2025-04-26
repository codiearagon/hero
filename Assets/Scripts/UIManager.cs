using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI eggsCountText;
    private static TextMeshProUGUI enemyCountText;
    private static TextMeshProUGUI enemiesDestroyedText;
    private static TextMeshProUGUI heroModeText;
    private static TextMeshProUGUI waypointModeText;

    private void Awake()
    {
        eggsCountText = GameObject.Find("EggsCountText").GetComponent<TextMeshProUGUI>();
        enemyCountText = GameObject.Find("EnemyCountText").GetComponent<TextMeshProUGUI>();
        enemiesDestroyedText = GameObject.Find("EnemiesDestroyedText").GetComponent<TextMeshProUGUI>();
        heroModeText = GameObject.Find("HeroModeText").GetComponent<TextMeshProUGUI>();
        waypointModeText = GameObject.Find("WaypointModeText").GetComponent<TextMeshProUGUI>();
    }

    public static void UpdateDestroyedEnemiesText(int count)
    {
        enemiesDestroyedText.text = "Enemies Destroyed: " + count;
    }

    public static void UpdateEnemyCountText(int count)
    {
        enemyCountText.text = "Enemy Count: " + count;
    }

    public void UpdateEggsCountText(int count)
    {
        eggsCountText.text = "Eggs Count: " + count;
    }

    public static void UpdateHeroModeText(string mode)
    {
        heroModeText.text = "Hero Mode: " + mode;
    }
    
    public static void UpdateWaypointModeText(string mode)
    {
        waypointModeText.text = "Waypoint Mode: " + mode;
    }
}
