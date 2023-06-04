using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private Light flashLight;
    [SerializeField] private float intensity = 1;
    [SerializeField] private PlayerMovementController playerController;
    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>();
    }
    public void LateUpdate()
    {
        gameObject.transform.eulerAngles = Camera.main.transform.eulerAngles;
    }
    public void Update()
    {
        if (!playerController.Get_canMove())
        {
            flashLight.intensity = 0;
            return;
        }
        if (Input.GetMouseButtonDown(0))
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
