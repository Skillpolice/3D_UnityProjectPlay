using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class TeleportController : MonoBehaviour
{
    [SerializeField] TeleportController otherTeleport;
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip stantTeleport;

    private Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
        audio = GetComponent<AudioSource>();
    }

    public void EnableCollider(bool enable)
    {
        col.enabled = enable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audio.PlayOneShot(stantTeleport, 0.5f);

            other.transform.DOMove(otherTeleport.transform.position, 2f);
            StartCoroutine(DisableOtherTeleport());
        }
    }

    IEnumerator DisableOtherTeleport()
    {
        otherTeleport.EnableCollider(false);
        yield return new WaitForSeconds(5f);
        otherTeleport.EnableCollider(true);
    }
}
