using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    Damageable damageable;
    Rigidbody rb;

    [SerializeField] public int damageBullet = 10;
    [SerializeField] private int speedBullet = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //Transform target = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 bulletAccuracy = new Vector3(Random.Range(0, 0.3f), Random.Range(0, 0.3f), Random.Range(0, 0f)); //�������� ������ ������� ������ ������
        Vector3 direction = (Player.Instance.transform.position - transform.position) + bulletAccuracy; //�������� ������ ������� ������ ������

        rb.AddForce(direction * speedBullet * Time.deltaTime); //������� ���� �������
    }

    private void Start()
    {
        damageable = GetComponent<Damageable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.DoDamage(damageBullet);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }

    }

    private void OnBecameInvisible() //����������� �������� �� ������� ������
    {
        if (gameObject.activeSelf)
        {
            //LeanPool.Despawn(gameObject);

            Destroy(gameObject, 10f);
        }

    }
}
