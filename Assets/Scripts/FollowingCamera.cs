using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private FollowingCamera _camera;
    [SerializeField] private GameObject _target;

    private Quaternion _initialRotation;

    private void Start()
    {
        
    }

    private void Update()
    {
        _camera.transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y + 2, -10);
    }
}
