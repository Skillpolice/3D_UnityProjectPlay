using UnityEngine;

public class Bullet : MonoBehaviour
{
    Damageable damageable;
    Rigidbody rb;

    [SerializeField] private int damageBullet = 10;
    [SerializeField] private int speedBullet = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //Transform target = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 bulletAccuracy = new Vector3(Random.Range(0, 0.9f), Random.Range(0, 0.9f), Random.Range(0,0f)); //Стрельба вокруг области центра Игрока
        Vector3 direction = (Player.Instance.transform.position - transform.position) + bulletAccuracy; //Стрельба вокруг области центра Игрока

        rb.AddForce(direction * speedBullet * Time.deltaTime); //Придаем силу патрону
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

    private void OnBecameInvisible() //Уничтожение обьектов за пределы камеры
    {
        if (gameObject.activeSelf)
        {
            //LeanPool.Despawn(gameObject);

            Destroy(gameObject, 10f);
        }

    }

}
