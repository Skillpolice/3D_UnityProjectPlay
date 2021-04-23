using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    [Header("Door Ui")]
    [SerializeField] private float doMoveY = 22;
    [SerializeField] private float timeOpenDoor = 2;

    public void ActivatedUp()
    {
        transform.DOMoveY(doMoveY, timeOpenDoor);
    }

}
