using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    private Encounter _currentEncounter;
    private Transform _panel;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _encounterButton;
    [SerializeField] private GameObject _encounterButtonPlace;
    private bool _encountersAreShowing = false;
    private List<GameObject> _encoounterButtons = new List<GameObject>();
    [SerializeField] private List<Material> _currentEncounterMatarials;
    [SerializeField] private GameObject _panelButton;

    private void Awake()
    {
        //_currentEncounter = EncounterGraphGeneratorSettings.Instance.StartEncounter;
    }

    private void Start()
    {
        _currentEncounter = EncounterGraphGeneratorSettings.Instance.StartEncounter;
        SpawnEncounterButtons(true);
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = new Ray(_mainCamera.transform.position,_mainCamera.transform.forward);
            if (Physics.Raycast(ray, out hit,2))
            {
                if (hit.collider.tag == "EncounterButton")
                {
                    // select encounter
                    _currentEncounter = hit.collider.GetComponent<EncounterButton>().ButtonsEncounter;

                    for (int i = 0; i <= _encoounterButtons.Count - 1; i++)
                    {
                        Destroy(_encoounterButtons[_encoounterButtons.Count - i - 1]);
                    }
                    _encoounterButtons = new List<GameObject>();

                    switch (_currentEncounter.EncounterType)
                    {
                        case 'S':
                            _panelButton.GetComponent<MeshRenderer>().material = _currentEncounterMatarials[0];
                            break;
                        case 'H':
                            _panelButton.GetComponent<MeshRenderer>().material = _currentEncounterMatarials[1];
                            break;
                        case 'N':
                            _panelButton.GetComponent<MeshRenderer>().material = _currentEncounterMatarials[2];
                            break;
                        case 'F':
                            _panelButton.GetComponent<MeshRenderer>().material = _currentEncounterMatarials[3];
                            break;

                        default:
                            break;
                    }

                    if (_currentEncounter == EncounterGraphGeneratorSettings.Instance.StartEncounter)
                    {
                        SpawnEncounterButtons(true);
                    }
                    else
                    {
                        SpawnEncounterButtons();
                    }

                }
                if (hit.collider.tag == "EncounterPanelButton")
                {
                    if (_encountersAreShowing)
                    {
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = _currentEncounterMatarials[4];
                       
                        for (int i = 0; i <= _encoounterButtons.Count-1; i++)
                        {
                            Destroy(_encoounterButtons[_encoounterButtons.Count-i-1]);
                        }
                        _encoounterButtons = new List<GameObject>();
                        _encountersAreShowing = false;
                    }
                    else
                    {
                        switch (_currentEncounter.EncounterType)
                        {
                            case 'S':
                                hit.collider.gameObject.GetComponent<MeshRenderer>().material = _currentEncounterMatarials[0];
                                break;
                            case 'H':
                                hit.collider.gameObject.GetComponent<MeshRenderer>().material = _currentEncounterMatarials[1];
                                break;
                            case 'N':
                                hit.collider.gameObject.GetComponent<MeshRenderer>().material = _currentEncounterMatarials[2];
                                break;
                            case 'F':
                                hit.collider.gameObject.GetComponent<MeshRenderer>().material = _currentEncounterMatarials[3];
                                break;

                            default:
                                break;
                        }

                        if (_currentEncounter == EncounterGraphGeneratorSettings.Instance.StartEncounter)
                        {
                            SpawnEncounterButtons(true);
                        }
                        else
                        {
                            SpawnEncounterButtons();
                        }
                        _encountersAreShowing = true;
                    } 
                }
            }
        }
    }

    private void SpawnEncounterButtons(bool startEncounter = false)
    {
        int indexStart = 1;

        if (startEncounter)
        {
            indexStart = 0;
        }
        int pos = 0;
        for (int i = indexStart; i <= _currentEncounter.Connections.Count-1; i++)
        {
            pos++;
            Vector3 buttonPos = _encounterButtonPlace.transform.position - Vector3.left * pos;
            GameObject e = Instantiate(_encounterButton, buttonPos, this.transform.rotation);
            e.GetComponent<EncounterButton>().ButtonsEncounter = _currentEncounter.Connections[i];
            _encoounterButtons.Add(e);

            switch (_currentEncounter.Connections[i].EncounterType)
            {
                case 'H':
                    
                    e.gameObject.GetComponent<EncounterButton>().EncounterButtonType = 0;
                    break;
                case 'N':
                    e.gameObject.GetComponent<EncounterButton>().EncounterButtonType = 1;
                    break;
                case 'F':
                    e.gameObject.GetComponent<EncounterButton>().EncounterButtonType = 2;
                    break;

                default:
                    break;
            }

            

        }
    }

}
