using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    public UnityEvent openObj;
    public UnityEvent closeObj;

    private void OnTriggerEnter(Collider other)
    {
        openObj.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        closeObj.Invoke();
    }




}
