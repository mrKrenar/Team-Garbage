using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Garbage"))
        {
            Debug.Log("Garbage cleared", other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
