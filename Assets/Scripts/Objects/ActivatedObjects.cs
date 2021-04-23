using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedObjects : MonoBehaviour
{
    public GameObject activOBJ;

    private void Start()
    {
        //activTorch = GetComponent<GameObject>();

        activOBJ.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            activOBJ.SetActive(true);
        }
    }
}
