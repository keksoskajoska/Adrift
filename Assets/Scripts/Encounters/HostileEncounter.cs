using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileEncounter : Encounter
{
    public HostileEncounter()
    {
        encounterType = 'H';
        encounterStreak = 0;
    }
}
