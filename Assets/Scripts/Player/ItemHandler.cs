using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _handTransform;
    [SerializeField] private Transform _lookingAt;
    [SerializeField] private float _reachDistance;

    [SerializeField] private Transform _inHand;

    [SerializeField] private LayerMask _layerMask;

    private void Update()
    {
        // LOOKING AT
        RaycastHit hit;
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        if (Physics.Raycast(ray, out hit, _reachDistance, _layerMask))
        {
            _lookingAt = hit.collider.transform;
        }
        else
        {
            _lookingAt = null;
        }

        // INPUT HANDLING
        if (Input.GetKeyUp(KeyCode.G))
        {
            Magnet magnet = _lookingAt != null ? _lookingAt.GetComponentInParent<Magnet>() : null;
            if(magnet)
            {
                DropFromMagnet(magnet);
            }
            else
            {
                DropItem();
            }
        }
        else if(Input.GetKeyUp(KeyCode.E) && _lookingAt)
        {
            Magnet magnet = _lookingAt.GetComponentInParent<Magnet>();
            Item lookingAtItem = _lookingAt.GetComponentInParent<Item>();
            if (lookingAtItem != null)
            {
                PickupItem(lookingAtItem);
            }
            else if(magnet != null)
            {
                InteractWithMagnet(magnet);
            }
        }
        else if(Input.GetButtonUp("Mouse0") && _inHand)
        {
            Item inHadItem = _inHand.GetComponentInParent<Item>();

            inHadItem.Use();
        }
    }

    private void DropItem()
    {
        if(_inHand)
        {
            Item item = _inHand.GetComponentInParent<Item>();
            item.Drop();
            _inHand = null;
        }    
    }
    
    private void DropFromMagnet(Magnet magnet)
    {
        if(magnet.InUse())
        {
            Item item = magnet.Detach();
            item.Drop();
        }
    }

    private void PickupItem(Item lookingAtItem)
    {
        if (lookingAtItem.Pickable)
        {
            // If player already has an item in their hand, drop that
            Item inHandItem = _inHand != null ? _inHand.GetComponentInParent<Item>() : null;
            if(inHandItem)
            {
                inHandItem.Drop();
            }

            _inHand = lookingAtItem.transform.GetChild(0);

            lookingAtItem.Pickup(_handTransform);

            return;
        }
    }

    private void InteractWithMagnet(Magnet magnet)
    {
        Item inHandItem = _inHand != null ? _inHand.GetComponentInParent<Item>() : null;
        Item onMag = null;

        if(magnet.InUse())
        {
            onMag = magnet.Detach();
        }

        if(inHandItem)
        {
            magnet.Attach(inHandItem);
            _inHand = null;
        }

        if(onMag)
        {
            PickupItem(onMag);
        }
    }
}
