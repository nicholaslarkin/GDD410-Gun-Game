using TMPro;
using UnityEngine;

public class Enemy_UI : MonoBehaviour
{
    [SerializeField] private Enemy_Statistics enemyStats;
    [SerializeField] private TextMeshProUGUI enemyHealthText;

    private void Update()
    {
        enemyHealthText.text = enemyStats.currentHealth.ToString();
    }
}
