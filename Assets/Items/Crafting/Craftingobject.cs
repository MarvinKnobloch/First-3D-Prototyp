using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Object", menuName = "Inventory System/Items/Crafting")]
public class Craftingobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Crafting;
    }
}
