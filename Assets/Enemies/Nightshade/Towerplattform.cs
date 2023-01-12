using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towerplattform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            other.transform.parent = transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            other.transform.parent = null;
        }
    }
}
