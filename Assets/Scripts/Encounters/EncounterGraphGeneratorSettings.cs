using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterGraphGeneratorSettings : MonoBehaviour
{
    private static EncounterGraphGeneratorSettings _instance;
    [SerializeField] private int _deph;
    [SerializeField] private int _difficulty;
    [SerializeField] private int _graphSideConnectChane;

    public int Deph
    {
        get { return _deph; }
    }
    public int Difficulty
    {
        get { return _difficulty; }
    }
    public int GraphSideConnectionChance
    {
        get { return _graphSideConnectChane; }
    }
    public static EncounterGraphGeneratorSettings Instance
    {
        get
        {
            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of Singleton exist, destroying self.");
            Destroy(this.gameObject);
        }
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
