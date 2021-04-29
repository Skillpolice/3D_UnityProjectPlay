using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void Start()
    {
        panel.SetActive(false);
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
                Time.timeScale = 0;
                panel.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
            }

        }
    }



}
