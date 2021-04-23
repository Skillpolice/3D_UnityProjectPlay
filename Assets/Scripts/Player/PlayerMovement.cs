using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Animation), typeof(CharacterController), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public static PlayerMovement Instance { get; private set; }

    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;

    [Header("Audio UI")]
    [SerializeField] private AudioClip runAudio;
    [SerializeField] private AudioClip jumpAudio;

    [Header("Movement config")]
    [SerializeField] private float playerSpeed;


    [Header("Rotation config")]
    [SerializeField] public float rotationSpeed = 250f;
    public Camera mainCamera;

    [Header("Gravity")]
    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private float graviryScale = 0.7f;
    [SerializeField] private int ctJump = 2;
    private int currentJump;

    private float gravity;
    private bool isDeath;

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
        mainCamera = Camera.main;

        currentJump = ctJump;
    }

    private void Update()
    {
        if (isDeath)
        {
            return;
        }
        Move();
    }

    private void Move()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = mainCamera.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 moveDirection = forward * inputV + right * inputH;

        if (moveDirection.sqrMagnitude > 1)
        {
            moveDirection.Normalize();
        }

        if (Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
        {
            anim.SetBool("Running", true);

            // 1-Текущий поворот 2-Желаемый поворот
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * rotationSpeed);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playerSpeed = 12;
                }
                else
                {
                    playerSpeed = 8;
                }
            }
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if (Input.GetButtonDown("Jump") && ctJump > 0)
        {
            audioSource.PlayOneShot(jumpAudio, 0.5f);
            ctJump--;
            gravity = jumpHeight;
        }
        else if (controller.isGrounded)
        {  
            gravity = -0.1f;
            ctJump = currentJump;
           
        }
        else
        {
            gravity += graviryScale * Physics.gravity.y * Time.deltaTime;
        }

        if (gravity > 0)
        {
            anim.SetInteger("Gravity", 1);
        }
        else if (gravity < -0.1f)
        {
            anim.SetInteger("Gravity", -1);
        }
        else
        {
            anim.SetInteger("Gravity", 0);
        }

        moveDirection.y = gravity;
        controller.Move(moveDirection * playerSpeed * Time.deltaTime); //Направление движения
    }

    public void MouseSensitvityThrid(Slider slider)
    {
        rotationSpeed = slider.value;
    }
}