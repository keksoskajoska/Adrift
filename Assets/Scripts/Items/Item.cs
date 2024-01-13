using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Item : MonoBehaviour
{
    public bool Pickable = true;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _col;
    public Magnet magnet;

    public void Pickup(Transform hand)
    {
        _rb.isKinematic = true;
        _col.enabled = false;

        this.transform.parent = hand;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        //SetLayerToItem();
    }

    public void AttachToMagnet(Transform magnet)
    {
        _rb.isKinematic = true;
        _col.enabled = false;

        this.transform.parent = magnet;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        //SetLayerToMagnet();
    }

    public void Drop()
    {
        _rb.isKinematic = false;
        _col.enabled = true;

        this.transform.position = this.transform.position + this.transform.forward * 0.3f;
        this.transform.parent = null;

        SetLayerToItem();
    }

    private void SetLayerToMagnet()
    {
        _col.gameObject.layer = LayerMask.NameToLayer("Magnet");
    }

    private void SetLayerToItem()
    {
        _col.gameObject.layer = LayerMask.NameToLayer("Item");
    }
}
