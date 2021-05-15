using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _velocityModifier;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxVelocity;

    private bool Grounded = true;
    private Vector3 _targetVelocity;
    private Rigidbody _rigidbody;
    private int _collectedCoins;

    public event UnityAction<int> CoinCollected;

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
        if (_rigidbody.velocity.x > _maxVelocity)
            _rigidbody.velocity = new Vector3(_maxVelocity * _targetVelocity.x, _rigidbody.velocity.y, _rigidbody.velocity.z);
        else
            _rigidbody.velocity += _targetVelocity * _velocityModifier * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Coin>(out Coin coin))
        {
            Destroy(coin.gameObject);
            _collectedCoins++;
            CoinCollected?.Invoke(_collectedCoins);
        }
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