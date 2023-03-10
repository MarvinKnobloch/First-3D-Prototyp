using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fistcontroller : MonoBehaviour
{
    public Transform righthand;
    public Transform lefthand;
    public GameObject rightfeet;
    public GameObject body;
    private float chainbasicdmg = 6;
    private float chainenddmg = 7;
    private float air3middmg = 4;
    private float weaponswitchdmg = 10f;
    public GameObject damagetext;
    public LayerMask Layerhitbox;
    private float dmgdealed;

    private float overallbasicdmg;
    private float overallenddmg;
    private float overallair3middmg;
    private float overallcritchance;
    private float basicweaponswitchdmg;

    private bool crit;

    private float enemydebuffcrit;

    private float weaponhealing;

    private float basicmanarestore = 2;
    private float endmanarestore = 5;

    public GameObject charmanager;

    private Manamanager manacontroller;
    private Attributecontroller attributecontroller;
    private Playerhp spielerhp;

    private void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>();
        manacontroller = charmanager.GetComponent<Manamanager>();
        spielerhp = GetComponent<Playerhp>();
    }
    private void OnEnable()
    {
        LoadCharmanager.fistcontrollerupdate -= fistdmgupdate;
        fistdmgupdate();
    }
    private void OnDisable()
    {
        LoadCharmanager.fistcontrollerupdate -= fistdmgupdate;
    }
    public void fistdmgupdate()
    {
        overallbasicdmg = Globalplayercalculations.calculateplayerdmgdone(chainbasicdmg, attributecontroller.attack, attributecontroller.fistattack, attributecontroller.stoneclassbonusdmg);
        overallenddmg = Globalplayercalculations.calculateplayerdmgdone(chainenddmg, attributecontroller.attack, attributecontroller.fistattack, attributecontroller.stoneclassbonusdmg);
        overallair3middmg = Globalplayercalculations.calculateplayerdmgdone(air3middmg, attributecontroller.attack, attributecontroller.fistattack, attributecontroller.stoneclassbonusdmg);
        basicweaponswitchdmg = Globalplayercalculations.calculateplayerdmgdone(weaponswitchdmg, attributecontroller.attack, attributecontroller.fistattack, attributecontroller.stoneclassbonusdmg);

        overallcritchance = Statics.playerbasiccritchance + attributecontroller.critchance;

        weaponhealing = Globalplayercalculations.calculateweaponheal(attributecontroller.maxhealth);
    }

    public void fistrighthandhitbox()
    {
        lookfordmgcollision(righthand.transform.position, 2, overallbasicdmg, 0, basicmanarestore);
    }
    public void fistlefthandhitbox()
    {
        lookfordmgcollision(lefthand.transform.position, 2, overallbasicdmg, 0, basicmanarestore);
    }
    public void fistfeethitbox()
    {
        lookfordmgcollision(rightfeet.transform.position, 2, overallbasicdmg, 0, basicmanarestore);
    }
    public void Checkfistdownfeethitbox()
    {
        lookfordmgcollision(rightfeet.transform.position, 2, overallenddmg, 1, endmanarestore);
    }
    public void Checkfistdownhandhitbox()
    {
        lookfordmgcollision(lefthand.transform.position, 2, overallenddmg, 1, endmanarestore);
    }
    public void Checkfistmidfeethitbox()
    {
        lookfordmgcollision(rightfeet.transform.position, 2, overallenddmg, 2, endmanarestore);
    }
    public void Checkfistmidbodyhitbox()
    {
        lookfordmgcollision(body.transform.position, 2, overallair3middmg, 2, endmanarestore);
    }
    public void Checkfistupfeethitbox()
    {
        lookfordmgcollision(rightfeet.transform.position, 2, overallenddmg, 3, endmanarestore);
    }
    public void Fistweaponswitchhitbox()
    {
        lookfordmgcollision(righthand.transform.position, 4, basicweaponswitchdmg, 0, endmanarestore);
    }
    private void lookfordmgcollision(Vector3 hitposition, float hitrange, float damage, int dmgtype, float manarestore)
    {
        Collider[] cols = Physics.OverlapSphere(hitposition, hitrange, Layerhitbox);
        foreach (Collider enemyhit in cols)
        {
            if (enemyhit.isTrigger)               //damit nur die meleehitbox getriggered wird
            {
                if (enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
                {
                    enemyscript.tookdmgfrom(1, Statics.playertookdmgfromamount);
                    calculatecritchance(enemyscript, damage);
                    enemyscript.takeplayerdamage(dmgdealed, dmgtype, crit);
                }
            }
        }
        if (cols.Length > 0)
        {
            healandmana(dmgtype, manarestore);
        }
    }
    private void calculatecritchance(EnemyHP enemyscript, float dmg)
    {
        if (enemyscript.enemydebuffcd == true)
        {
            enemydebuffcrit = attributecontroller.basicattributecritbuff;
        }
        else
        {
            enemydebuffcrit = 0;
        }
        if (Random.Range(0, 100) < overallcritchance + enemydebuffcrit)
        {
            crit = true;
            float switchbuffdmg = Globalplayercalculations.calculateweaponcharbuff(dmg);
            dmgdealed = Mathf.Round(dmg * (attributecontroller.critdmg / 100f) + switchbuffdmg);
        }
        else
        {
            crit = false;
            float switchbuffdmg = Globalplayercalculations.calculateweaponcharbuff(dmg);
            dmgdealed = Mathf.Round(dmg + switchbuffdmg);
        }
    }
    private void healandmana(int type, float manarestore)
    {
        manacontroller.Managemana(manarestore);
        if (type == 0)
        {
            return;
        }
        else
        {
            spielerhp.addhealth(weaponhealing);
        }
    }

}

