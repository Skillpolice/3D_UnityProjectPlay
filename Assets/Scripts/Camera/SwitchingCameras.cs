using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchingCameras : MonoBehaviour
{
    //[Header("Camers")]
    //[SerializeField] private Camera mainCamera;
    [SerializeField] private Camera firsPersonCamera;

    [Header("Scripts")]
    PlayerMovement movement;
    LookingFirstPerson lookingFirst;

    public UnityEvent SwitchingPistolCamera;
    public UnityEvent SwitchingMachineCamera;
    public UnityEvent SwitchingStaffCamera;


    public void PistolCamera()
    {
        SwitchingPistolCamera.Invoke();
    }

    public void MachineCamera()
    {
        SwitchingMachineCamera.Invoke();
    }

    public void StaffCamera()
    {
        SwitchingStaffCamera.Invoke();
    }


    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        lookingFirst = GetComponent<LookingFirstPerson>();


        firsPersonCamera.gameObject.SetActive(false);
        lookingFirst.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            PistolGun();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            MachineGun();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            StaffGun();
        }
    }

    private void PistolGun()
    {
        PistolCamera();
        firsPersonCamera.gameObject.SetActive(true);

        movement.enabled = false;
        lookingFirst.enabled = true;
    }

    private void MachineGun()
    {
        MachineCamera();
        firsPersonCamera.gameObject.SetActive(true);

        movement.enabled = false;
        lookingFirst.enabled = true;
    }

    private void StaffGun()
    {
        StaffCamera();
        movement.enabled = true;
        lookingFirst.enabled = false;
    }


}
