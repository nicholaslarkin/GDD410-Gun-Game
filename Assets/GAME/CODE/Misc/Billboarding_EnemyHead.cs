using UnityEngine;

public class Billboarding_EnemyHead : MonoBehaviour
{
    private Enemy_Grunt enemy; //on Enemy_Grunt for state test, change to generic enemy type later when integrated
    private Transform camera;

    private void Start()
    {
        camera = Camera.main.transform;
        enemy = GetComponentInParent<Enemy_Grunt>();
    }

    private void Update()
    {
        if (enemy.state == GruntStates.Death)
            this.enabled = false;

        transform.LookAt(camera);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
    }
}
