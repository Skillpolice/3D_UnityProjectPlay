using UnityEngine;
using UnityEngine.Audio;

public class CheckPointController : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip standTeleport;

    private Vector3 checkPoint;
    public ParticleSystem checkpointEffect;

    bool ischeckpoint;

    private void Start()
    {
        checkPoint = transform.position;
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audio.PlayOneShot(standTeleport, 0.5f);

            Player.Instance.startPos = checkPoint;

            Instantiate(checkpointEffect, transform.position, Quaternion.identity);
        }

        print("CheckPoint " + name + "  " + checkPoint);
    }

}
