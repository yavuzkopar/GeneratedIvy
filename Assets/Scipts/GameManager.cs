using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    IvyGenerator ivyGenerator;
    [SerializeField] UnityEvent endGameEvent;
    bool isOver;
    [SerializeField] CinemachineVirtualCamera dolly;
    [SerializeField] float dollySpeed;
    [SerializeField] Animator animator;
    [SerializeField] Transform houseTransform;
    CinemachineTrackedDolly trackedDolly;
    void Start()
    {
        ivyGenerator = FindObjectOfType<IvyGenerator>();
        trackedDolly = dolly.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameEnded() && !isOver)
        {
            endGameEvent?.Invoke();
            isOver = true;
        }
        if(isOver)
            EndiningSequence();
        if (trackedDolly.m_PathPosition > 1.5f)
            animator.SetTrigger("Patla");
    }
    bool isGameEnded()
    {
       return ivyGenerator.GetBackValue() > 1f && ivyGenerator.GetFrontValue()>1f && ivyGenerator.GetLeftValue()>1f && ivyGenerator.GetRightValue()>1f;
    }
    void EndiningSequence()
    {
        if (trackedDolly.m_PathPosition < 1.6f)
        {
            trackedDolly.m_PathPosition += Time.deltaTime * dollySpeed;
            houseTransform.localScale = Vector3.one * (Mathf.Sin(Time.time * 3) * 0.08f + 1f);
        }
    }
}
