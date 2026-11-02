using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// World space UI to showcase test stats (health, enemy state)
/// </summary>

public class Enemy_UI : MonoBehaviour
{
    [SerializeField] private Enemy_Statistics enemyStats;
    [SerializeField] private Enemy_Grunt enemyGrunt; //mainly for testing states; remove or expand when more enemies added
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private TextMeshProUGUI enemyHealthText;
    [SerializeField] private TextMeshProUGUI enemyState;
    //[SerializeField] private RawImage regHeartImage;
    //[SerializeField] private RawImage gildedHeartImage;

    private void Update()
    {
        enemyNameText.text = this.name.ToString();
        enemyHealthText.text = enemyStats.currentHealth.ToString();
        enemyState.text = enemyGrunt.state.ToString();
    }
}
