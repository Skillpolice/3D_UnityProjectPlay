using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPharmacy : MonoBehaviour
{
    [SerializeField] private float upHealth = 10f;

    private void Applyeffect()
    {
        Player.Instance.UpHealthPlayer(upHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Applyeffect();
            Destroy(gameObject);

        }
    }

}
