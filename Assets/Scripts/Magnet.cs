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
        item.OnMagnet = this;
        item.AttachToMagnet(this.transform);
    }

    public Item Detach()
    {
        this.item.OnMagnet = null;
        Item ret = this.item;
        this.item = null;

        return ret;
    }
}
