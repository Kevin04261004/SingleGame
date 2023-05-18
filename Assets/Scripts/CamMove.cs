using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField] private CharMove charmove;
    [SerializeField] private GameObject Target;
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed;

    private void Start()
    {
        //rotateSpeed = charmove.rotateSpeed;
    }
    void Update()
    {
        if (Input.GetMouseButton(1)) // 클릭한 경우
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
        transform.position = Target.transform.position;
    }
}
