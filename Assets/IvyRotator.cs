using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyRotator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] LayerMask rayLayer;
    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
       transform.forward = -contactPoints[0].normal;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;
    }
}
