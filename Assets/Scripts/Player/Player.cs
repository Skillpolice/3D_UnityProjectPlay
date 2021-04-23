using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public static Player Instance { get; private set; } //сингилтон
    public Animator anim;

    [Header("Audio UI")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathAudio;
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private AudioClip healthAudio;

    [Header("Player Health")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] public float playerHealth = 100f;
    [SerializeField] private float upHealth = 2f;
    [SerializeField] private float downHealth = 3f;

    [Header("Resitting Time Health")]
    [SerializeField] private float upTimeHealt = 1f;
    [SerializeField] private float downTimeHealt = 1f;

    [Header("Visual Effects")]
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private Transform cameraFollow;


    private bool isResetting;
    private float damage;


    public Vector3 startPos;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //anim = GetComponent<Animator>();
        healthBar.SetMaxHealth(playerHealth);

        Cursor.lockState = CursorLockMode.Locked; //Лочим курсор в Unity при заапуске игры
    }

    private void Update()
    {
        if (isResetting) //Не получать урон дважды
        {
            return;
        }
    }

    public void StartUpHealth()
    {
        StopAllCoroutines();
        StartCoroutine(IUpHealth(damage));
    }

    public void StartDownHealth()
    {
        StopAllCoroutines();
        StartCoroutine(IDownHealth(damage));
    }

    private IEnumerator IUpHealth(float amount)
    {
        while (playerHealth <= 100)
        {
            yield return new WaitForSeconds(upTimeHealt);

            playerHealth += upHealth;

            playerHealth -= amount;
            healthBar.Sethealth(playerHealth);
            yield return null;
        }
    }

    private IEnumerator IDownHealth(float amount)
    {
        //yield return new WaitForSeconds(1f);

        while (playerHealth >= 0)
        {
            yield return new WaitForSeconds(downTimeHealt);
            playerHealth -= amount;
            playerHealth -= downHealth;
            healthBar.Sethealth(playerHealth);

            yield return null;
        }

        DoDamage(damage);
    }

    public void UpHealthPlayer(float amountUp)
    {
        playerHealth += amountUp;
        audioSource.PlayOneShot(healthAudio, 0.5f);
    }

    public void DoDamage(float amount)
    {
        playerHealth -= amount;
        audioSource.PlayOneShot(hitAudio, 0.5f);

        if (playerHealth <= 0)
        {

            if (isResetting)
            {
                audioSource.PlayOneShot(deathAudio, 0.5f);
                return;
            }

            isResetting = true;

            Instantiate(deathEffect, cameraFollow.position, Quaternion.identity);
            anim.SetTrigger("Death");

            Vector3 spawnEffectPos = startPos;
            RaycastHit hit;
            if (Physics.Raycast(startPos, Vector3.down, out hit))
            {
                spawnEffectPos = hit.point;
            }


            Instantiate(spawnEffect, spawnEffectPos, Quaternion.identity);
            //transform.DOMove(startPos, 1f).SetDelay(1f).OnComplete(
            //    () =>
            //    {
            //        isResetting = false;
            //    }); //Летим на стратовую позицию

            transform.position = startPos;
            isResetting = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (hit.gameObject.CompareTag("Res"))
        {
            if (rb == null || rb.isKinematic)
            {
                //transform.DOMove(startPos, 1.5f).OnComplete(() => { isResetting = false; });
                transform.position = startPos;
                isResetting = false;
            }
        }
    }
}