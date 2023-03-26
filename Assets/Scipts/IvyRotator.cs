using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyRotator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] LayerMask rayLayer;
    [SerializeField] Direction direction = Direction.right;
    [SerializeField] Transform[] branch;
    [SerializeField] Transform branchTarget;
    [SerializeField] Transform flower;
    float timer;
    float flowerTimer;
    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
       transform.forward = -contactPoints[0].normal;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - transform.forward *0.5f;
        SetDirection();
        timer += Time.deltaTime;
        flowerTimer += Time.deltaTime;
        if(timer > 2f)
        {
            timer = 0;
            GenerateBranch();
        }
        if(flowerTimer > 1f)
        {
            flowerTimer = 0;
            GenerateFlowerh();
        }    
        
    }

    private void GenerateBranch()
    {
        Vector3 spawnpoint = transform.TransformPoint(Random.insideUnitCircle) - transform.forward *0.5f;
        Vector3 spawnDir = ((spawnpoint + transform.forward *0.5f) - transform.position).normalized;
        Transform brancInstance = Instantiate(branch[Random.Range(0,branch.Length)], transform.position, Quaternion.LookRotation(spawnDir,transform.forward));
        brancInstance.parent = branchTarget;
    }
    private void GenerateFlowerh()
    {
        Vector3 spawnpoint = transform.TransformPoint(Random.insideUnitCircle) - transform.forward * 0.5f;
        Vector3 spawnDir = ((spawnpoint + transform.forward * 0.5f) - transform.position).normalized;
        Transform brancInstance = Instantiate(flower, transform.position, Quaternion.LookRotation(spawnDir, transform.forward));
        brancInstance.parent = branchTarget;
    }

    private void SetDirection()
    {
        if (transform.eulerAngles.y < 45 && transform.eulerAngles.y > -45)
            direction = Direction.forward;
        if (transform.eulerAngles.y < 135 && transform.eulerAngles.y > 45)
            direction = Direction.left;
        if (transform.eulerAngles.y < 225 && transform.eulerAngles.y > 135)
            direction = Direction.back;
        if (transform.eulerAngles.y < 360 && transform.eulerAngles.y > 225)
            direction = Direction.right;
    }

    public Direction GetDirection()
    {
        return direction;
    }
}
public enum Direction
{
    forward,
    back,
    right,
    left
}
