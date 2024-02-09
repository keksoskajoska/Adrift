using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableMagnet : Item
{
    public override void Use()
    {
        base.Use();

        Debug.Log("Used placable magnet");
    }
}
