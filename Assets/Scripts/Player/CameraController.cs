using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _cameraTargetTransform;
    [SerializeField] public Vector2 _mouseInput;
    [SerializeField] private float _mouseSensitivity = 1.0f;
    [SerializeField] private bool _invertMouseY = false;
    private void Start()
    {
        //_camera = Camera.main;

        _mouseInput = Vector2.zero;
    }

    private void Update()
    {
        _camera.position = _cameraTargetTransform.position;

        int invertMul = _invertMouseY ? 1 : -1;

        _mouseInput.x += Input.GetAxis("Mouse X") * _mouseSensitivity;
        _mouseInput.y += Input.GetAxis("Mouse Y") * _mouseSensitivity * invertMul;

        _mouseInput.x %= 360.0f;

        if(_mouseInput.y >= 85.0f)
        {
            _mouseInput.y = 85.0f;
        }
        else if(_mouseInput.y <= -85.0f)
        {
            _mouseInput.y = -85.0f;
        }

        Quaternion rotation = Quaternion.Euler(_mouseInput.y, _mouseInput.x, 0.0f);

        this.transform.localRotation = rotation;
        _camera.localRotation = rotation;
    }
}
