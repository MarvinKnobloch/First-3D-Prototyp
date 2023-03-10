using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opensupportchar : MonoBehaviour
{
    public GameObject thirdcharselection;
    public bool thirdcharselectionactive;
    public GameObject[] thirdselectionslots;

    public GameObject forthcharselection;
    public bool forthcharselectionactive;
    public GameObject[] forthselectionslots;

    private void OnEnable()
    {
        thirdcharselectionactive = false;
        thirdcharselection.SetActive(false);

        forthcharselectionactive = false;
        forthcharselection.SetActive(false);
    }
    public void openthirdcharselection()
    {
        if(thirdcharselectionactive == false)
        {
            thirdcharselectionactive = true;
            thirdcharselection.SetActive(true);
            forthcharselectionactive = false;
            forthcharselection.SetActive(false);
            foreach (GameObject slot in thirdselectionslots)
            {
                slot.SetActive(true);
            }
        }
        else
        {
            thirdcharselectionactive = false;
            thirdcharselection.SetActive(false);
        }
    }
    public void openforthcharselection()
    {
        if(forthcharselectionactive == false)
        {
            forthcharselectionactive = true;
            forthcharselection.SetActive(true);
            thirdcharselectionactive = false;
            thirdcharselection.SetActive(false);

            foreach (GameObject slot in forthselectionslots)
            {
                slot.SetActive(true);
            }
        }
        else
        {
            forthcharselectionactive = false;
            forthcharselection.SetActive(false);
        }
    }
}
