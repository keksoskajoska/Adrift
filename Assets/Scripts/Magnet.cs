using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private Item item;

    public bool InUse()
    {
        return item != null;
    }

    public void Attach(Item item)
    {
        this.item = item;
        item.magnet = this;
        item.AttachToMagnet(this.transform);
    }

    public void Detach()
    {
        this.item.magnet = null;
        this.item = null;
    }
}
