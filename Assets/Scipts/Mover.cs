using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] float speed;
    Rigidbody rb;
    [SerializeField] LayerMask raycastLayer;
    Transform camTransform;
    IvyRotator rotator;
    void Start()
    {
        rotator = FindObjectOfType<IvyRotator>();
        rb = GetComponent<Rigidbody>();
        camTransform = Camera.main.transform;
    }
    void Update()
    {
        //  BoundControl();
        transform.position += rotator.transform.forward * Time.deltaTime;
        Vector3 a = camTransform.right * joystick.Horizontal;
        Vector3 b = camTransform.up * joystick.Vertical;
        rb.velocity = (a+b) *speed;
        transform.up = rb.velocity.normalized;
    }
    void BoundControl()
    {
        Vector3 dir = transform.position - Camera.main.transform.position;
        dir.Normalize();
        if (Physics.Raycast(Camera.main.transform.position,dir,out RaycastHit hit,1000,raycastLayer))
        {
            Debug.Log("aaa");
            transform.position = hit.point + hit.normal;
        }
    }
}
