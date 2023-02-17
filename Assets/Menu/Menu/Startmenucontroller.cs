using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
//using Newtonsoft.Json;

public class Startmenucontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject loadgameobj;
    [SerializeField] private GameObject settingsobj;
    private Setitemsandinventory setitemsandinventory;
    private Setstaticsnull setstaticsnull;
    //private Isaveload loadsaveinterface = new Saveloadgame();
    private Convertstatics convertstatics = new Convertstatics();

    public Color selectedcolor;
    public Color notselectedcolor;

    private void Awake()
    {
        setitemsandinventory = GetComponent<Setitemsandinventory>();
        setstaticsnull = GetComponent<Setstaticsnull>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        loadgamesettings();
        loadsaveslots();
    }
    private void OnEnable()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Image>().color = notselectedcolor;
        }
        Mouseactivate.enablemouse();
    }
    private void loadsaveslots()
    {
        for (int i = 0; i < Slotvaluesarray.slotisnotempty.Length; i++)
        {
            string loadpath = Application.persistentDataPath + "/Statics" + i + ".json";
            if (File.Exists(loadpath))
            {
                string loaded_data = File.ReadAllText(loadpath);
                convertstatics = JsonUtility.FromJson<Convertstatics>(loaded_data);
                Debug.Log("Data Slot" + i + " exists");
                saveslotvalues(i);
            }
            else
            {
                Debug.Log("Data Slot" + i + " doesn't exist");
            }
        }
    }
    private void loadgamesettings()
    {
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("Vsyncvalue");
    }
    private void saveslotvalues(int slot)
    {
        Slotvaluesarray.slotisnotempty[slot] = true;
        Slotvaluesarray.slotlvl[slot] = convertstatics.charcurrentlvl;
        Slotvaluesarray.slotdate[slot] = convertstatics.gamesavedate;
        Slotvaluesarray.slottime[slot] = convertstatics.gamesavetime;
    }
    public void newgame()
    {
        Statics.currentfirstchar = 0;
        Statics.currentsecondchar = 1;
        Statics.currentthirdchar = 2;
        Statics.currentforthchar = 3;

        Statics.charcurrentlvl = 1;
        Statics.charcurrentexp = 0;
        Statics.charrequiredexp = 52;

        Statics.charbasichealth = new float[] { 100, 93, 97, 91, 95 };
        Statics.charcurrenthealth = new float[] { 100, 93, 97, 91, 95 };
        Statics.charmaxhealth = new float[] { 100, 93, 97, 91, 95 };
        Statics.chardefense = new float[] { 100, 100, 100, 100, 100 };
        Statics.charattack = new float[] { 1, 1, 1, 1, 1 };
        Statics.charcritchance = new float[] { 5, 5, 5, 5, 5 };
        Statics.charcritdmg = new float[] { 150, 150, 150, 150, 150 };
        Statics.charweaponbuff = new float[] { 100, 100, 100, 100, 100 };
        Statics.charweaponbuffduration = new float[] { 5, 5, 5, 5, 5 };
        Statics.charswitchbuff = new float[] { 100, 100, 100, 100, 100 };
        Statics.charswitchbuffduration = new float[] { 5, 5, 5, 5, 5 };
        Statics.charbasiccritbuff = new float[] { 1, 1, 1, 1, 1 };
        Statics.charbasicdmgbuff = new float[] { 150, 150, 150, 150, 150 };

        //resetgameplaystatics
        Statics.healcdbool = false;
        Statics.dashcdbool = false;
        Statics.weapsonswitchbool = false;
        Statics.charswitchbool = false;
        Statics.characterswitchbuff = 100;
        Statics.weaponswitchbuff = 100;
        Statics.timer = false;

        Statics.stoneisactivated = new bool[24];
        Statics.charactersecondelement = new int[] { -1, -1, -1, -1, -1 };
        Statics.charactersecondelementcolor = new Color[] { new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255) };
        Statics.characterclassrolltext = new string[5];
        Statics.characterclassroll = new int[] { -1, -1, -1, -1, -1 };
        Statics.groupstonehealbonus = 0;
        Statics.groupstonedefensebonus = 0;
        Statics.groupstonedmgbonus = 0;

        for (int i = 0; i < Statics.playablechars; i++)
        {
            Statics.firstweapon[i] = 0;
            Statics.secondweapon[i] = 1;
        }
        for (int i = 0; i < Statics.playablechars; i++)
        {
            Statics.charspendedskillpoints[i] = 0;
            Statics.charskillpoints[i] = 0;
            Statics.charhealthskillpoints[i] = 0;
            Statics.chardefenseskillpoints[i] = 0;
            Statics.charattackskillpoints[i] = 0;
            Statics.charcritchanceskillpoints[i] = 0;
            Statics.charcritchanceskillpoints[i] = 0;
            Statics.charweaponskillpoints[i] = 0;
            Statics.charcharswitchskillpoints[i] = 0;
            Statics.charbasicskillpoints[i] = 0;
        }

        setitemsandinventory.resetitems();
        setitemsandinventory.resetinventorys();
        setstaticsnull.resetstatics();
        SceneManager.LoadScene(1);
    }

    public void loadgame()
    {
        loadgameobj.SetActive(true);
        gameObject.SetActive(false);
    }
    public void settings()
    {
        settingsobj.SetActive(true);
        gameObject.SetActive(false);
    }
    public void endgame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
