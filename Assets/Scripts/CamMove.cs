using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField] private CharMove charmove;
    [SerializeField] private GameObject Target;
    public bool isCamMove;
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 120;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isCamMove)
            {
                isCamMove = false;
            }
            else
            {
                isCamMove = true;
            }
        }
        if(!isCamMove && (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)))
        {
            isCamMove = true;
        }
        if(isCamMove)
        {
            xRotateMove = -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxisRaw("Mouse X") * Time.deltaTime * rotateSpeed;

            yRotate = transform.eulerAngles.y + yRotateMove;
            //xRotate = transform.eulerAngles.x + xRotateMove; 

            xRotate = xRotate + xRotateMove;

            xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
        }
    }
    private void LateUpdate()
    {
        Vector3 targetPosition = Target.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 6);
    }
}
