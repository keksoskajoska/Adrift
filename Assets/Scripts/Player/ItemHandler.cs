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
        // DROP
        Item inHandItem = _inHand != null ? _inHand.GetComponentInParent<Item>() : null;
        if (Input.GetKeyUp(KeyCode.G) && _inHand)
        {
            _inHand = null;
            inHandItem.Drop();
        }


        // PICKUP
        RaycastHit hit;
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        if(Physics.Raycast(ray, out hit, _reachDistance, _layerMask))
        {
            _lookingAt = hit.collider.transform;
        }
        else
        {
            _lookingAt = null;
        }

        if(!_lookingAt)
        {
            return;
        }

        Item lookingAtItem = _lookingAt.GetComponentInParent<Item>();
        if (lookingAtItem && lookingAtItem.Pickable && Input.GetKeyUp(KeyCode.E))
        {
            _inHand = _lookingAt;

            if(lookingAtItem.magnet)
            {
                lookingAtItem.magnet.Detach();
            }

            lookingAtItem.Pickup(_handTransform);

            return;
        }

        Magnet magnet = _lookingAt.GetComponentInParent<Magnet>();
        if(_inHand && magnet && !magnet.InUse() && Input.GetKeyUp(KeyCode.E))
        {
            magnet.Attach(inHandItem);
            _inHand = null;
        }
    }
}
