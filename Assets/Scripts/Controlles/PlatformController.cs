using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private GameObject nextPlatform;

    private void Start()
    {
        nextPlatform.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nextPlatform.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(IStartEnablePlatform());
        }
    }


    IEnumerator IStartEnablePlatform()
    {
        yield return new WaitForSeconds(2f);

        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendCallback(() =>
        {
            nextPlatform.SetActive(false);
        });
        mySequence.AppendInterval(1f);
        mySequence.AppendCallback(() =>
        {
            nextPlatform.SetActive(true);
        });

       //mySequence.SetLoops(-1);
        

        
    }







}
