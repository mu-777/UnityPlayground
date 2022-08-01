using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float DeltaAngleDegPerSec = 5f;
    private Vector3 _rotationAxis;

    void Start()
    {
        _rotationAxis = Vector3.up;
    }

    void Update()
    {
        _rotationAxis += new Vector3(Random.Range(0f, 0.1f), Random.Range(0f, 0.1f), Random.Range(0f, 0.1f));
        _rotationAxis.Normalize();
        this.transform.rotation *= Quaternion.AngleAxis(DeltaAngleDegPerSec * Time.deltaTime, _rotationAxis);
    }
}
