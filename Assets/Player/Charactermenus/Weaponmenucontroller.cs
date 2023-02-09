using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weaponmenucontroller : MonoBehaviour
{
    [SerializeField] private int charnumber;

    [SerializeField] private GameObject weaponslot1;
    [SerializeField] private GameObject weaponslot2;
    [SerializeField] private GameObject weapongrid1;
    [SerializeField] private GameObject weapongrid2;
    [SerializeField] private Text lvltext;
    [SerializeField] private Text exp;
    [SerializeField] private Text hpanzeige;
    [SerializeField] private Sprite[] images;

    private bool weapongridopen;
    private int firstweapon;
    private int secondweapon;

    private void OnEnable()
    {
        weapongridopen = false;
        weapongrid1.SetActive(false);
        weapongrid2.SetActive(false);
        firstweapon = Statics.firstweapon[charnumber];
        secondweapon = Statics.secondweapon[charnumber];
        weaponslot1.gameObject.GetComponent<Image>().sprite = images[firstweapon];
        weaponslot2.gameObject.GetComponent<Image>().sprite = images[secondweapon];
        lvltext.text = "Level " + Statics.charcurrentlvl;
        exp.text = "EXP " + Statics.charcurrentexp + "/" + Statics.charrequiredexp;
        hpanzeige.text = "HP " + Statics.charcurrenthealth[charnumber] + "/" + Statics.charmaxhealth[charnumber];
    }
    public void openweapongrid(GameObject grid)
    {
        if (weapongridopen == false)
        {
            grid.SetActive(true);
            weapongridopen = true;
        }
        else
        {
            grid.SetActive(false);
            weapongridopen = false;
        }
    }
    public void setmainweapon(int newweaponnumber)
    {
        if (secondweapon == newweaponnumber)
        {
            secondweapon = firstweapon;
            Statics.secondweapon[charnumber] = secondweapon;
            weaponslot2.gameObject.GetComponent<Image>().sprite = images[secondweapon];
        }
        firstweapon = newweaponnumber;
        Statics.firstweapon[charnumber] = firstweapon;
        weaponslot1.gameObject.GetComponent<Image>().sprite = images[firstweapon];
        weapongrid1.SetActive(false);
        weapongrid2.SetActive(false);
        weapongridopen = false;

    }

    public void setsecondweapon(int newweaponnumber)
    {
        if (firstweapon == newweaponnumber)
        {
            firstweapon = secondweapon;
            Statics.firstweapon[charnumber] = firstweapon;
            weaponslot1.gameObject.GetComponent<Image>().sprite = images[firstweapon];
        }
        secondweapon = newweaponnumber;
        Statics.secondweapon[charnumber] = secondweapon;
        weaponslot2.gameObject.GetComponent<Image>().sprite = images[secondweapon];
        weapongrid1.SetActive(false);
        weapongrid2.SetActive(false);
        weapongridopen = false;
    }
}
