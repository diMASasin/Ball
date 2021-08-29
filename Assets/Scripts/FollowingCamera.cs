using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -10);

    private void Update()
    {
        _camera.transform.position = _target.position + _offset;
    }
}
