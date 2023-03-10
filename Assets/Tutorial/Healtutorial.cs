using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Healtutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private Areacontroller areacontroller;
    private int textindex;
    private string heal;
    private string player1target;
    private string player2target;
    private string player3target;

    private int tutorialnumber;

    private bool readinputs;

    private void Start()
    {
        tutorialcontroller = GetComponentInParent<Tutorialcontroller>();
        controlls = Keybindinputmanager.inputActions;
        readinputs = false;
        areacontroller = tutorialcontroller.areacontroller;
        tutorialnumber = GetComponent<Areanumber>().areanumber;
        if (areacontroller.tutorialcomplete[tutorialnumber] == true)
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (readinputs == true && controlls.Player.Interaction.WasPressedThisFrame())
        {
            if (textindex != 2)
            {
                tutorialcontroller.tutorialtext.text = string.Empty;
                textindex++;
                showtext();
            }
            else
            {
                endtutorial();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar && areacontroller.tutorialcomplete[tutorialnumber] == false)
        {
            heal = controlls.Player.Heal.GetBindingDisplayString();
            player1target = controlls.SpielerHeal.Target1.GetBindingDisplayString();
            player2target = controlls.SpielerHeal.Target2.GetBindingDisplayString();
            player3target = controlls.SpielerHeal.Target3.GetBindingDisplayString();
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Press and hold \"" + "<color=green>" + heal + "</color>" + "\" to enter the heal state.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "While holding \"" + "<color=green>" + heal + "</color>" + "\" press the buttons appearing on the screen in the right order.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "After succesfully hitting the buttons, choose the target you want to heal with \"" + "<color=green>" + player1target + "</color>" + "\", \""
                                               + "<color=green>" + player2target + "</color>" + "\" or \"" + "<color=green>" + player3target + "</color>" + "\".";
    }
    private void endtutorial()
    {
        readinputs = false;
        tutorialcontroller.endtutorial();
        areacontroller.tutorialcomplete[tutorialnumber] = true;
        areacontroller.autosave();
        gameObject.SetActive(false);
    }
}
