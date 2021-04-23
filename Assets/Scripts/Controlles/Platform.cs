using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Platform : MonoBehaviour
{
    public Platform platform;

    [SerializeField] Vector3 moveDistance; //откуда едем и куда
    [SerializeField] float speed = 1f; //Время

    private void Start()
    {
        platform = GetComponent<Platform>();

        float  moveTime = moveDistance.magnitude / speed;

        transform.DOMove(moveDistance, moveTime)
            .SetRelative(true) //движение из текущей похиции
            .SetEase(Ease.InOutFlash) //линейное движение 
            .SetUpdate(UpdateType.Fixed)
            .SetLoops(-1, LoopType.Yoyo); //постоянное двиЖение туда обратно
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform); //Влаживаем Player  в  Parent Platform
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null); //Освобождаем Parent 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.3f);
        Gizmos.DrawSphere(transform.position + moveDistance, 0.3f);
        Gizmos.DrawLine(transform.position, transform.position + moveDistance);

    }

}
