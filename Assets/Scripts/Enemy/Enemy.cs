using System.Collections;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(Damageable), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    BoxCollider coll;

    private Animator anim;
    private Damageable damageable;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip deathClip;
    public AudioClip hitClip;

    [Header("Enemy UI")]
    public ParticleSystem deathEffect;
    public GameObject healthPrefab;
    public GameObject shootPosEnemy;
    public GameObject bullet;


    [Header("Enemy UI")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float healthEnemy = 100;


    Vector3 targetPosition;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        coll = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        damageable.OnResiveDamage += ResiveDamage; //Action Damageable который принемает действ метода ResiveDamage(int damage)
        healthBar.SetMaxHealth(healthEnemy);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RotateEnemy();
      
    }

    //private void DamageToPlayer()
    //{
    //    Vector3 shootToPlayer = Player.Instance.transform.position - transform.position;

    //    
    //GameObject tempBullet = Instantiate(bullet, shootPosEnemy.transform.position, transform.rotation) as GameObject;
    //    Rigidbody tempRBBullet = tempBullet.GetComponent<Rigidbody>();
    //    tempRBBullet.AddForce(tempRBBullet.transform.forward * speedBullet);

    //    Destroy(tempBullet, 10f);
    //}


    private void DamageToPlayer()
    {
        Instantiate(bullet, shootPosEnemy.transform.position, transform.rotation);
        audioSource.PlayOneShot(attackClip);
    }


    private void ResiveDamage(float damage)
    {
        healthEnemy -= damage;

        healthBar.Sethealth(healthEnemy);
        audioSource.PlayOneShot(hitClip, 0.5f);

        anim.SetTrigger("EnemyHit");

        if (healthEnemy <= 0)
        {

            audioSource.PlayOneShot(deathClip);
            coll.enabled = false;
            anim.SetTrigger("EnemyDeath");

            Destroy(gameObject, 5f);

            Instantiate(healthPrefab, transform.position * 1f, Quaternion.identity);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

    }

    private void RotateEnemy()
    {
        if (healthEnemy > 0)
        {
            Vector3 direction = Player.Instance.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 5f * Time.deltaTime);

            //Vector3 targetPosition = Player.Instance.transform.position - transform.position;
            //Quaternion rotation = Quaternion.LookRotation(targetPosition, Vector3.up);
            //transform.rotation = rotation;
        }
        else
        {
            return;
        }

    }


}