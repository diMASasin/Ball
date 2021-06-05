using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _velocityModifier;
    [SerializeField] private float _jumpForce;

    private bool _grounded = true;
    private Vector3 _targetVelocity;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        _grounded = Physics.SphereCast(new Vector3(transform.position.x, 0.01f + transform.position.y, transform.position.z), transform.localScale.x / 2, Vector3.down, out RaycastHit hit, 0.02f);
        _rigidbody.position += _targetVelocity * _velocityModifier * Time.deltaTime;
    }
}