using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fistattack : MonoBehaviour
{
    private SpielerSteu controlls;
    private Animator animator;
    private Movescript movementscript;
    private Fistcontroller fistcontroller;
    private Healingscript healingscript;
    private EleAbilities eleAbilities;

    private bool root;
    private float basicattackcd;
    public int combochain;

    private bool readattackinput;
    private bool down;
    private bool mid;
    private bool up;

    //animations
    const string groundbasic1state = "Fistbasic";
    const string groundbasic2state = "Fistbasic2";
    const string groundbasic3state = "Fistbasic3";
    const string grounddownstate = "Fistdownend";
    const string groundmidstate = "Fistmidend";
    const string groundupstate = "Fistupend";
    const string airbasic1state = "Fistair1";
    const string airbasic2state = "Fistair2";
    const string airdownstate = "Fistair3down";
    const string airmidstate = "Fistair3mid";
    const string airupstate = "Fistair3up";
    const string swordswitchstate = "Fistweaponswitch";
    const string dashstate = "Fistdash";

    //weaponswitch
    private float checkforenemyonswitchrange = 3f;
    public LayerMask weaponswitchlayer;

    void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        movementscript = GetComponent<Movescript>();
        animator = GetComponent<Animator>();
        fistcontroller = GetComponent<Fistcontroller>();
        healingscript = GetComponent<Healingscript>();
        eleAbilities = GetComponent<EleAbilities>();
    }
    private void OnEnable()
    {
        controlls.Enable();
        root = false;
        movementscript.currentstate = null;
        basicattackcd = 1f;
        fistcontroller.enabled = true;
        combochain = 0;
        readattackinput = false;
        attackestate = Attackstate.weaponswitch;
        StartCoroutine(startweaponswitch());
    }

    private Attackstate attackestate;
    enum Attackstate
    {
        waitforattack,
        attack1,
        attack2,
        attack3,
        attackchain,
        groundintoair,                                // eine zusätzliche chain, bei ground into air
        weaponswitch,
    }
    void Update()
    {
        if (LoadCharmanager.disableattackbuttons == false)
        {
            switch (attackestate)
            {
                case Attackstate.waitforattack:
                    waitforattackinput();
                    break;
                case Attackstate.attack1:
                    attack1input();
                    break;
                case Attackstate.attack2:
                    attack2input();
                    break;
                case Attackstate.attack3:
                    attack3input();
                    break;
                case Attackstate.attackchain:
                    airintogroundinput();
                    break;
                case Attackstate.groundintoair:
                    groundintoairinput();
                    break;
                case Attackstate.weaponswitch:
                    break;
                default:
                    break;
            }
            if (Statics.dazestunstart == true)                                //reset alles values bei stun
            {
                Statics.dazestunstart = false;
                Statics.playeriframes = false;
                resetvalues();
                movementscript.Charrig.enabled = false;
                if (Statics.enemyspezialtimescale == false)
                {
                    Time.timeScale = Statics.normalgamespeed;
                    Time.fixedDeltaTime = Statics.normaltimedelta;
                }
            }
            if (controlls.Player.Dash.WasPerformedThisFrame() && Statics.dashcdmissingtime > Statics.dashcost && Statics.dash == false)
            {
                movementscript.state = Movescript.State.Beforedash;                  //damit man beim angreifen noch die Richtung bestimmen kann
                Statics.dash = true;
                Statics.playeriframes = true;
                resetvalues();
                GlobalCD.startdashcd();
                movementscript.graviti = 0;
                Invoke("dash", 0.05f);                                             //damit man beim angreifen noch die Richtung bestimmen kann
            }
        }
    }

    private void waitforattackinput()
    {
        basicattackcd += Time.deltaTime;
        if (movementscript.state == Movescript.State.Ground)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame() && basicattackcd > 0.5f && Statics.otheraction == false)
            {
                movementscript.state = Movescript.State.Groundattack;
                attackestate = Attackstate.attack1;
                Statics.otheraction = true;
                movementscript.ChangeAnimationState(groundbasic1state);
                readattackinput = false;
                combochain = 0;
            }
        }
        else if (movementscript.state == Movescript.State.Air)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame() && movementscript.airattackminheight == true && movementscript.attackonceair == true && Statics.otheraction == false)// && Statics.infight == true)
            {
                movementscript.state = Movescript.State.Airattack;
                attackestate = Attackstate.attack1;
                movementscript.graviti = 0f;
                Statics.otheraction = true;
                movementscript.attackonceair = false;
                combochain = 0;
                readattackinput = false;
                movementscript.ChangeAnimationState(airbasic1state);
            }
        }
    }
    private void attack1input()
    {
        if (readattackinput == true)
        {
            if (controlls.Player.Attack2.WasPressedThisFrame())
            {
                readattackinput = false;
            }
        }
    }
    private void attack2input()
    {
        if (readattackinput == true)
        {
            if (controlls.Player.Attack2.WasPressedThisFrame())
            {
                readattackinput = false;
            }
        }
    }
    private void attack3input()
    {
        if (readattackinput == true)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame())
            {
                combochain++;
                down = true;
                readattackinput = false;
            }
            else if (controlls.Player.Attack2.WasPressedThisFrame())
            {
                combochain++;
                mid = true;
                readattackinput = false;
            }
            else if (controlls.Player.Attack3.WasPressedThisFrame())
            {
                combochain++;
                up = true;
                readattackinput = false;
            }
        }
    }
    private void groundintoairinput()
    {
        if (readattackinput == true)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame())
            {
                readattackinput = false;
            }
        }
    }
    private void airintogroundinput()
    {
        if (readattackinput == true && combochain < 2)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame())
            {
                readattackinput = false;
            }
        }
    }
    private void OnAnimatorMove()
    {
        if (root == true)
        {
            Vector3 velocity = animator.deltaPosition;
            movementscript.charactercontroller.Move(velocity);
        }
    }

    private void resetvalues()
    {
        attackestate = Attackstate.waitforattack;
        Statics.otheraction = true;
        root = true;
        CancelInvoke();
        healingscript.resethealvalues();
        eleAbilities.resetelementalmovementvalues();
        eleAbilities.icelanceiscanceled();
    }
    private void dash()
    {
        movementscript.state = Movescript.State.Empty;
        movementscript.ChangeAnimationStateInstant(dashstate);
    }
    private void fistdashend()
    {
        root = false;
        Statics.playeriframes = false;
        Statics.otheraction = false;
        GlobalCD.startresetdash();
        movementscript.switchtoairstate();
    }
    private void setinputtotrue()
    {
        readattackinput = true;
    }
    private void fistgroundattackchainend()
    {
        readattackinput = false;
        movementscript.switchtogroundstate();
        attackestate = Attackstate.waitforattack;
        Statics.otheraction = false;
        combochain = 0;
        basicattackcd = 0;
    }
    private void fistgroundbasicend()
    {
        if (readattackinput == true) fistgroundattackchainend();
        else
        {
            down = false;
            mid = false;
            up = false;
            attackestate = Attackstate.attack2;
            movementscript.ChangeAnimationState(groundbasic2state);
        }
    }
    private void fistgroundbasic2end()
    {
        if (readattackinput == true) fistgroundattackchainend();
        else
        {
            attackestate = Attackstate.attack3;
            movementscript.ChangeAnimationState(groundbasic3state);
        }
    }
    private void fistgroundbasic3end()
    {
        if (readattackinput == true) fistgroundattackchainend();
        else
        {
            if (down == true)
            {
                attackestate = Attackstate.attackchain;
                if (down) movementscript.ChangeAnimationState(grounddownstate);
            }
            else if (mid == true)
            {
                attackestate = Attackstate.attackchain;
                movementscript.ChangeAnimationState(groundmidstate);
            }
            else if (up == true)
            {
                attackestate = Attackstate.groundintoair;
                movementscript.ChangeAnimationState(groundupstate);
            }
        }
    }
    private void fiststaygroundend()
    {
        if (readattackinput == true) fistgroundattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
    }
    private void fistgrounduproot()
    {
        movementscript.attackonceair = false;
        root = true;
        movementscript.state = Movescript.State.Airattack;
        movementscript.graviti = 0f;
    }
    private void fistgroundupend()
    {
        root = false;
        if (readattackinput == true) fistairattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(airbasic1state);
        }
    }

    private void fistairattackchainend()
    {
        readattackinput = false;
        movementscript.switchtoairstate();
        attackestate = Attackstate.waitforattack;
        Statics.otheraction = false;
        combochain = 0;
        basicattackcd = 0;
    }
    private void fistbasicairend()
    {
        if (readattackinput == true) fistairattackchainend();
        else
        {
            down = false;
            mid = false;
            up = false;
            attackestate = Attackstate.attack3;
            movementscript.ChangeAnimationState(airbasic2state);
        }
    }
    private void fistairbasic2end()
    {
        if (readattackinput == true) fistairattackchainend();
        else
        {
            attackestate = Attackstate.attackchain;
            if (down) movementscript.ChangeAnimationState(airdownstate);
            else if (mid) movementscript.ChangeAnimationState(airmidstate);
            else if (up) movementscript.ChangeAnimationState(airupstate);
        }
    }
    private void fistairdownroot()
    {
        root = true;
    }
    private void fistairdownend()
    {
        root = false;
        if (readattackinput == true) fistgroundattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.graviti = -0.5f;
            movementscript.state = Movescript.State.Groundattack;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
    }
    private void fiststayairend()
    {
        if (readattackinput == true) fistairattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(airbasic1state);
        }
    }
    IEnumerator startweaponswitch()
    {
        yield return null;
        fistweaponswitch();
    }
    private void fistweaponswitch()
    {
        if (Physics.CheckSphere(transform.position, checkforenemyonswitchrange, weaponswitchlayer))
        {
            Statics.otheraction = true;
            movementscript.graviti = -5;
            movementscript.state = Movescript.State.Airattack;
            movementscript.ChangeAnimationState(swordswitchstate);
        }
        else
        {
            attackestate = Attackstate.waitforattack;
        }
    }
    private void fistweaponswitchend()
    {
        attackestate = Attackstate.waitforattack;
        movementscript.switchtogroundstate();
        Statics.otheraction = false;
    }
}
