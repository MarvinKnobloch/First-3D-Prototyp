using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponswitch : MonoBehaviour
{
    private SpielerSteu controlls;
    private Animator animator;

    [SerializeField] private int charnumber;

    [SerializeField] private GameObject[] allweapons;
    [SerializeField] private MonoBehaviour[] weaponscripts;
    [SerializeField] private RuntimeAnimatorController[] weaponanimation;

    private Movescript movescript;

    private int firstweapon;
    private int secondweapon;
    private bool mainweaponactiv;
    void Awake()
    {
        animator = GetComponent<Animator>();
        movescript = GetComponent<Movescript>();
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        controlls.Enable();
        LoadCharmanager.setweapons += setweapons;
    }
    private void OnDisable()
    {
        LoadCharmanager.setweapons -= setweapons;
    }
    void Update()
    {
        if (LoadCharmanager.disableattackbuttons == false)
        {
            if (controlls.Player.Weaponchange.WasPerformedThisFrame() && Statics.otheraction == false && Statics.weapsonswitchbool == false)
            {
                movescript.checkforcamstate();
                Statics.otheraction = true;
                if (mainweaponactiv == true)
                {
                    spawnsecondweapon();                 
                }
                else
                {
                    spawnmainweapon();
                }
            }
        }
    }

    private void spawnmainweapon()
    {
        mainweaponactiv = true;
        allweapons[secondweapon].SetActive(false);
        allweapons[firstweapon].SetActive(true);
        animator.runtimeAnimatorController = weaponanimation[firstweapon];
        weaponscripts[secondweapon].enabled = false;
        weaponscripts[firstweapon].enabled = true;

        Statics.weaponswitchbuff = Statics.charweaponbuff[charnumber];
        GlobalCD.startweaponswitchcd();
        GlobalCD.startweaponswitchbuff(charnumber);
        Statics.otheraction = false;
        movescript.state = Movescript.State.Air;
    }
    private void spawnsecondweapon()
    {
        mainweaponactiv = false;
        allweapons[firstweapon].SetActive(false);
        allweapons[secondweapon].SetActive(true);
        animator.runtimeAnimatorController = weaponanimation[secondweapon];
        weaponscripts[firstweapon].enabled = false;
        weaponscripts[secondweapon].enabled = true;

        Statics.weaponswitchbuff = Statics.charweaponbuff[charnumber];
        GlobalCD.startweaponswitchcd();
        GlobalCD.startweaponswitchbuff(charnumber); 
        Statics.otheraction = false;
        movescript.state = Movescript.State.Air;
    }


    private void setweapons()
    {
        firstweapon = Statics.firstweapon[charnumber];
        secondweapon = Statics.secondweapon[charnumber];
        foreach (MonoBehaviour attackscripts in weaponscripts)
        {
            attackscripts.enabled = false;
        }
        foreach (GameObject weapon in allweapons)
        {
            weapon.SetActive(false);
        }
        GlobalCD.startweaponswitchcd();
        mainweaponactiv = true;
        allweapons[firstweapon].SetActive(true);
        animator.runtimeAnimatorController = weaponanimation[firstweapon];
        weaponscripts[firstweapon].enabled = true;
    }
}

/*if (Statics.healmissingtime > 9f)
{
    Statics.healmissingtime = 9f;
    GlobalCD.onswitchhealingcd();
}*/