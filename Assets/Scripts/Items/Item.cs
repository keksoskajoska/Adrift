using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Item : MonoBehaviour
{
    public bool Pickable = true;
    public string Name = "";

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _col;
    public Magnet OnMagnet;
    [SerializeField] protected bool _equipped = false;

    public virtual void Pickup(Transform hand)
    {
        _rb.isKinematic = true;
        _col.enabled = false;

        this.transform.parent = hand;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        _equipped = true;
    }

    public virtual void AttachToMagnet(Transform magnet)
    {
        _rb.isKinematic = true;
        _col.enabled = false;

        this.transform.parent = magnet;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        _equipped = false;
    }

    public virtual void Drop()
    {
        _rb.isKinematic = false;
        _col.enabled = true;

        this.transform.position = this.transform.position + this.transform.forward * 0.3f;
        this.transform.parent = null;

        _equipped = false;
    }

    public virtual void Use()
    {

    }
}
