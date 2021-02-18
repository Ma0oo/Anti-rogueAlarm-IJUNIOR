using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _maxSpeed;
    [Range(0,90)] [SerializeField] private float _maxAngelDown;
    [Range(0, -90)] [SerializeField] private float _maxAngelUp;

    private Rigidbody _rigidbody;
    private Camera _camera;
    private Vector3 _rotateToBody;
    private Vector3 _rotateToCameraLocal;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>();
        _rotateToBody = transform.eulerAngles;
        _rotateToCameraLocal = transform.localEulerAngles;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            Move(transform.forward);
        else if (Input.GetKey(KeyCode.S))
            Move(-transform.forward);

        if (Input.GetKey(KeyCode.D))
            Move(transform.right);
        else if (Input.GetKey(KeyCode.A))
            Move(-transform.right);
        Rotate();
    }

    private void Move(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _maxSpeed);
    }   

    private void Rotate()
    {
        _rotateToBody.y += Input.GetAxis("Mouse X") * _sensitivity;
        transform.eulerAngles = _rotateToBody;

        _rotateToCameraLocal.x -= Input.GetAxis("Mouse Y") * _sensitivity;
        _rotateToCameraLocal.x = Mathf.Clamp(_rotateToCameraLocal.x, _maxAngelUp, _maxAngelDown);
        _camera.transform.localEulerAngles = _rotateToCameraLocal;
    }
}