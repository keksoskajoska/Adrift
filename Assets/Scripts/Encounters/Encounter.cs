using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter
{
    protected List<Encounter> connections = new List<Encounter>();
    protected char encounterType;
    protected List<string> possibleEncounters = new List<string>();
    protected string encounter;
    protected int encounterStreak = 0;
    public int encounterNumber;

    public char EncounterType
    {
        get { return encounterType; }
    }
    public List<Encounter> Connections
    {
        get { return connections; }
    }
    public int EncounterStreak
    {
        get { return encounterStreak; }
    }

    public void AddConnection(Encounter e)
    {
        connections.Add(e);
    }

    public void OnAStreak() // oh im on a roll
    {
        encounterStreak++;
    }
}
