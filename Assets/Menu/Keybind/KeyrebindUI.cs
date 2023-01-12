using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyrebindUI : MonoBehaviour
{
    [SerializeField]
    private InputActionReference inputActionReference;                                 // Gew�hrt zugriff auf die SpielerSteu(Input Action Asset) und dann kann nach den verschiedenen actionen gesucht werden
    [Range(0, 4)]
    [SerializeField]
    private int selectedBinding;                                                       // falls die Actionen mehrere Keybinds hat (0, 4) = 4 verschiedene keybinds
    [SerializeField]
    private InputBinding.DisplayStringOptions displayStringOptions;
    [Header("Diese Werte niemals �ndern")]
    [SerializeField]
    private InputBinding inputBinding;
    private int bindingindex;                                                          // bindingindex ist der int f�r selectedBinding

    private string actionname;                                                         // der Hotkey wird hier im string gespeichtert und dann in das Inputscript gesendet

    [Header("UI Felder")]
    //[SerializeField]
    //private Button Keybindchangebutton;
    [SerializeField]
    public Text Keybindtext;
    [SerializeField]
    private Button resetbutton;

    public GameObject cantclicklayer;

    private void OnEnable()
    {
        cantclicklayer.SetActive(false);
        //Keybindchangebutton.onClick.AddListener(() => changekeybinding());                                  // aktiviert die buttons f�r changekeybind
        resetbutton.onClick.AddListener(() => resetkeybinding());                                           // und resetbutton
        Keybindinputmanager.keyrebindfinished += updatebindingUI;
        Keybindinputmanager.keyrebindcanceled += updatebindingUI;
        Keybindinputmanager.disablecantclicklayer += cantclicklayerdisable;

        if (inputActionReference != null)
        {
            getbindinginfo();
            Keybindinputmanager.loadbindingsoverride(actionname);
            updatebindingUI();
        }
    }
    private void OnDisable()
    {
        Keybindinputmanager.keyrebindfinished -= updatebindingUI;
        Keybindinputmanager.keyrebindcanceled -= updatebindingUI;
        Keybindinputmanager.disablecantclicklayer -= cantclicklayerdisable;
    }

    private void OnValidate()                                                           // Wird au�erhalb vom Playmodusgecalled, immer dann wenn ein Wert im Inspector ge�ndert wird
    {
        if (inputActionReference == null)
            return;
        getbindinginfo();
        updatebindingUI();
    }
    /*private void Start()
    {
        getbindinginfo();
        updatebindingUI();
    }*/
    private void getbindinginfo()
    {
        if(inputActionReference.action != null)
        {
            actionname = inputActionReference.action.name;                              // durchsucht die Hotkeys/Actionen und setzt dann String 
        }
        if (inputActionReference.action.bindings.Count > selectedBinding)              // eher unwichtig, weil ich f�r jede Action nur ein hotkey habe
        {
            inputBinding = inputActionReference.action.bindings[selectedBinding];
            bindingindex = selectedBinding;
        }
    }
    private void updatebindingUI()
    {
        if(Keybindtext != null)
        {
            if (Application.isPlaying)
            {
                Keybindtext.color = Color.white;
                Keybindtext.text = Keybindinputmanager.getbindingname(actionname, bindingindex);
            }
            else
            {
                Keybindtext.color = Color.white;
                Keybindtext.text = inputActionReference.action.GetBindingDisplayString(bindingindex);
            }
        }
    }
    private void cantclicklayerdisable()
    {
        cantclicklayer.SetActive(false);
    }
    public void changekeybinding()
    {
        cantclicklayer.SetActive(true);
        Keybindinputmanager.startrebind(actionname, bindingindex, Keybindtext);
        //Debug.Log(actionname.ToString());
    }
    private void resetkeybinding()
    {
        Keybindinputmanager.resetbinding(actionname, bindingindex);
        updatebindingUI();
    }


    public void changeupkeybinding()
    {
        cantclicklayer.SetActive(true);
        bindingindex = 1;
        Keybindinputmanager.startrebind(actionname, bindingindex, Keybindtext);
    }
    public void changedownkeybinding()
    {
        cantclicklayer.SetActive(true);
        bindingindex = 2;
        Keybindinputmanager.startrebind(actionname, bindingindex, Keybindtext);
    }
    public void changeleftkeybinding()
    {
        cantclicklayer.SetActive(true);
        bindingindex = 3;
        Keybindinputmanager.startrebind(actionname, bindingindex, Keybindtext);
    }
    public void changerightkeybinding()
    {
        cantclicklayer.SetActive(true);
        bindingindex = 4;
        Keybindinputmanager.startrebind(actionname, bindingindex, Keybindtext);
    }
}
