using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Switchequipmentgrid : MonoBehaviour, ISelectHandler
{
    [SerializeField] private GameObject grid;
    [SerializeField] Equipmentmenucontroller equipmentcontroller;
    [SerializeField] private int buttonnumber;

    public void OnSelect(BaseEventData eventData)
    {
        equipmentcontroller.switchgrid(grid);
        Statics.currentequipmentbutton = buttonnumber;
        Equipcharselection.currentbuttonslot = gameObject;
    } 
}

