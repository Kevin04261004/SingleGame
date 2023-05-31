using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private Light flashLight;
    [SerializeField] private float intensity = 1;
    public void LateUpdate()
    {
        gameObject.transform.eulerAngles = Camera.main.transform.eulerAngles;
    }
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(flashLight.intensity >0)
            {
                flashLight.intensity = 0;
            }
            else
            {
                flashLight.intensity = intensity;
            }
        }
    }
}
