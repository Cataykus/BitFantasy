using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothness;


    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_target.position.x, _target.position.y, transform.position.z), _smoothness * Time.deltaTime);
    }
}
