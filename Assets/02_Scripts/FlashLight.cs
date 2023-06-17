using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

/// <summary>
/// FlashLight가 On인 상태에서 발사되는 Ray에 닿을 때의 행위
/// </summary>
public interface IFlashLight
{
    /// <summary>
    /// 해당 오브젝트를 Ray로 쏘고있는 중에 빛을 킬 경우
    /// </summary>
    public void OnLighting_Start();
    /// <summary>
    /// 빛이 켜진 상태에서 해당 오브젝트에 Ray가 닿고있을 경우 매 프레임 마다
    /// </summary>
    public void OnLighting();
    /// <summary>
    /// 해당 오브젝트를 Ray로 쏘고있는 중에 빛을 끌 경우
    /// </summary>
    public void OnLighting_End();
}

public class FlashLight : MonoBehaviour
{
    [SerializeField] private Light flashLight;
    [SerializeField, Min(0.01f)] private float intensity = 1;
    [SerializeField] private PlayerMovementController playerController;
    [SerializeField] Transform forward;
    [SerializeField, Min(0.01f)] float flash_ray_length = 15f;

    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>();
    }
    private void Start()
    {
        InputManager.instance.event_keyInput += GetInput;
    }
    /// <summary>
    /// 현재 손전등이 켜져있는지 여부
    /// </summary>
    [SerializeField] public bool isLightOn => flashLight.intensity > float.Epsilon;
    void GetInput(KeyType keyType, InputType inputType)
    {
        if (keyType == KeyType.toggle_flashLight)
        {
            if (inputType == InputType.down)
            {
                Debug.Log("손전등 토글 키 입력받음");
                if (isLightOn) ToggleFlashLight(false);
                else ToggleFlashLight(true);
            }
        }
    }
    void ToggleFlashLight(bool turnOn)
    {
        if (turnOn)
        {
            if (!playerController.allowedMovement) return;
            flashLight.intensity = intensity;
            if (Physics.Raycast(forward.transform.position, forward.transform.forward, out RaycastHit rhit))
            {
                if (rhit.distance <= flash_ray_length)
                {
                    if (rhit.collider.gameObject.TryGetComponent<IFlashLight>(out IFlashLight iFlash))
                    {
                        iFlash.OnLighting_Start();
                    }
                }
            }
        }
        else
        {
            flashLight.intensity = float.Epsilon;
            if (Physics.Raycast(forward.transform.position, forward.transform.forward, out RaycastHit rhit))
            {
                if (rhit.distance <= flash_ray_length)
                {
                    if (rhit.collider.gameObject.TryGetComponent<IFlashLight>(out IFlashLight iFlash))
                    {
                        iFlash.OnLighting_End();
                    }
                }
            }
        }
    }
    public void LateUpdate()
    {
        gameObject.transform.eulerAngles = Camera.main.transform.eulerAngles;
    }
    public void Update()
    {
        if (!playerController.allowedMovement)
        {
            flashLight.intensity = 0;
        }
        if (isLightOn)
        {
            if (Physics.Raycast(forward.transform.position, forward.transform.forward, out RaycastHit rhit))
            {
                Debug.DrawRay(forward.transform.position, forward.transform.forward * rhit.distance, Color.yellow);
                if (rhit.distance <= flash_ray_length)
                {
                    if (rhit.collider.gameObject.TryGetComponent<IFlashLight>(out IFlashLight iFlash))
                    {
                        iFlash.OnLighting();
                    }
                }
            }
        }
    }
}
