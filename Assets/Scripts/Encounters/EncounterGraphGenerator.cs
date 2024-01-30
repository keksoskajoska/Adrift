using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EncounterGraphGenerator : MonoBehaviour
{
    private static System.Random rnd = new System.Random();
    private int _deph;
    private StartEncounter _startEncounter = new StartEncounter();
    private int _encounterNumbers = 1;
    private List<Encounter>[] _encounters;

    private void Awake()
    {
        _deph = EncounterGraphGeneratorSettings.Instance.Deph;
        _encounters = new List<Encounter>[_deph + 1];
    }

    void Start()
    {
        _encounterGraphGenerator(0, _startEncounter);
    }

    private void _encounterGraphGenerator(int deph, Encounter previousEncounter)
    {
        if (_encounters[deph] != null)
        {
            if (_encounters[deph].Count > 0)
            {
                if (EncounterGraphGeneratorSettings.Instance.GraphSideConnectionChance >= rnd.Next(0, 101))
                {
                    Encounter e = _encounters[deph][rnd.Next(0, _encounters[deph].Count)];
                    e.AddConnection(previousEncounter);
                    previousEncounter.AddConnection(e);
                    Debug.Log("side: " + previousEncounter.EncounterType + " " + previousEncounter.encounterNumber + " coneccted with " + e.EncounterType + " " + e.encounterNumber);
                }
            }
        }

        if (_encounters[deph] == null)
        {
            _encounters[deph] = new List<Encounter>();
            Debug.Log(deph);
        }
        _encounters[deph].Add(previousEncounter);

        if (deph >= _deph)
        {
            return;
        }

        int childCount = rnd.Next(1, 4);

        for (int i = 0; i < childCount; i++)
        {
            Encounter e = _encounterTypeSelector(deph, previousEncounter.EncounterType, previousEncounter.EncounterStreak);
            e.encounterNumber = _encounterNumbers;
            _encounterNumbers++;
            if (e.EncounterType == previousEncounter.EncounterType)
            {
                e.OnAStreak();
            }
            previousEncounter.AddConnection(e);
            e.AddConnection(previousEncounter);
            Debug.Log(previousEncounter.EncounterType + " " + previousEncounter.encounterNumber + " coneccted with " + e.EncounterType + " " + e.encounterNumber);
            _encounterGraphGenerator(deph + 1, e);
        }
    }

    private Encounter _encounterTypeSelector(int deph, char parentEncounterType, int parentEncounterStreak)
    {
        int difficulty = EncounterGraphGeneratorSettings.Instance.Difficulty;
        int rndMax;
        int rndMin;
        Encounter e;

        int dephDifficultyIndex = deph * ((difficulty / 10) + 1);
        rndMin = -500 + dephDifficultyIndex + deph * 10 + difficulty * 10;
        rndMax = dephDifficultyIndex > 10 ? rndMin + deph * 10 : rndMin + 60;

        switch (parentEncounterType)
        {
            case 'F':
                if (deph + (difficulty / 10) > 10)
                {
                    rndMin += (difficulty / 5) * (parentEncounterStreak + 1);
                }
                break;

            case 'N':
                rndMax += 10 * (parentEncounterStreak + 1);
                rndMin -= 10 * (parentEncounterStreak + 1);
                break;

            case 'H':
                if (deph + (difficulty / 10) > 10)
                {
                    rndMax -= 10 * (parentEncounterStreak + 1);
                }
                else
                {
                    rndMin = 0;
                    rndMax = 50;
                }
                break;

            default:
                break;
        }

        int chance = rnd.Next(rndMin, ++rndMax);
        switch (chance)
        {
            case < 40:
                e = new FriendlyEncounter();
                return e;
            case < 80:
                e = new NeutralEncounter();
                return e;
            default:
                e = new HostileEncounter();
                return e;
        }
    }
}
