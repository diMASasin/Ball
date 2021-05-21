using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _velocityModifier;
    [SerializeField] private float _jumpForce;

    private bool Grounded = true;
    private Vector3 _targetVelocity;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        _rigidbody.position += _targetVelocity * _velocityModifier * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        Grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        Grounded = false;
    }
}