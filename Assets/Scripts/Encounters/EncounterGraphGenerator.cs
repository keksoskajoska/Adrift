using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterGraphGenerator : MonoBehaviour
{
    private static System.Random rnd = new System.Random();
    private int _deph;
    private StartEncounter _startEncounter = new StartEncounter();
    private static int _encounterNumbers = 1;

    private void Awake()
    {
        _deph = EncounterGraphGeneratorSettings.Instance.Deph;
        _startEncounter.encounterNumber = 0;
    }

    private void _encounterGraphGenerator(int deph,Encounter previousEncounter)
    {
        Debug.Log(deph);
        if (deph >= 2)
        {
            return;
        }

        if (previousEncounter == null)
        {
            
        }

        

        int childCount = rnd.Next(2,4);

        for (int i = 0; i < childCount; i++)
        {
            Encounter e = _encounterTypeSelector(deph);
            e.encounterNumber = _encounterNumbers;
            _encounterNumbers++;
            previousEncounter.AddConnection(e);
            e.AddConnection(previousEncounter);
            Debug.Log(previousEncounter.encounterNumber + " coneccted with " + e.encounterNumber);
            _encounterGraphGenerator(deph+1,e);
        }
        return;
    }

    private Encounter _encounterTypeSelector(int deph)
    {
        Encounter e;
        switch (rnd.Next(1, 4))
        {
            case 1:
                e = new NeutralEncounter();
                return e;
            case 2:
                e = new HostileEncounter();
                return e;
            case 3:
                e = new FriendlyEncounter();
                return e;

            default:
                Debug.LogWarning("encounter type returns null");
                return null;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _encounterGraphGenerator(0,_startEncounter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
