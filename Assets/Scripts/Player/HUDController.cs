using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum HUDState
{
    HAND,
    COMBAT
}

public class HUDController : MonoBehaviour
{
    [SerializeField] private HUDState _state;
    [Header("Hand stuff")]
    [SerializeField] private GameObject _handElements;
    [SerializeField] private TMP_Text _actionName;
    [SerializeField] private TMP_Text _itemName;

    private void Start()
    {
        _state = HUDState.HAND;
    }

    /*
    private void Update()
    {
        switch(_state)
        {
            case HUDState.HAND:
                break;
            case HUDState.COMBAT:
                break;
        }
    }
    */

    public void ChangeHudState(HUDState state)
    {
        _state = state;
        switch(_state)
        {
            case HUDState.HAND:
                _handElements.SetActive(true);
                break;
            case HUDState.COMBAT:
                _handElements.SetActive(false);
                break;
        }
    }

    public void ChangeHandAction(string action)
    {
        _actionName.text = action;
    }

    public void ChangeItemName(string name)
    {
        _itemName.text = name;
    }
}
