using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    protected List<Encounter> connections = new List<Encounter>();
    protected string encounterType;
    protected List<string> possibleEncounters = new List<string>();
    protected string encounter;
    public int encounterNumber;

    public void AddConnection(Encounter e)
    {
        connections.Add(e);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
