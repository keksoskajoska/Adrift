using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFPSMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _inputDirection;
    private Vector3 _movementForce;
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private float _acceleration = 10.0f;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private CameraController _cameraController;
    [SerializeField] private float _bodyRotationSpeed = 1.0f;

    private void Update()
    {
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float CameraYrotation = _cameraController.transform.eulerAngles.y;

        _movementForce = new Vector3(_inputDirection.x, 0.0f, _inputDirection.y) * _acceleration;

        _movementForce = Quaternion.AngleAxis(CameraYrotation, Vector3.up) * _movementForce;

        // Rotate body towards movement direction
        if(_inputDirection.Equals(Vector2.zero))
        {
            return;
        }

        float transformY = this.transform.eulerAngles.y;

        Quaternion ownRot = Quaternion.Euler(0.0f, transformY, 0.0f);
        Quaternion targetRot = Quaternion.Euler(0.0f, CameraYrotation, 0.0f);

        Quaternion rotation = Quaternion.Lerp(ownRot, targetRot, _bodyRotationSpeed);

        if(Quaternion.Angle(rotation, targetRot) < 1f)
        {
            rotation = targetRot;
        }

        this.transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movementForce;
        /*
        if(_rb.velocity.magnitude >= _movementSpeed)
        {
            return;
        }

        if(_movementForce.magnitude > 0)
        {
            _rb.AddForce(_movementForce);
        }
        else
        {
            Vector3 stoppingDir = _rb.velocity.normalized * _acceleration * -1;
            //_rb.AddForce(stoppingDir);
        }
        */
    }
}
