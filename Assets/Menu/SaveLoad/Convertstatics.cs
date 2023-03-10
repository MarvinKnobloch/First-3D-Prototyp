using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convertstatics 
{
    public Vector3 playerposition;                       //Werte m?ssen public sein sonst werde sich nicht gespeichtert
    public Quaternion playerrotation;
    public float camvalueX;
    public int charcurrentlvl;
    public float charcurrentexp;
    public float charrequiredexp;
    public string gamesavedate;
    public string gamesavetime;

    public int firstchar;
    public int secondchar;
    public int thirdchar;
    public int forthchar;

    public bool elementalmenuisactiv;

    public Color[] charactersecondelementcolor;
    public float[] charbasichealth;
    public float[] charcurrenthealth;
    public float[] charmaxhealth;
    public float[] chardefense;
    public float[] charattack;
    public float[] charcritchance;
    public float[] charcritdmg;
    public float[] charweaponbuff;
    public float[] charweaponbuffduration;
    public float[] charswitchbuff;
    public float[] charswitchbuffduration;
    public float[] charbasiccritbuff;
    public float[] charbasicdmgbuff;

    public int[] firstweapon;
    public int[] secondweapon;

    public float[] charswordattack;
    public float[] charbowattack;
    public float[] charfistattack;

    public Itemcontroller[] currentsword;
    public Itemcontroller[] currentbow;
    public Itemcontroller[] currentfist;
    public Itemcontroller[] currenthead;
    public Itemcontroller[] currentchest;
    public Itemcontroller[] currentbelt;
    public Itemcontroller[] currentlegs;
    public Itemcontroller[] currentshoes;
    public Itemcontroller[] currentneckless;
    public Itemcontroller[] currentring;

    public int[] charspendedskillpoints;
    public int[] charskillpoints;

    public int[] charhealthskillpoints;
    public int[] chardefenseskillpoints;
    public int[] charattackskillpoints;
    public int[] charcritchanceskillpoints;
    public int[] charcritdmgskillpoints;
    public int[] charweaponskillpoints;
    public int[] charcharswitchskillpoints;
    public int[] charbasicskillpoints;


    public int[] spellnumbers;
    public Color[] spellcolors;

    public bool[] stoneisactivated;
    public int[] charactersecondelement;
    public string[] characterclassrolltext;
    public int[] characterclassroll;
    public int maincharstoneclass;
    public int secondcharstoneclass;
    public int thirdcharstoneclass;
    public int forthcharstoneclass;
    public float groupstonehealbonus;
    public float groupstonedefensebonus;
    public float groupstonedmgbonus;
    public bool thirdcharishealer;
    public bool forthcharishealer;


    public void savestaticsinscript()
    {
        playerposition = LoadCharmanager.savemainposi;
        playerrotation = LoadCharmanager.savemainrota;
        camvalueX = LoadCharmanager.savecamvalueX;
        gamesavedate = System.DateTime.UtcNow.ToString("dd MMMM, yyyy");
        gamesavetime = System.DateTime.UtcNow.ToString("HH:mm");

        charcurrentlvl = Statics.charcurrentlvl;
        charcurrentexp = Statics.charcurrentexp;
        charrequiredexp = Statics.charrequiredexp;

        firstchar = Statics.currentfirstchar;
        secondchar = Statics.currentsecondchar;
        thirdchar = Statics.currentthirdchar;
        forthchar = Statics.currentforthchar;

        elementalmenuisactiv = Statics.elementalmenuisactiv;

        firstweapon = Statics.firstweapon;
        secondweapon = Statics.secondweapon;

        charbasichealth = Statics.charbasichealth;
        charcurrenthealth = Statics.charcurrenthealth;
        charmaxhealth = Statics.charmaxhealth;
        chardefense = Statics.chardefense;
        charattack = Statics.charattack;
        charcritchance = Statics.charcritchance;
        charcritdmg = Statics.charcritdmg;
        charweaponbuff = Statics.charweaponbuff;
        charweaponbuffduration = Statics.charweaponbuffduration;
        charswitchbuff = Statics.charswitchbuff;
        charswitchbuffduration = Statics.charswitchbuffduration;
        charbasiccritbuff = Statics.charbasiccritbuff;
        charbasicdmgbuff = Statics.charbasicdmgbuff;


        charswordattack = Statics.charswordattack;
        charbowattack = Statics.charbowattack;
        charfistattack = Statics.charfistattack;

        currentsword = Statics.charcurrentsword;
        currentbow = Statics.charcurrentbow;
        currentfist = Statics.charcurrentfist;
        currenthead = Statics.charcurrenthead;
        currentchest = Statics.charcurrentchest;
        currentbelt = Statics.charcurrentbelt;
        currentshoes = Statics.charcurrentshoes;
        currentlegs = Statics.charcurrentlegs;
        currentneckless = Statics.charcurrentnecklace;
        currentring = Statics.charcurrentring;

        charspendedskillpoints = Statics.charspendedskillpoints;
        charskillpoints = Statics.charskillpoints;
        charhealthskillpoints = Statics.charhealthskillpoints;
        chardefenseskillpoints = Statics.chardefenseskillpoints;
        charattackskillpoints = Statics.charattackskillpoints;
        charcritchanceskillpoints = Statics.charcritchanceskillpoints;
        charcritdmgskillpoints = Statics.charcritchanceskillpoints;
        charweaponskillpoints = Statics.charweaponskillpoints;
        charcharswitchskillpoints = Statics.charcharswitchskillpoints;
        charbasicskillpoints = Statics.charbasicskillpoints;



        spellnumbers = Statics.spellnumbers;
        spellcolors = Statics.spellcolors;

        stoneisactivated = Statics.stoneisactivated;
        charactersecondelementcolor = Statics.charactersecondelementcolor;
        charactersecondelement = Statics.charactersecondelement;
        characterclassrolltext = Statics.characterclassrolltext;
        characterclassroll = Statics.characterclassroll;
        groupstonehealbonus = Statics.groupstonehealbonus;
        groupstonedefensebonus = Statics.groupstonedefensebonus;
        groupstonedmgbonus = Statics.groupstonedmgbonus;
    }

    public void setstaticsafterload()
    {
        LoadCharmanager.savemainposi = playerposition;
        LoadCharmanager.savemainrota = playerrotation;
        LoadCharmanager.savecamvalueX = camvalueX;

        Statics.charcurrentlvl = charcurrentlvl;
        Statics.charcurrentexp = charcurrentexp;
        Statics.charrequiredexp = charrequiredexp;

        charcurrentlvl = Statics.charcurrentlvl;
        charcurrentexp = Statics.charcurrentexp;
        charrequiredexp = Statics.charrequiredexp;

        Statics.currentactiveplayer = 0;
        Statics.currentfirstchar = firstchar;
        Statics.currentsecondchar = secondchar;
        Statics.currentthirdchar = thirdchar;
        Statics.currentforthchar = forthchar;

        Statics.elementalmenuisactiv = elementalmenuisactiv;

        Statics.firstweapon = firstweapon;
        Statics.secondweapon = secondweapon;
        Statics.charbasichealth = charbasichealth;
        Statics.charcurrenthealth = charcurrenthealth;
        Statics.charmaxhealth = charmaxhealth;
        Statics.chardefense = chardefense;
        Statics.charattack = charattack;
        Statics.charcritchance = charcritchance;
        Statics.charcritdmg = charcritdmg;
        Statics.charweaponbuff = charweaponbuff;
        Statics.charweaponbuffduration = charweaponbuffduration;
        Statics.charswitchbuff = charswitchbuff;
        Statics.charswitchbuffduration = charswitchbuffduration;
        Statics.charbasiccritbuff = charbasiccritbuff;
        Statics.charbasicdmgbuff = charbasicdmgbuff;

        Statics.charswordattack = charswordattack;
        Statics.charbowattack = charbowattack;
        Statics.charfistattack = charfistattack;

        Statics.charcurrentsword = currentsword;
        Statics.charcurrentbow = currentbow;
        Statics.charcurrentfist = currentfist;
        Statics.charcurrenthead = currenthead;
        Statics.charcurrentchest = currentchest;
        Statics.charcurrentbelt = currentbelt;
        Statics.charcurrentshoes = currentshoes;
        Statics.charcurrentlegs = currentlegs;
        Statics.charcurrentnecklace = currentneckless;
        Statics.charcurrentring = currentring;

        Statics.charspendedskillpoints = charspendedskillpoints;
        Statics.charskillpoints = charskillpoints;
        Statics.charhealthskillpoints = charhealthskillpoints;
        Statics.chardefenseskillpoints = chardefenseskillpoints;
        Statics.charattackskillpoints = charattackskillpoints;
        Statics.charcritchanceskillpoints = charcritchanceskillpoints;
        Statics.charcritchanceskillpoints = charcritdmgskillpoints;
        Statics.charweaponskillpoints = charweaponskillpoints;
        Statics.charcharswitchskillpoints = charcharswitchskillpoints;
        Statics.charbasicskillpoints = charbasicskillpoints;

        Statics.spellnumbers = spellnumbers;
        Statics.spellcolors = spellcolors;

        Statics.stoneisactivated = stoneisactivated;
        Statics.charactersecondelementcolor = charactersecondelementcolor;
        Statics.charactersecondelement = charactersecondelement;
        Statics.characterclassrolltext = characterclassrolltext;
        Statics.characterclassroll = characterclassroll;
        Statics.groupstonehealbonus = groupstonehealbonus;
        Statics.groupstonedefensebonus = groupstonedefensebonus;
        Statics.groupstonedmgbonus = groupstonedmgbonus;
    }
}
