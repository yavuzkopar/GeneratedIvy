using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyGrower : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    Material growMaterial;
    float growValue = 0f;
    [SerializeField] float growSpeed;
    void Start()
    {
        growMaterial = mesh.material;
    }

    // Update is called once per frame
    void Update()
    {
        if(growValue> 1f) return;
        growValue += Time.deltaTime * growSpeed;
        growMaterial.SetFloat("_Grow", growValue);
    }
}
