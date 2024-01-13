using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFPSMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _inputDirection;
    private Vector3 _movementForce;
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private CameraController _cameraController;
    [SerializeField] private float _bodyRotationSpeed = 1.0f;

    private void Update()
    {
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float CameraYrotation = _cameraController.transform.localEulerAngles.y;

        _movementForce = new Vector3(_inputDirection.x, 0.0f, _inputDirection.y) * _movementSpeed;

        _movementForce = Quaternion.AngleAxis(CameraYrotation, Vector3.up) * _movementForce;

        // Rotate body towards movement direction
        if(_inputDirection.Equals(Vector2.zero))
        {
            return;
        }

        float transformY = this.transform.localEulerAngles.y;

        Quaternion ownRot = Quaternion.Euler(0.0f, transformY, 0.0f);
        Quaternion targetRot = Quaternion.Euler(0.0f, CameraYrotation, 0.0f);

        Quaternion rotation = Quaternion.Lerp(ownRot, targetRot, _bodyRotationSpeed);

        this.transform.localRotation = rotation;
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_movementForce);
    }
}
