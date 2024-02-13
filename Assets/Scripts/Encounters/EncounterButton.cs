using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterButton : MonoBehaviour
{

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private List<Material> _encounterButtonMaterials;
    public int EncounterButtonType;
    public Encounter ButtonsEncounter;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer.material = _encounterButtonMaterials[EncounterButtonType];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
