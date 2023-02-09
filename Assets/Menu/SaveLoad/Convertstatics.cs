using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convertstatics 
{
    public Vector3 playerposition;                       //Werte m�ssen public sein sonst werde sich nicht gespeichtert
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

    public Color[] characterelementcolor;
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
    public Itemcontroller[] currentgloves;
    public Itemcontroller[] currentlegs;
    public Itemcontroller[] currentshoes;
    public Itemcontroller[] currentneckless;
    public Itemcontroller[] currentring;

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

        firstweapon = Statics.firstweapon;
        secondweapon = Statics.secondweapon;

        characterelementcolor = Statics.characterelementcolor;
        charactersecondelementcolor = Statics.charactersecondelementcolor;
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
        currentgloves = Statics.charcurrentgloves;
        currentshoes = Statics.charcurrentshoes;
        currentlegs = Statics.charcurrentlegs;
        currentneckless = Statics.charcurrentneckless;
        currentring = Statics.charcurrentring;


    /*public static float charwechselbuff = 100f;
    public static float weaponswitchbuff = 100f;
    public static int[] charactersecondelement = { 8, 8, 8, 8, 8 };       // 8 = hat noch kein element
    public static string[] characterclassrolltext = new string[5];
    public static int maincharstoneclass = 3;
    public static int secondcharstoneclass = 3;
    public static int thirdcharstoneclass = 3;
    public static int forthcharstoneclass = 3;
    public static float groupstonehealbonus = 0;
    public static float groupstonedefensebonus = 0;
    public static float groupstonedmgbonus = 0;
    public static float guardbonushpeachlvl = 10;
    public static bool thirdcharishealer;
    public static bool forthcharishealer;*/
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

        Statics.firstweapon = firstweapon;
        Statics.secondweapon = secondweapon;

        Statics.characterelementcolor = characterelementcolor;
        Statics.charactersecondelementcolor = charactersecondelementcolor;
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
        Statics.charcurrentgloves = currentgloves;
        Statics.charcurrentshoes = currentshoes;
        Statics.charcurrentlegs = currentlegs;
        Statics.charcurrentneckless = currentneckless;
        Statics.charcurrentring = currentring;
    }
}
