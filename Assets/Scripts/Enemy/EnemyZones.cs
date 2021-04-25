using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Lean.Pool;

[RequireComponent(typeof(Damageable), typeof(NavMeshAgent))]
public class EnemyZones : MonoBehaviour
{
    Animator anim;
    EnemyPatrol partol;
    EnemyZones zones;
    Enemy rotate;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip meleeAttackClip;
    public AudioClip StepClip;

    [Header("NavMesh")]
    public NavMeshAgent agent;
    public GameObject thePlayer;

    [Header("Zones UI")]
    //[SerializeField] private float rangeAttack = 5f;
    [SerializeField] private float meleeAttack = 5f;
    [SerializeField] private float attackRadius = 4f;
    [SerializeField] private float moveRadius = 10f;

    [Header("Attack UI")]
    [SerializeField] private float hitDamage = 3f;
    [SerializeField] private float hitRangeRotate;
    [SerializeField] private float hitMeleeRotate;
    float hitNexAttack;

    float distanceToPlayer;

    Vector3 startPos;

    EnemyState activeState;

    enum EnemyState //проверка состояний зомби
    {
        STAND,
        MOVE_TO_PLAYER,
        ATTACK,
        MELEEATTACK
    }
    private void Awake()
    {
        zones = GetComponent<EnemyZones>();
        rotate = GetComponent<Enemy>();
        partol = GetComponent<EnemyPatrol>();
    }

    private void Start()
    {
        startPos = transform.position;
        activeState = EnemyState.STAND;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        DistanceEnemy();
    }

    private void DistanceEnemy()
    {
        distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);

        switch (activeState)
        {
            case EnemyState.STAND:
                DoStand();
                break;
            case EnemyState.MOVE_TO_PLAYER:
                DoMove();
                break;
            case EnemyState.ATTACK:
                DoAttack();
                break;
            case EnemyState.MELEEATTACK:
                DoMeleeAttack();
                break;
        }

    }


    private void DoStand()
    {
        if (distanceToPlayer > moveRadius)
        {
            //zones.enabled = false;
            rotate.enabled = false;
            activeState = EnemyState.STAND;
        }
        if (distanceToPlayer < moveRadius)
        {
            anim.SetFloat("EnemyRun", 1);

            agent.SetDestination(thePlayer.transform.position);

            partol.enabled = false;
            zones.enabled = true;
            rotate.enabled = true;
            activeState = EnemyState.MOVE_TO_PLAYER;
            return;
        }
    }

    private void DoMove()
    {
        if (distanceToPlayer < attackRadius)
        {
            anim.SetFloat("EnemyRun", 0);

            activeState = EnemyState.ATTACK;
            return;
        }
        else if (distanceToPlayer < meleeAttack)
        {
            anim.SetFloat("EnemyRun", 0);

            activeState = EnemyState.MELEEATTACK;
            return;
        }

        agent.isStopped = true;

    }

    private void DoAttack()
    {
        if (distanceToPlayer > attackRadius && distanceToPlayer > meleeAttack)
        {
            anim.SetFloat("EnemyRun", 1);
            anim.SetBool("EnemyRangeAttack", false);
            activeState = EnemyState.MOVE_TO_PLAYER;
            return;
        }

        hitNexAttack -= Time.deltaTime;
        if (hitNexAttack < 0)
        {
            anim.SetBool("EnemyRangeAttack", true);
            hitNexAttack = hitRangeRotate;
        }


        if (distanceToPlayer < attackRadius && distanceToPlayer < meleeAttack)
        {
            anim.SetBool("EnemyRangeAttack", false);
            activeState = EnemyState.MELEEATTACK;
            return;
        }


    }

    private void DoMeleeAttack()
    {
        if (distanceToPlayer > meleeAttack)
        {
            anim.SetFloat("EnemyRun", 0);
            activeState = EnemyState.ATTACK;
            return;
        }

        hitNexAttack -= Time.deltaTime;
        if (hitNexAttack < 0)
        {
            anim.SetTrigger("EnemyMeleeAttack");
            hitNexAttack = hitMeleeRotate;
        }

    }


    private void MeleeAttack()
    {
        if (distanceToPlayer > attackRadius && distanceToPlayer > meleeAttack)
        {
            return;
        }

        Player.Instance.DoDamage(hitDamage);
    }

    private void MeleeAttackClip()
    {
        audioSource.PlayOneShot(meleeAttackClip, 0.7f);
    }
    private void PlayStep()
    {
        audioSource.PlayOneShot(StepClip, 0.7f);
    }



    //private void DamageToPlayer()
    //{
    //    RaycastHit hit;
    //    //сам луч, начинается от позиции этого объекта и направлен в сторону цели
    //    Ray ray = new Ray(shootPosEnemy.transform.position, thePlayer.transform.position - transform.position);
    //    //пускаем луч
    //    Physics.Raycast(ray, out hit);

    //    //если луч с чем-то пересёкся, то..
    //    if (hit.collider != null)
    //    {
    //        //если луч не попал в цель
    //        if (hit.collider.gameObject != thePlayer.gameObject)
    //        {
    //            Debug.Log("Путь к врагу преграждает объект: " + hit.collider.name);
    //        }
    //        //если луч попал в цель
    //        else
    //        {
    //            Debug.Log("Попадаю во врага!!!");
    //            if (distanceToPlayer > attackRadius)
    //            {
    //                return;
    //            }
    //            Player.Instance.DoDamage(damageEnemy);

    //        }
    //        //просто для наглядности рисуем луч в окне Scene
    //        Debug.DrawLine(ray.origin, hit.point, Color.red);
    //    }
    //}


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, attackRadius);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, moveRadius);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, meleeAttack);

    //    //Gizmos.color = Color.cyan;
    //    //Gizmos.DrawWireSphere(transform.position, rangeAttack);

    //}

}