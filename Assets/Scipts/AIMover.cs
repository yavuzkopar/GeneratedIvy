using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMover : MonoBehaviour
{
    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position +(Vector3) Random.insideUnitCircle.normalized*5;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
        Vector3 dir = targetPos - transform.position;   
        transform.up = Vector3.Lerp(transform.up, dir, Time.deltaTime);
        if(Vector3.Distance(targetPos,transform.position)<0.1f)
            targetPos = transform.position + (Vector3)Random.insideUnitCircle.normalized*5;
    }
}
