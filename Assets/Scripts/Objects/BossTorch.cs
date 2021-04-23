using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTorch : MonoBehaviour
{
    public MeshRenderer mesh;
    public CapsuleCollider collider;
    public GameObject fireEffect;

    [Header("Audio UI")]
    [SerializeField] private AudioSource audio;

    [Header("Text UI")]
    [SerializeField] private Text saveStrom;
    [SerializeField] private Text searing—old;

    [Header("Torch UI Time")]
    [SerializeField] private float timeFromTorchOn;
    [SerializeField] private float timeBeforeTorchOff;


    float timeOnTorch;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<CapsuleCollider>();
        

        StartCoroutine(IOffOnTorch());
    }

    private IEnumerator IOffOnTorch()
    {
        timeOnTorch = Random.Range(timeFromTorchOn, timeBeforeTorchOff);

        while (true)
        {
            yield return new WaitForSeconds(timeOnTorch);
            mesh.enabled = false;
            collider.enabled = false;
            fireEffect.gameObject.SetActive(false);
            ApplyEfefct();

            yield return new WaitForSeconds(timeOnTorch);
            mesh.enabled = true;
            collider.enabled = true;
            fireEffect.gameObject.SetActive(true);
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.StartUpHealth();

            if (Player.Instance.playerHealth <= 100)
            {
                audio.Play();
            }
            else
            {
                audio.Stop();
            }

            saveStrom.gameObject.SetActive(true);
            searing—old.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ApplyEfefct();
        }
    }
    private void ApplyEfefct()
    {
        Player.Instance.StartDownHealth();

        audio.Stop();

        saveStrom.gameObject.SetActive(false);
        searing—old.gameObject.SetActive(true);
    }



}
