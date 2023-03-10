using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skilltreescript : MonoBehaviour
{
    private SpielerSteu Steuerung;
    [SerializeField] private GameObject overview;
    [SerializeField] private GameObject skilltree;

    private int skillpointsperlvl = 1;

    [SerializeField] private Image[] charselectionimage;
    [SerializeField] private int currentchar;
    
    public Text skillpointtext;
    public Text nametext;
    public Text healthbuttonnumber;
    public Text defensebuttonnumber;
    public Text attackbuttonnumber;
    public Text critchancebuttonnumber;
    public Text critdmgbuttonnumber;
    public Text weaponbuttonnumber;
    public Text charbuttonnumber;
    public Text basicbuttonnumber;

    [SerializeField] private TextMeshProUGUI statshealth;
    [SerializeField] private TextMeshProUGUI healingbonus;
    [SerializeField] private TextMeshProUGUI statsdefense;
    [SerializeField] private TextMeshProUGUI defensetoattack;
    [SerializeField] private TextMeshProUGUI statsattack;
    [SerializeField] private TextMeshProUGUI statscritchance;
    [SerializeField] private TextMeshProUGUI statscritdmg;
    [SerializeField] private TextMeshProUGUI statsweaponbuff;
    [SerializeField] private TextMeshProUGUI statsweaponbuffduration;
    [SerializeField] private TextMeshProUGUI statscharbuff;
    [SerializeField] private TextMeshProUGUI statscharbuffduration;
    [SerializeField] private TextMeshProUGUI statsbasicbuffdmg;
    [SerializeField] private TextMeshProUGUI statsbasiccrit;


    private void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
    }

    private void Update()
    {
        if (Steuerung.Menusteuerung.Menucharselectionleft.WasPerformedThisFrame())
        {
            selectionbackward();
        }
        if (Steuerung.Menusteuerung.Menucharselectionright.WasPerformedThisFrame())
        {
            selectionforward();
        }
        if (Steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            closeskilltree();
        }
    }
    private void OnEnable()
    {
        Steuerung.Enable();
        foreach (Image chars in charselectionimage)
        {
            chars.color = Color.white;
        }
        currentchar = PlayerPrefs.GetInt("Maincharindex");
        charselectionimage[currentchar].color = Color.green;
        choosechar(currentchar);
    }
    private void closeskilltree()
    {
        skilltree.SetActive(false);
        overview.SetActive(true);
    }
    public void choosechar(int currentchar)
    {
        this.currentchar = currentchar;           // falls man mit click den char ausw?hlt
        nametext.text = Statics.characternames[this.currentchar] + " LvL" + Statics.charcurrentlvl;
        settextandpoints();
    }

    private void selectionforward() 
    {
        if(currentchar >= charselectionimage.Length -1)
        {
            currentchar = 0;
            choosechar(currentchar);
        }
        else
        {
            currentchar++;
            choosechar(currentchar);
        }
    }
    private void selectionbackward()
    {
        if (currentchar == 0)
        {
            currentchar = charselectionimage.Length -1;
            choosechar(currentchar);
        }
        else
        {
            currentchar--;
            choosechar(currentchar);
        }
    }
    public void imagewhite()
    {
        foreach (Image image in charselectionimage)
        {
            image.color = Color.white;
        }
    }

    private void settextandpoints()
    {
        imagewhite();

        charselectionimage[currentchar].color = Color.green;
        Statics.charskillpoints[currentchar] = Statics.charcurrentlvl * skillpointsperlvl - Statics.charspendedskillpoints[currentchar];
        skillpointtext.text = "Skillpoints " + Statics.charskillpoints[currentchar];

        healthbuttonnumber.text = "" + Statics.charhealthskillpoints[currentchar];
        defensebuttonnumber.text = "" + Statics.chardefenseskillpoints[currentchar];
        attackbuttonnumber.text = "" + Statics.charattackskillpoints[currentchar];
        critchancebuttonnumber.text = "" + Statics.charcritchanceskillpoints[currentchar];
        critdmgbuttonnumber.text = "" + Statics.charcritdmgskillpoints[currentchar];
        weaponbuttonnumber.text = "" + Statics.charweaponskillpoints[currentchar];
        charbuttonnumber.text = "" + Statics.charcharswitchskillpoints[currentchar];
        basicbuttonnumber.text = "" + Statics.charbasicskillpoints[currentchar];
        statshealth.text = Mathf.Round(Statics.charcurrenthealth[currentchar]) + "/ " + Mathf.Round(Statics.charmaxhealth[currentchar]);
        healingbonus.text = Mathf.Round(Statics.charmaxhealth[currentchar] * Statics.healhealthbonuspercentage * 0.01f).ToString();
        statsdefense.text = Mathf.Round(Statics.chardefense[currentchar]).ToString();
        defensetoattack.text = Mathf.Round(Statics.chardefense[currentchar] * Statics.defenseconvertedtoattack * 0.01f).ToString();
        statsattack.text = Mathf.Round(Statics.charattack[currentchar]).ToString();
        statscritchance.text = Mathf.Round(Statics.charcritchance[currentchar]) + "%";
        statscritdmg.text = Mathf.Round(Statics.charcritdmg[currentchar]) + "%";
        statsweaponbuff.text = Mathf.Round(Statics.charweaponbuff[currentchar]) + "%";
        statsweaponbuffduration.text = string.Format("{0:#.0}", Statics.charweaponbuffduration[currentchar]) + "sec";
        statscharbuff.text = Mathf.Round(Statics.charswitchbuff[currentchar]) + "%";
        statscharbuffduration.text = string.Format("{0:#.0}", Statics.charswitchbuffduration[currentchar]) + "sec";
        statsbasiccrit.text = string.Format("{0:#.0}", Statics.charbasiccritbuff[currentchar]) + "%";
        statsbasicbuffdmg.text = Mathf.Round(Statics.charbasicdmgbuff[currentchar]) + "%";
    }

    public void plusbutton()
    {
        Statics.charspendedskillpoints[currentchar] += 1;
        updateunspendpoint();
    }

    public void minusbutton()
    {
        Statics.charspendedskillpoints[currentchar] -= 1;

        updateunspendpoint();
    }
    public void updateunspendpoint()
    {
        Statics.charskillpoints[currentchar] = Statics.charcurrentlvl * skillpointsperlvl - Statics.charspendedskillpoints[currentchar];
        skillpointtext.text = "Skillpoints" + Statics.charskillpoints[currentchar];
    }

    public void healthnumberplus()
    {
        if(Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charhealthskillpoints[currentchar] += 1;
            Statics.charmaxhealth[currentchar] += Statics.healthperskillpoint;
            if(Statics.charcurrenthealth[currentchar] > Statics.charmaxhealth[currentchar])
            {
                Statics.charcurrenthealth[currentchar] = Statics.charmaxhealth[currentchar];
            }
            healthtext();
            plusbutton();
        }
    }
    public void healthnumberminus()
    {
        if (Statics.charhealthskillpoints[currentchar] > 0)
        {
            Statics.charhealthskillpoints[currentchar] -= 1;
            Statics.charmaxhealth[currentchar] -= Statics.healthperskillpoint;
            if (Statics.charcurrenthealth[currentchar] > Statics.charmaxhealth[currentchar])
            {
                Statics.charcurrenthealth[currentchar] = Statics.charmaxhealth[currentchar];
            }
            healthtext(); 
            minusbutton();
        }
    }
    private void healthtext()
    {
        statshealth.text = Mathf.Round(Statics.charcurrenthealth[currentchar]) + "/ " + Mathf.Round(Statics.charmaxhealth[currentchar]);
        healingbonus.text = Mathf.Round(Statics.charmaxhealth[currentchar] * Statics.healhealthbonuspercentage * 0.01f).ToString();
        healthbuttonnumber.text = "" + Statics.charhealthskillpoints[currentchar];
    }
    public void defensenumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.chardefenseskillpoints[currentchar] += 1;
            Statics.chardefense[currentchar] += Statics.armorperskillpoint;
            statsdefense.text = Statics.chardefense[currentchar] + "";
            defensebuttonnumber.text = "" + Statics.chardefenseskillpoints[currentchar];
            defensetext();
            plusbutton();
        }
    }
    public void defensenumberminus()
    {
        if (Statics.chardefenseskillpoints[currentchar] > 0)
        {
            Statics.chardefenseskillpoints[currentchar] -= 1;
            Statics.chardefense[currentchar] -= Statics.armorperskillpoint;
            defensetext();
            minusbutton();
        }
    }
    private void defensetext()
    {
        statsdefense.text = Statics.chardefense[currentchar] + "";
        defensetoattack.text = Mathf.Round(Statics.chardefense[currentchar] * Statics.defenseconvertedtoattack * 0.01f).ToString();
        defensebuttonnumber.text = Statics.chardefenseskillpoints[currentchar].ToString();
    }
    public void attacknumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charattackskillpoints[currentchar] += 1;
            Statics.charattack[currentchar] += Statics.attackperskillpoint;
            attacktext();
            plusbutton();
        }
    }
    public void attacknumberminus()
    {
        if (Statics.charattackskillpoints[currentchar] > 0)
        {
            Statics.charattackskillpoints[currentchar] -= 1;
            Statics.charattack[currentchar] -= Statics.attackperskillpoint;
            attacktext();
            minusbutton();
        }
    }
    private void attacktext()
    {
        statsattack.text = Mathf.Round(Statics.charattack[currentchar]) + "";
        attackbuttonnumber.text = Statics.charattackskillpoints[currentchar].ToString();
    }
    public void critchancenumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charcritchanceskillpoints[currentchar] += 1;
            Statics.charcritchance[currentchar] += Statics.critchanceperskillpoint;
            critchancetext();
            plusbutton();
        }
    }
    public void critchancenumberminus()
    {
        if (Statics.charcritchanceskillpoints[currentchar] > 0)
        {
            Statics.charcritchanceskillpoints[currentchar] -= 1;
            Statics.charcritchance[currentchar] -= Statics.critchanceperskillpoint;
            critchancetext();
            minusbutton();
        }
    }
    private void critchancetext()
    {
        statscritchance.text = Mathf.Round(Statics.charcritchance[currentchar]) + "%";
        critchancebuttonnumber.text = Statics.charcritchanceskillpoints[currentchar].ToString();
    }
    public void critdmgnumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charcritdmgskillpoints[currentchar] += 1;
            Statics.charcritdmg[currentchar] += Statics.critdmgperskillpoint;
            critdmgtext();
            plusbutton();
        }
    }
    public void critdmgnumberminus()
    {
        if (Statics.charcritdmgskillpoints[currentchar] > 0)
        {
            Statics.charcritdmgskillpoints[currentchar] -= 1;
            Statics.charcritdmg[currentchar] -= Statics.critdmgperskillpoint;
            critdmgtext();
            minusbutton();
        }
    }
    private void critdmgtext()
    {
        statscritdmg.text = Mathf.Round(Statics.charcritdmg[currentchar]) + "%";
        critdmgbuttonnumber.text = Statics.charcritdmgskillpoints[currentchar].ToString();
    }
    public void weaponnumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charweaponskillpoints[currentchar] += 1;
            Statics.charweaponbuff[currentchar] += Statics.weaponswitchbuffperskillpoint;
            Statics.charweaponbuffduration[currentchar] += 0.05f;
            weapontext();
            plusbutton();
        }
    }
    public void weaponnumberminus()
    {
        if (Statics.charweaponskillpoints[currentchar] > 0)
        {
            Statics.charweaponskillpoints[currentchar] -= 1;
            Statics.charweaponbuff[currentchar] -= Statics.weaponswitchbuffperskillpoint;
            Statics.charweaponbuffduration[currentchar] -= 0.05f;
            weapontext();
            minusbutton();
        }
    }
    private void weapontext()
    {
        statsweaponbuff.text = Mathf.Round(Statics.charweaponbuff[currentchar]) + "%";
        statsweaponbuffduration.text = string.Format("{0:#.0}", Statics.charweaponbuffduration[currentchar]) + "sec";
        weaponbuttonnumber.text = Statics.charweaponskillpoints[currentchar].ToString();
    }
    public void charnumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charcharswitchskillpoints[currentchar] += 1;
            Statics.charswitchbuff[currentchar] += Statics.charswitchbuffperskillpoint;
            Statics.charswitchbuffduration[currentchar] += 0.05f;
            chartext();
            plusbutton();
        }
    }

    public void charnumberminus()
    {
        if (Statics.charcharswitchskillpoints[currentchar] > 0)
        {
            Statics.charcharswitchskillpoints[currentchar] -= 1;
            Statics.charswitchbuff[currentchar] -= Statics.charswitchbuffperskillpoint;
            Statics.charswitchbuffduration[currentchar] -= 0.05f;
            chartext();
            minusbutton();
        }
    }
    private void chartext()
    {
        statscharbuff.text = Mathf.Round(Statics.charswitchbuff[currentchar]) + "%";
        statscharbuffduration.text = string.Format("{0:#.0}", Statics.charswitchbuffduration[currentchar]) + "sec";
        charbuttonnumber.text = Statics.charcharswitchskillpoints[currentchar].ToString();
    }
    public void basicnumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charbasicskillpoints[currentchar] += 1;
            Statics.charbasiccritbuff[currentchar] += 0.5f;
            Statics.charbasicdmgbuff[currentchar] += Statics.basicbuffdmgperskillpoint;
            basictext();
            plusbutton();
        }
    }
    public void basicnumberminus()
    {
        if (Statics.charbasicskillpoints[currentchar] > 0)
        {
            Statics.charbasicskillpoints[currentchar] -= 1;
            Statics.charbasiccritbuff[currentchar] -= 0.5f;
            Statics.charbasicdmgbuff[currentchar] -= Statics.basicbuffdmgperskillpoint;
            basictext();
            minusbutton();
        }
    }
    private void basictext()
    {
        statsbasiccrit.text = string.Format("{0:#.0}", Statics.charbasiccritbuff[currentchar]) + "%";
        statsbasicbuffdmg.text = Mathf.Round(Statics.charbasicdmgbuff[currentchar]) + "%";
        basicbuttonnumber.text = Statics.charbasicskillpoints[currentchar].ToString();
    }
}
