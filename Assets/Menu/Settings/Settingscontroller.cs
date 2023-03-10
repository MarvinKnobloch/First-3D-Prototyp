using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Settingscontroller : MonoBehaviour
{
    [SerializeField] private GameObject menuobj;
    [SerializeField] private GameObject[] buttons;
    private int currentbutton;
    private SpielerSteu steuerung;

    public Color selectedcolor;
    public Color notselectedcolor;
    private void Awake()
    {
        steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Image>().color = notselectedcolor;
            button.GetComponent<Settingsbuttoncontroller>().buttonobjclose();
        }
        steuerung.Enable();
        currentbutton = 0;
        EventSystem.current.SetSelectedGameObject(buttons[currentbutton]);
        buttons[currentbutton].GetComponent<Image>().color = selectedcolor; 
        buttons[currentbutton].GetComponent<Settingsbuttoncontroller>().buttonobjopen();
    }

    private void Update()
    {
        if (steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            buttons[currentbutton].GetComponent<Settingsbuttoncontroller>().buttonobjclose();
            menuobj.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void selectsetting(int buttonclicked)
    {
        buttons[currentbutton].GetComponent<Settingsbuttoncontroller>().buttonobjclose();
        buttons[currentbutton].GetComponent<Image>().color = notselectedcolor;
        currentbutton = buttonclicked;
        EventSystem.current.SetSelectedGameObject(buttons[currentbutton]);
        buttons[currentbutton].GetComponent<Image>().color = selectedcolor;
        buttons[currentbutton].GetComponent<Settingsbuttoncontroller>().buttonobjopen();
    }
}
