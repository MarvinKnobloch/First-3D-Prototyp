using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeplayerchildofplattform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar)
        {
            other.transform.parent = transform.parent;           //braucht ein �bertransform damit der Scale vom Char nicht umge�ndert wird
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
