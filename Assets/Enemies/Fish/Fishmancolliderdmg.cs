using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fishmancolliderdmg : MonoBehaviour
{
    [SerializeField] private GameObject redline;

    private bool dealdmgonce;

    [NonSerialized] public float basedmg;

    private void OnEnable()
    {
        StartCoroutine("turnoff");
        dealdmgonce = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar && dealdmgonce == false)
        {
            dealdmgonce = true;
            LoadCharmanager.Overallmainchar.GetComponent<SpielerHP>().TakeDamage(basedmg);
        }
    }
    IEnumerator turnoff()
    {
        yield return null;
        StopAllCoroutines();
        gameObject.SetActive(false);
        redline.SetActive(false);
    }
}
