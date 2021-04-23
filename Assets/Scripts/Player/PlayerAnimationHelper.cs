using UnityEngine;

public class PlayerAnimationHelper : MonoBehaviour
{
    private Animator anim;

    [Header("Audio UI")]
    [SerializeField] private AudioSource RunAudioSource;
    [SerializeField] private AudioClip landingAudio;
    [SerializeField] private AudioClip staffComboAttack;
    [SerializeField] private AudioClip staffAttack;

    [SerializeField] private Weapon weapon;

    private bool checkCombo;
    private bool isAttacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
        RunAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (isAttacking)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (checkCombo)
            {
                anim.SetTrigger("ComboAttack");
                RunAudioSource.PlayOneShot(staffComboAttack, 0.7f);
            }
            else
            {
                anim.SetTrigger("Attack");
                RunAudioSource.PlayOneShot(staffAttack, 0.7f);
            }
        }
    }

    public void MeleeAttackStart()
    {
        isAttacking = true;
        weapon.AttackStart();
    }

    public void MeleeAttackEnd()
    {
        isAttacking = false;
        weapon.AttackEnd();

        anim.ResetTrigger("Attack");
    }

    public void ComboStart()
    {
        checkCombo = true;
    }

    public void ComboEnd()
    {
        checkCombo = false;
    }

    public void RunStartAudio()
    {
        RunAudioSource.Play();
    }

    public void RunStopAudio()
    {
        RunAudioSource.Stop();
    }

    public void RunEndAudio()
    {
        RunAudioSource.Play();
    }


    public void Landing()
    {
        RunAudioSource.PlayOneShot(landingAudio, 0.5f);
    }

}