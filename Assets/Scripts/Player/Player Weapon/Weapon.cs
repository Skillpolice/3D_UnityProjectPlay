using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Collider col;

    [SerializeField] private int damage = 40;

    [Header("Ref")]
    [SerializeField] ParticleSystem attackEffect;

    private void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    public void AttackStart()
    {
        attackEffect.Play();
        col.enabled = true;
    }

    public void AttackEnd()
    {
        col.enabled = false;
        attackEffect.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable damageable = other.GetComponent<Damageable>(); //Пытаемся взять Damageable у обьекта с которым столькнулись
        if (damageable != null)
        {
            damageable.DoDamage(damage);
        }

        print("Name on trigger: " + other.name);
    }
}