using System;
using UnityEngine;
using UnityEngine.UI;

public class LookingFirstPerson : MonoBehaviour
{
    Animator anim;

    [SerializeField] public static LookingFirstPerson Instance { get; private set; }

    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private AudioSource audioSource;

    [Header("Audio UI")]
    [SerializeField] private AudioClip runAudio;
    [SerializeField] private AudioClip jumpAudio;

    [Header("Movement config")]
    [SerializeField] private float playerSpeed = 10f;

    [Header("Rotation config")]
    [SerializeField] Transform mainCamera;
    [SerializeField] public float mouseSensivity = 250f;
    [SerializeField] float maxVerticalAngle = 90f;
    float xRotation = 70f;

    [Header("Gravity")]
    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private float graviryScale = 0.7f;
    [SerializeField] private int ctJump = 2;
    private int currentJump;


    private bool isResetting;
    private float gravity;
    private Vector3 startPos;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        currentJump = ctJump;

        startPos = transform.position;
    }


    void Update()
    {
        if (isResetting)
        {
            return;
        }
        Move();
        Rotation();
    }

    private void Move()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * inputV + transform.right * inputH;  //Движение по вертикали вперед Forward и впаро Right

        if (Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
        {
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

        if (moveDirection.sqrMagnitude > 1)
        {
            moveDirection.Normalize();
        }

        if (Input.GetButtonDown("Jump") && ctJump > 0)
        {
            audioSource.PlayOneShot(jumpAudio, 0.5f);
            ctJump--;
            gravity = jumpHeight;
        }
        else if (controller.isGrounded)
        {
            gravity = 0;
            ctJump = currentJump;
        }
        else
        {
            gravity += graviryScale * Physics.gravity.y * Time.deltaTime;
        }

        moveDirection.y = gravity;
        controller.Move(moveDirection * playerSpeed * Time.deltaTime); //Направление движения  в каждом кадре
    }

    private void Rotation()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseVertical = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        transform.Rotate(Vector3.up, mouseHorizontal);

        xRotation -= mouseVertical;
        xRotation = Mathf.Clamp(xRotation, -maxVerticalAngle, maxVerticalAngle);
        mainCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    public void MouseSensitvity(Slider slider)
    {
        mouseSensivity = slider.value;
    }


}
