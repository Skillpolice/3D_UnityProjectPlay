using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActivatedEnemy : MonoBehaviour
{
    public GameObject enemies;
    [SerializeField] private float timeActivatedEnemy = 1f;


    private void Start()
    {
        //enemies = GetComponent<GameObject>();
        enemies.gameObject.SetActive(false);
    }

    public void ActivEnemy()
    {
        StartCoroutine(IActivatedEnemy());
        print("Is Active Enemy");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivEnemy();
        }
    }

    private IEnumerator IActivatedEnemy()
    {
        yield return new WaitForSeconds(timeActivatedEnemy);
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendCallback(() =>
        {
            enemies.gameObject.SetActive(true);
        });
    }
}
