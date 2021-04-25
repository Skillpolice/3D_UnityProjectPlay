using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Damageable), typeof(Animator))]
public class EnemyBossHealth : MonoBehaviour
{
    Enemy enemy;
    Damageable damageable;

    public GameObject panel;

    public static int bossCount = 2;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        damageable = GetComponent<Damageable>();

        damageable.OnResiveBossDamage += OnResiveBossDamage;
    }

    public void OnResiveBossDamage(float damage)
    {
        enemy.healthEnemy -= damage;

        print("Damage");

        if (enemy.healthEnemy <= 0)
        {
            bossCount--;
            if (bossCount <= 0)
            {
                print("game over");
                SceneManager.LoadScene(1);
            }

        }
    }



}
