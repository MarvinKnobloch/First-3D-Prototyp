using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gasmantarget : MonoBehaviour
{
    public bool laserfail;
    public bool lasercomplete;
    public bool dmgonce;

    [NonSerialized] public float faillinedmg;

    private void OnEnable()
    {
        dmgonce = false;
        laserfail = false;
        lasercomplete = false;
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
    public void dealdmg()
    {
        if(laserfail == false)
        {
            if(dmgonce == false)
            {
                dmgonce = true;
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(faillinedmg);
            }
            laserfail = true;
        }
    }
}
