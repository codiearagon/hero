using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eggsCountText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI enemiesDestroyedText;
    [SerializeField] private TextMeshProUGUI heroModeText;
    [SerializeField] private TextMeshProUGUI waypointModeText;

    public void UpdateDestroyedEnemiesText(int count)
    {
        enemiesDestroyedText.text = "Enemies Destroyed: " + count;
    }

    public void UpdateEnemyCountText(int count)
    {
        enemyCountText.text = "Enemy Count: " + count;
    }

    public void UpdateEggsCountText(int count)
    {
        eggsCountText.text = "Eggs Count: " + count;
    }

    public void UpdateHeroModeText(string mode)
    {
        heroModeText.text = "Hero Mode: " + mode;
    }
    
    public void UpdateWaypointModeText(string mode)
    {
        waypointModeText.text = "Waypoint Mode: " + mode;
    }
}
