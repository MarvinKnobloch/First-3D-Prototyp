using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LoadKeybindsMaingame : MonoBehaviour
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

    private void OnValidate()                                                           // Wird au�erhalb vom Playmodusgecalled, immer dann wenn ein Wert im Inspector ge�ndert wird
    {
        if (inputActionReference == null)
            return;
        getbindinginfo();
    }
    private void Start()
    {
        getbindinginfo();
        Keybindinputmanager.loadbindingsoverride(actionname);
    }
    private void getbindinginfo()
    {
        if (inputActionReference.action != null)
        {
            actionname = inputActionReference.action.name;                              // durchsucht die Hotkeys/Actionen und setzt dann String 
        }
        if (inputActionReference.action.bindings.Count > selectedBinding)              // eher unwichtig, weil ich f�r jede Action nur ein hotkey habe
        {
            inputBinding = inputActionReference.action.bindings[selectedBinding];
            bindingindex = selectedBinding;
        }
    }
}