using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] Direction direction;
    IvyGenerator ivyGenerator;
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        ivyGenerator = FindObjectOfType<IvyGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Direction.forward:
                slider.value = ivyGenerator.GetFrontValue();
                break;
            case Direction.back:
                slider.value = ivyGenerator.GetBackValue();
                break;
            case Direction.right:
                slider.value = ivyGenerator.GetRightValue();
                break;
            case Direction.left:
                slider.value = ivyGenerator.GetLeftValue();
                break;
            default:
                break;
        }
    }
}
