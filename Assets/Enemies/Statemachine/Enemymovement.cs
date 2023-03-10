using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemymovement : MonoBehaviour
{
    public NavMeshAgent Meshagent;
    public LayerMask Player;
    private Animator animator;
    [SerializeField] Enemyvalues enemyvalues;
    [NonSerialized] public EnemyHP enemyhp;

    [NonSerialized] public GameObject currenttarget;

    [NonSerialized] public float enemyresetrange = 55;
    [NonSerialized] public float checkforplayertimer;
    [NonSerialized] public float aggrorangecheck = 20;
    [NonSerialized] public float attackrange = 3f;
    [NonSerialized] public float followplayerafterattack;
    [NonSerialized] public float chancetochangeposi = 33;
    [NonSerialized] public float checkforresettimer;
    [NonSerialized] public float healticksafterreset = 1;
    [NonSerialized] public float healticktimer;
    [NonSerialized] public float healtickamount;

    [NonSerialized] public float basedmg;                //wird in enemyhp gesetzt
    [NonSerialized] public float normalattackcd;
    [NonSerialized] public float normalattacktimer;
    [NonSerialized] public bool spezialattack;

    [NonSerialized] public float patroltimer;
    [NonSerialized] public float patrolwaittimer = 5f;
    [NonSerialized] public float patrolspeed = 2f;
    [NonSerialized] public int enemeytriggerrange = 15;

    public LayerMask checkforplayerlayer;
    public LayerMask meleehitboxlayer;

    [NonSerialized] public float normalnavspeed;

    [NonSerialized] public Vector3 spawnpostion;
    [NonSerialized] public Vector3 patrolposi;
    [NonSerialized] public Vector3 posiafterattack;

    const string idlestate = "Idle";
    const string attack1state = "Attack1";

    private Enemypatrol enemypatrol = new Enemypatrol();
    private Enemyattack enemyattack = new Enemyattack();
    private Enemyreset enemyreset = new Enemyreset();

    public string currentstate;

    public State state;

    public enum State
    {
        empty,
        waitfornextpatrolpoint,
        patrol,
        gettomeleerange,
        waitforattacks,
        isattacking,
        spezialattack,
        changeposi,
        resetheal,
        idleheal,
    }

    private void Awake()
    {
        enemyhp = GetComponent<EnemyHP>();
        Meshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        enemypatrol.esm = this;
        enemyattack.esm = this;
        enemyreset.esm = this;

        spawnpostion = transform.position;

        normalnavspeed = enemyvalues.movementspeed;
        normalattackcd = enemyvalues.attackspeed;

        checkforplayerlayer =  1 << 6 | 1 << 9 | 1 << 13;
        int layerAsLayerMask = 1 << 8;
        meleehitboxlayer = layerAsLayerMask;
    }
    private void OnEnable()
    {
        currentstate = null;
        state = State.empty;
        Meshagent.ResetPath();
        ChangeAnimationState(idlestate);
        currenttarget = LoadCharmanager.Overallmainchar;
        checkforresettimer = 0;
        checkforplayertimer = 0;
        followplayerafterattack = 0;
        triggerenemy();
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.empty:
                break;
            case State.waitfornextpatrolpoint:
                enemypatrol.waitfornextpatrolpoint();
                break;
            case State.patrol:
                enemypatrol.patrol();
                break;
            case State.gettomeleerange:
                enemyreset.checkforreset();
                enemyattack.gettomeleerange();
                break;
            case State.waitforattacks:
                enemyreset.checkforreset();
                enemyattack.waitingforattacks();
                break;
            case State.changeposi:
                enemyreset.checkforreset();
                enemyattack.repositionafterattack();
                break;
            case State.isattacking:
                enemyreset.checkforreset();
                FaceTraget();
                break;
            case State.spezialattack:
                enemyreset.checkforreset();
                resetpath();
                Facemainchar();
                break;
            case State.resetheal:
                enemyreset.resetheal();
                break;
            case State.idleheal:
                enemyreset.healwhileidle();
                break;
        }
    }
    public void triggerenemy()
    {
        enemypatrol.triggerenemy();       //ontriggerenter
    }
    public void checkforplayerinrange() => enemypatrol.checkforplayerinrange();
    public void enemyinrangeistriggered() => enemypatrol.enemyinrangeistriggered();
    public void switchtoattackstate()
    {
        normalattacktimer = 0;
        ChangeAnimationState(attack1state);
        Meshagent.ResetPath();
        state = State.isattacking;
    }
    private void backtowaitforattack()
    {
        if(enemyhp.enemyisdead == false) enemyattack.backtowaitforattack();         //wird mit der animation gecalled
    }
    private void resetpath() => Meshagent.ResetPath();
    private void callemptystate() => state = State.empty;                          //wird mit der animation gecalled
    private void normalattackdmg() => currenttarget.GetComponent<Playerhp>().TakeDamage(basedmg);                    //wird mit der animation gecalled
    public void enemydied() => enemyreset.enemydied();
    public void FaceTraget()
    {
        Vector3 direction = (currenttarget.transform.position - transform.position).normalized;                    // normalized wegen magnitude
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));               //LookRotation reicht um die rotation zu bestimmmen + extra schritt das sich das objekt nur in x und z dreht
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);              //Slerp wird benutzt damit das Objekt sich in einer bestimmen geschwindikeit dreht (sonst w?rde sich das objekt instant drehen)
    }
    private void Facemainchar()
    {
        Vector3 direction = (LoadCharmanager.Overallmainchar.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void ChangeAnimationState(string newstate)
    {
        if (currentstate == newstate) return;
        animator.CrossFadeInFixedTime(newstate, 0.1f);
        currentstate = newstate;
    }
}
