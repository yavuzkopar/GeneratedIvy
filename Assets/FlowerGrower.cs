using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGrower : MonoBehaviour
{
    float size = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        size += Time.deltaTime;
        if(size<=1)
            transform.localScale = Vector3.one* size;
    }
}
