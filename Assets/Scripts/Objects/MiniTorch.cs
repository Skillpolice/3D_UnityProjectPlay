using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class MiniTorch : MonoBehaviour
{
    [Header("Parctical Effect")]
    [SerializeField] ParticleSystem fireEffect;
    [SerializeField] GameObject lightEffect;

    [Header("Torch")]
    [SerializeField] GameObject bigTorch;


    private void Start()
    {
        Damageable damageable = GetComponent<Damageable>();
        damageable.OnResiveDamage += ActiveteTorch;
       

        bigTorch.gameObject.SetActive(false);
    }

    private void ActiveteTorch(float damage)
    {
        //fireEffect.SetActive(!fireEffect.activeSelf);
        //GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value);

        if (fireEffect.isPlaying)
        {
            fireEffect.Stop();
            lightEffect.SetActive(false);
        }
        else
        {
            fireEffect.Play();
            lightEffect.SetActive(true);
        }

        bigTorch.gameObject.SetActive(true);

    }
}