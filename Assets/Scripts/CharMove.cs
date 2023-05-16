using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float jumpPower = 4;
    public float rotateSpeed = 500.0f;
    private Rigidbody myRigid;
    private float h, v;
    [SerializeField] private bool isJumping = false;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        /* player �̵� */
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        /* ȸ�� */
        if (Input.GetMouseButton(1))
        {
            float yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            float yRotate = transform.eulerAngles.y + yRotateMove;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotate, 0);
        }

        if (myRigid.velocity.y < 0) // �÷��̾ �������� �� == velocity.y�� ����
        {
            Debug.DrawRay(myRigid.position, Vector3.down, Color.red); //ray�� �׸���
            if (Physics.Raycast(myRigid.position, Vector3.down, 0.2f))
            {
                isJumping = false;
            }
        }
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            myRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        Vector3 moveDir = transform.forward * v + transform.right * h;
        Vector3 moveAmount = moveDir.normalized * moveSpeed * Time.deltaTime;
        myRigid.MovePosition(transform.position + moveAmount);
    }
}