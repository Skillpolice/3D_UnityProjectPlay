using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spikes : MonoBehaviour
{
    [SerializeField] float downY;
    [SerializeField] float upY;

    [SerializeField] float upTime = 10f;
    [SerializeField] float downTime = 10f;

    [SerializeField] float upDelay = 1f;
    [SerializeField] float downDelay = 1f;

    [SerializeField] private float spikesDamage = 2f;


    private void Start()
    {
        Vector3 newPos = transform.position;
        newPos.y = downY;
        transform.position = newPos;

        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Append(transform.DOMoveY(upY, upTime));
        moveSequence.AppendInterval(upDelay);

        moveSequence.Append(transform.DOMoveY(downY, downTime));
        moveSequence.AppendInterval(downDelay);
        moveSequence.SetLoops(-1); //Повторять бесконечно
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.DoDamage(spikesDamage);
        }
    }
}
