using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor/Belt")]
public class Beltobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Belt;
    }
}
