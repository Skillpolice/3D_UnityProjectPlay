using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Torch : MonoBehaviour
{
    [Header("Audio UI")]
    [SerializeField] private AudioSource audio;

    [Header("Text UI")]
    [SerializeField] private Text saveStrom;
    [SerializeField] private Text searingСold;

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
            searingСold.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.StartDownHealth();

            audio.Stop();

            saveStrom.gameObject.SetActive(false);
            searingСold.gameObject.SetActive(true);
        }
    }
}