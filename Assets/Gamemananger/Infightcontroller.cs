using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Infightcontroller : MonoBehaviour
{
    static MonoBehaviour instance;

    public GameObject setinfightimage;
    public static GameObject infightimage;
    public static List<GameObject> infightenemylists = new List<GameObject>();

    public static float teammatesdespawntime = 5;

    public float spezialtimer;


    private void Start()
    {
        instance = this;
        infightimage = setinfightimage;
        checkifinfight();
        instance.CancelInvoke();
    }
    public static void checkifinfight()
    {
        if (infightenemylists.Count == 0)
        {
            Statics.infight = false;
            Statics.currentenemyspecialcd = Statics.enemyspecialcd;
            instance.StopCoroutine("enemyspezialcd");
            infightimage.SetActive(false);
            if (LoadCharmanager.Overallthirdchar != null)                               
            {
                instance.Invoke("disablethirdchar", teammatesdespawntime);
            }
            if (LoadCharmanager.Overallforthchar != null)
            {
                instance.Invoke("disableforthchar", teammatesdespawntime);
            }
        }
        else
        {
            GameObject mainchar = LoadCharmanager.Overallmainchar;
            if (Statics.infight == false)
            {
                Statics.infight = true;
                instance.StartCoroutine("enemyspezialcd");
            }
            infightimage.SetActive(true);
            instance.CancelInvoke();                        //unterbricht den Allie despawn wenn man wieder infight kommmt
            if (LoadCharmanager.Overallthirdchar !=null && LoadCharmanager.Overallthirdchar.activeSelf == false)                              
            {
                LoadCharmanager.Overallthirdchar.transform.position = mainchar.transform.position + mainchar.transform.forward * -1 + mainchar.transform.right * -1;
                LoadCharmanager.Overallthirdchar.SetActive(true);
            }
            if (LoadCharmanager.Overallforthchar != null && LoadCharmanager.Overallforthchar.activeSelf == false)
            {
                LoadCharmanager.Overallforthchar.transform.position = mainchar.transform.position + mainchar.transform.forward * -1 + mainchar.transform.right * 1;
                LoadCharmanager.Overallforthchar.SetActive(true);
            }
        }
    }
    public void disablethirdchar()
    {
        if(LoadCharmanager.Overallthirdchar.TryGetComponent(out Playerhp playerhp))
        {
            playerhp.playerisdead = false;
            playerhp.addhealth(Mathf.Round(Statics.charmaxhealth[Statics.currentthirdchar] * 0.1f));
        }
        LoadCharmanager.Overallthirdchar.SetActive(false);
    }
    public void disableforthchar()
    {
        if (LoadCharmanager.Overallforthchar.TryGetComponent(out Playerhp playerhp))
        {
            playerhp.playerisdead = false;
            playerhp.addhealth(Mathf.Round(Statics.charmaxhealth[Statics.currentforthchar] * 0.1f));
        }
        LoadCharmanager.Overallforthchar.SetActive(false);
    }
    IEnumerator enemyspezialcd()
    {
        while (true)
        {
            yield return new WaitForSeconds(Statics.currentenemyspecialcd);
            int enemycount = infightenemylists.Count;
            //Debug.Log(Statics.currentenemyspecialcd);
            int enemyonlist = UnityEngine.Random.Range(1, enemycount +1);
            if (infightenemylists[enemyonlist - 1].GetComponent<Enemymovement>())
            {
                infightenemylists[enemyonlist - 1].GetComponent<Enemymovement>().spezialattack = true;
            }
        }
    }
}
