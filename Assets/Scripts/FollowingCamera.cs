using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -10);

    private void Update()
    {
        Camera.main.transform.position = new Vector3(_target.position.x, _target.position.y, _target.position.z) + _offset;
    }
}
