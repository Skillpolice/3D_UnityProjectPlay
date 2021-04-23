using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Gun UI")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float range = 100f;
    [SerializeField] private float impactForce = 30f;
    [SerializeField] private float fireRotate; //частота стрельбы
    [SerializeField] private int maxAmmo;
    [SerializeField] private int maxClips;
    [SerializeField] private float reloadTime;
    [HideInInspector]
    public int currenAmmo = 0;
    private bool isreloding = false;

    [Header("Interface UI")]
    public ParticleSystem fireEffect;
    public GameObject impactEffect;
    public GameObject shootPos;
    public Camera fpsCam;
    private Animator anim;
    public Text playerAmmo;

    [Header("Audio UI")]
    [SerializeField] private AudioSource audioSource;


    float nextFire;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        currenAmmo = maxAmmo;
    }

    void Update()
    {
        playerAmmo.text = currenAmmo + " / " + maxClips.ToString();

        if (currenAmmo <= 0)
        {
            StartCoroutine(ReloadAmmo());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRotate;
            Shoot();
        }
    }

    private void Shoot()
    {
        fireEffect.Play();
        audioSource.Play();

        anim.SetTrigger("GunAttack");
        currenAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //проверяем обьекты у которых есть скрипт Dmageable
            Damageable target = hit.transform.GetComponent<Damageable>();
            if (target != null)
            {
                //если не пусто - наносим урон обьекту
                target.OnResiveDamage(damage);
            }

            //Ищем обьекты у торорых есть фозическое тело
            if (hit.rigidbody != null)
            {
                //если не пусто , то придаем силу(скорость) обьекту от позиции игрока
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //создаем эффекты попадания по предметам (1- еффект, 2-рейкаст позиция, 3-куда смотрим туда и попадаем в 3D)
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    IEnumerator ReloadAmmo()
    {
        isreloding = true;
        //anim.SetBool("Reloding", true);
        maxClips--;

        yield return new WaitForSeconds(reloadTime - .25f);

        playerAmmo.text = currenAmmo + " / " + maxClips.ToString();
        //anim.SetBool("Reloding", false);

        //yield return new WaitForSeconds(.25f);

        currenAmmo = maxAmmo;
        isreloding = false;
    }

}
