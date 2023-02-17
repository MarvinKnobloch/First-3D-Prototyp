using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Charswitch : MonoBehaviour
{
    private SpielerSteu Steuerung;
    [SerializeField] private CinemachineFreeLook Cam1;
    [SerializeField] private CinemachineVirtualCamera aimcam;
    public Image ability1;
    public Image ability2;

    public GameObject charmanager;
    private Manamanager manacontroller;

    void Awake()
    {
        manacontroller = charmanager.GetComponent<Manamanager>();
        Steuerung = Keybindinputmanager.inputActions;
    }

    private void OnEnable()
    {
        Steuerung.Enable();
    }

    void Update()
    {
        if (LoadCharmanager.disableattackbuttons == false)
        {
            if (Steuerung.Player.Charchange.WasPerformedThisFrame() && Statics.otheraction == false && Statics.charswitchbool == false)
            {
                if (Statics.currentactiveplayer == 0)
                {
                    switchtosecondchar();
                }
                else
                {
                    switchtomainchar();
                }
            }
        }
    }
    private void switchtosecondchar()
    {
        switchvalues();
        GetComponent<HealthUImanager>().switchtosecond();
        GlobalCD.currentcharswitchchar = Statics.currentsecondchar;
        GlobalCD.startcharswitch();
        ability1.color = Statics.spellcolors[3];
        ability2.color = Statics.spellcolors[4];
        if(Statics.characterclassroll[Statics.currentsecondchar] == 1)
        {
            Statics.playertookdmgfromamount = 2;
        }
        else
        {
            Statics.playertookdmgfromamount = 1;
        }
        Statics.currentactiveplayer = 1;                                                     
    }

    private void switchtomainchar()
    {
        switchvalues();
        GetComponent<HealthUImanager>().switchtomain();
        GlobalCD.currentcharswitchchar = Statics.currentfirstchar;
        GlobalCD.startcharswitch();
        ability1.color = Statics.spellcolors[0];
        ability2.color = Statics.spellcolors[1];
        if (Statics.characterclassroll[Statics.currentfirstchar] == 1)
        {
            Statics.playertookdmgfromamount = 2;
        }
        else
        {
            Statics.playertookdmgfromamount = 1;
        }
        Statics.currentactiveplayer = 0;
    }
    private void switchvalues()
    {
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        LoadCharmanager.Overallsecondchar.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        LoadCharmanager.Overallsecondchar.transform.rotation = LoadCharmanager.Overallmainchar.transform.rotation;
        LoadCharmanager.Overallmainchar.SetActive(false);
        GameObject Savechar = LoadCharmanager.Overallmainchar;
        LoadCharmanager.Overallmainchar = LoadCharmanager.Overallsecondchar;
        LoadCharmanager.Overallmainchar.SetActive(true);
        LoadCharmanager.Overallsecondchar = Savechar;
        Cam1.LookAt = LoadCharmanager.Overallmainchar.transform;
        Cam1.Follow = LoadCharmanager.Overallmainchar.transform;
        aimcam.LookAt = LoadCharmanager.Overallmainchar.transform;
        aimcam.Follow = LoadCharmanager.Overallmainchar.transform;
        LoadCharmanager.Overallmainchar.gameObject.GetComponent<Playerhp>().playerhpuislot = 0;
        manacontroller.Managemana(5);
    }
}


/*
if (Statics.healmissingtime > 9f)
{
    Statics.healmissingtime = 9f;
    GlobalCD.onswitchhealingcd();
}*/


