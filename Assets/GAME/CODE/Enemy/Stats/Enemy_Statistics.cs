using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Holds base enemy attributes that all enemies use (derived classes use specific enemy behavior)
/// </summary>

//
// TO DO: Transfer generic state changes into here outside of Enemy_Grunt to Enemy_Statistics
//
public class Enemy_Statistics : ActorStatistics
{
    [Header("Enemy-Specifics")]
    protected bool _damageFlashing;

    [SerializeField] protected Renderer currentMaterial;
    [SerializeField] protected Material idleColor;
    [SerializeField] protected Material spottedColor;
    [SerializeField] protected Material aggressiveColor;
    [SerializeField] protected Material damagedColor;
    [SerializeField] protected Material deathColor;
    [SerializeField] protected Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        currentMaterial = GetComponentInChildren<Renderer>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(int amount)
    {
        if (currentHealth <= 0)
            return;

        base.TakeDamage(amount);

        Material lastColor = currentMaterial.material;
        currentMaterial.material = damagedColor;
        _damageFlashing = true;

        StartCoroutine(ResetMaterial(lastColor));
    }

    protected virtual IEnumerator ResetMaterial(Material lastColor)
    {
        yield return new WaitForSeconds(0.1f);

        _damageFlashing = false;
        currentMaterial.material = lastColor;
    }

    protected virtual void ShootingPlayer()
    {

    }

    public override void Death()
    {
        currentMaterial.material = deathColor;
        return;
    }
}
