using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Playerhp : MonoBehaviour
{
    [SerializeField] private int charnumber;
    public float health;
    public float maxhealth;
    [NonSerialized] public bool playerisdead;
    public int playerhpuislot;
    [SerializeField] private Healthuimanager healthUImanager;
    private Attributecontroller attributecontroller;

    public static event Action triggergameover;

    const string dyingstate = "Dying";

    void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>(); 
    }
    private void OnEnable()
    {
        if(playerisdead == true)
        {
            playerisdead = false;
            addhealth(1);
        }

    }
    public void TakeDamage(float damage)
    {
        if (Statics.playeriframes == true && gameObject == LoadCharmanager.Overallmainchar)
        {
            return;
        }

        float dmgtodeal = Mathf.Round(damage - ((Statics.groupstonedefensebonus + attributecontroller.stoneclassdmgreduction + (attributecontroller.defense / 40)) * 0.01f * damage));
        health -= dmgtodeal;
        handlehealth();
    }
    public void addhealthwithtext(float heal)
    {
        health += Mathf.Round(heal);
        Floatingnumberscontroller.floatingnumberscontroller.activatenumbers(this.gameObject, heal, Color.green);
        handlehealth();
    }

    public void addhealth(float heal)
    {
        health += Mathf.Round(heal);
        handlehealth();
    }
    public void playerisresurrected()
    {
        gameObject.GetComponent<Supportmovement>().supportresurrected();
        foreach (GameObject obj in Infightcontroller.infightenemylists)
        {
            obj.GetComponent<EnemyHP>().playerisresurrected(playerhpuislot - 1);
        }      
    }
    private void handlehealth()
    {
        if(playerisdead == false)
        {
            if (health > maxhealth)
            {
                health = maxhealth;
            }
            if (health <= 0)
            {
                playerisdead = true;
                health = 0;
                if (playerhpuislot == 0)           //0 ist immer mainchar
                {
                    Statics.resetvaluesondeathorstun = true;
                    LoadCharmanager.disableattackbuttons = true;
                    gameObject.GetComponent<Movescript>().ChangeAnimationStateInstant(dyingstate);
                    gameObject.GetComponent<Movescript>().state = Movescript.State.Empty;
                    if (Statics.oneplayerisdead == false)
                    {
                        Statics.oneplayerisdead = true;
                        GlobalCD.startsupportresurrectioncd();
                    }
                    foreach (GameObject obj in Infightcontroller.infightenemylists)
                    {
                        obj.GetComponent<EnemyHP>().newtargetonplayerdeath(playerhpuislot);
                    }
                }
                else
                {
                    gameObject.GetComponent<Supportmovement>().ChangeAnimationStateInstant(dyingstate);                  //wenn ich die 2 sachen ?ber eine funktion call werden sie manchmal ?berschrieben und dann wird die empty state ?berschrieben
                    gameObject.GetComponent<Supportmovement>().state = Supportmovement.State.empty;
                    if (Statics.oneplayerisdead == false)
                    {
                        Statics.oneplayerisdead = true;
                        GlobalCD.startsupportresurrectioncd();
                    }
                    foreach (GameObject obj in Infightcontroller.infightenemylists)
                    {
                        obj.GetComponent<EnemyHP>().newtargetonplayerdeath(playerhpuislot - 1);
                    }
                }
                checkforgameover();
            }
            Statics.charcurrenthealth[charnumber] = health;
            healthUImanager.healthupdate(playerhpuislot, health, maxhealth);
        }
    }
    private void checkforgameover()
    {
        if(LoadCharmanager.Overallmainchar.gameObject.GetComponent<Playerhp>().playerisdead == true && LoadCharmanager.Overallthirdchar.gameObject.GetComponent<Playerhp>().playerisdead == true && LoadCharmanager.Overallforthchar.gameObject.GetComponent<Playerhp>().playerisdead == true)
        {
            triggergameover?.Invoke();
        }
    }
}

