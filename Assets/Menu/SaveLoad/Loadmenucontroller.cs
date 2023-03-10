using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Loadmenucontroller : MonoBehaviour
{
    private SpielerSteu steuerung;
    [SerializeField] private GameObject menuobj;
    [SerializeField] private GameObject commitloadobj;
    [SerializeField] private TextMeshProUGUI[] slotempty;
    [SerializeField] private TextMeshProUGUI[] charlvl;
    [SerializeField] private TextMeshProUGUI[] savedate;
    public int selectedslot;

    private void Awake()
    {
        steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        steuerung.Enable();
        commitloadobj.SetActive(false);
        setslots();
    }
    private void Update()
    {
        if (steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame() && commitloadobj.activeSelf == false)
        {
            menuobj.SetActive(true);
            gameObject.SetActive(false);
        }
        if (steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame() && commitloadobj.activeSelf == true)
        {
            closecommitload();
        }
    }
    public void setslots()
    {
        for (int i = 0; i < Slotvaluesarray.slotisnotempty.Length; i++)
        {
            if (Slotvaluesarray.slotisnotempty[i] == false)
            {
                slotempty[i].text = "Empty Slot";
                charlvl[i].text = "";
                savedate[i].text = "";
            }
            else
            {
                slotempty[i].text = "";
                charlvl[i].text = "PlayerLvL " + Slotvaluesarray.slotlvl[i].ToString();
                savedate[i].text = Slotvaluesarray.slotdate[i];
                savedate[i].text += "\n " + Slotvaluesarray.slottime[i];
            }
        }
    }
    public void opencommitload(int slot)
    {
        if (Slotvaluesarray.slotisnotempty[slot -1] == true)
        {
            commitloadobj.SetActive(true);
            selectedslot = slot -1;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    public void closecommitload()
    {
        commitloadobj.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
