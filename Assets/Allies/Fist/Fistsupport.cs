using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fistsupport : MonoBehaviour
{
    public GameObject righthand;
    public GameObject rightfoot;
    public LayerMask enemylayer;

    private float basicfistdmg = 1;
    private float endfistdmg = 3;

    private float enddmgtodeal;
    private float basicdmgtodeal;

    private Attributecontroller attributecontroller;
    private Playerhp hpscript;

    private void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>();
        hpscript = GetComponent<Playerhp>();
    }

    private void OnEnable()
    {
        fistdmgupdate();
    }
    private void fistdmgupdate()
    {
        basicdmgtodeal = Damagecalculation.calculateplayerdmgdone(basicfistdmg, attributecontroller.dmgfromallies, attributecontroller.fistattack, attributecontroller.stoneclassbonusdmg);
        enddmgtodeal = Damagecalculation.calculateplayerdmgdone(endfistdmg, attributecontroller.dmgfromallies, attributecontroller.fistattack, attributecontroller.stoneclassbonusdmg);
    }

    private void firstfistattack()
    {
        Collider[] cols = Physics.OverlapSphere(righthand.transform.position, 2f, enemylayer);

        foreach (Collider Enemyhit in cols)
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }
        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;

                    if (gameObject == LoadCharmanager.Overallthirdchar)
                    {
                        enemyscript.tookdmgfrom(3, Statics.thirdchartookdmgformamount);
                    }
                    if (gameObject == LoadCharmanager.Overallforthchar)
                    {
                        enemyscript.tookdmgfrom(4, Statics.forthchartookdmgformamount);
                    }
                    enemyscript.takesupportdmg(basicdmgtodeal);
                }

            }
    }
    private void secondfistattack()
    {
        Collider[] cols = Physics.OverlapSphere(rightfoot.transform.position, 3f, enemylayer);

        foreach (Collider Enemyhit in cols)
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }

        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;

                    if (gameObject == LoadCharmanager.Overallthirdchar)
                    {
                        enemyscript.tookdmgfrom(3, Statics.thirdchartookdmgformamount);
                    }
                    if (gameObject == LoadCharmanager.Overallforthchar)
                    {
                        enemyscript.tookdmgfrom(4, Statics.forthchartookdmgformamount);
                    }
                    enemyscript.takesupportdmg(basicdmgtodeal);
                }

            }
    }
    private void thirdfistattack()
    {
        hpscript.playerheal(4);
        Collider[] cols = Physics.OverlapSphere(rightfoot.transform.position, 3f, enemylayer);

        foreach (Collider Enemyhit in cols)
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }

        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;

                    if (gameObject == LoadCharmanager.Overallthirdchar)
                    {
                        enemyscript.tookdmgfrom(3, Statics.thirdchartookdmgformamount);
                    }
                    if (gameObject == LoadCharmanager.Overallforthchar)
                    {
                        enemyscript.tookdmgfrom(4, Statics.forthchartookdmgformamount);
                    }
                    enemyscript.takesupportdmg(enddmgtodeal);
                }
            }
            
    }
}
                
        

