using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Offset;
    public float Smoothing;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position + Offset, Smoothing);
    }
}
