using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Supportmeleeattack
{
    public Supportmovement ssm;
    private float sideposition;

    const string idlestate = "Idle";
    const string runstate = "Run";
    const string attack1state = "Attack1";
    const string attack2state = "Attack2";
    const string attack3state = "Attack3";
    public void waitforattackcd()
    {
        if (ssm.currenttarget != null)
        {
            ssm.supportreset();
            ssm.FaceTraget();
            ssm.attacktimer += Time.deltaTime;
            if(ssm.attacktimer > ssm.attackcd)
            {
                if (Vector3.Distance(ssm.transform.position, ssm.currenttarget.transform.position) > ssm.attackrangecheck)
                {
                    ssm.ChangeAnimationState(runstate);
                    ssm.Meshagent.SetDestination(ssm.currenttarget.transform.position);
                }
                else
                {
                    ssm.state = Supportmovement.State.attackstate;
                    ssm.ChangeAnimationState(attack1state);
                    ssm.Meshagent.ResetPath();
                }
            }
        }
        else
        {
            ssm.switchtarget();
        }
    }
    public void waitingformeleeattack()
    {
        if (ssm.currenttarget != null)
        {
            ssm.supportreset();
            ssm.FaceTraget();
            ssm.attacktimer += Time.deltaTime;
            if (ssm.attacktimer > ssm.attackcd)
            {
                ssm.state = Supportmovement.State.attackstate;
                ssm.ChangeAnimationState(attack1state);
                ssm.Meshagent.ResetPath();
                return;
            }
            ssm.followenemytimer += Time.deltaTime;
            if(ssm.followenemytimer > 0.3f)
            {
                if (ssm.currenttarget.gameObject.GetComponent<Enemymovement>().currenttarget != ssm.gameObject)
                {
                    Vector3 newposi = ssm.currenttarget.transform.position + ssm.currenttarget.transform.forward * -2 + ssm.currenttarget.transform.right * sideposition;
                    if (Vector3.Distance(ssm.transform.position, newposi) > ssm.attackrangecheck)
                    {
                        ssm.ChangeAnimationState(runstate);
                        ssm.Meshagent.SetDestination(newposi);
                    }
                    else ssm.ChangeAnimationState(idlestate);
                }
                else
                {
                    if (Vector3.Distance(ssm.transform.position, ssm.currenttarget.transform.position + ssm.transform.forward * -2) > ssm.attackrangecheck)
                    {
                        ssm.ChangeAnimationState(runstate);
                        ssm.Meshagent.SetDestination(ssm.currenttarget.transform.position + ssm.transform.forward * -2);
                    }
                    else ssm.ChangeAnimationState(idlestate);
                    ssm.followenemytimer = 0;
                }
            }
        }
        else
        {
            ssm.switchtarget();
        }
    }
    public void meleeattack1end()
    {
        attackrangecheck(attack2state);
    }
    public void meleeattack2end()
    {
        attackrangecheck(attack3state);
    }
    public void meleeattack3end()
    {
        afterattackaction();
    }
    private void afterattackaction()
    {
        ssm.attacktimer = 0f;
        if (ssm.currenttarget != null && ssm.playerhp.playerisdead == false)
        {
            int newposi = Random.Range(0, 100);
            if (newposi < ssm.chancetochangeposi)
            {
                ssm.posiafterattack = ssm.currenttarget.transform.position + Random.insideUnitSphere * 5;
                ssm.posiafterattack.y = ssm.transform.position.y;
                NavMeshHit hit;
                bool blocked;
                blocked = NavMesh.Raycast(ssm.transform.position, ssm.posiafterattack, out hit, NavMesh.AllAreas);
                if (blocked == true)
                {
                    ssm.posiafterattack = hit.position;
                    ssm.Meshagent.SetDestination(ssm.posiafterattack);
                    ssm.ChangeAnimationState(runstate);
                    ssm.state = Supportmovement.State.changeposiafterattack;                       //wenn nach dem attacken eine neue posi gesucht wird bleibt der char an der posi stehen bis er attacken kann
                }
                else
                {
                    ssm.Meshagent.SetDestination(ssm.posiafterattack);
                    ssm.ChangeAnimationState(runstate);
                    ssm.state = Supportmovement.State.changeposiafterattack;
                }
            }
            else
            {
                ssm.ChangeAnimationState(idlestate);
                sideposition = Random.Range(-2f, 2f);
                ssm.state = Supportmovement.State.waitformeleeattack;           //wenn nach dem attacken keine neue posi gesucht wird folgt der enemy dem target
            }
        }
    }
    private void attackrangecheck(string state)
    {
        if (ssm.currenttarget != null && ssm.playerhp.playerisdead == false)
        {
            if (Vector3.Distance(ssm.transform.position, ssm.currenttarget.transform.position + ssm.transform.forward * -2) > ssm.attackrangecheck)
            {
                afterattackaction();
            }
            else
            {
                ssm.ChangeAnimationState(state);
            }
        }
    }
}
