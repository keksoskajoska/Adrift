using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralEncounter : Encounter
{
    public NeutralEncounter()
    {
        encounterType = 'N';
        encounterStreak = 0;
    }
}
