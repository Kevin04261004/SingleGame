using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    //[SerializeField] private bool canMove;
    private StackableBool isPreventedMovement = new StackableBool();
    [field: SerializeField] public GameObject forward {get; private set; }
    /// <summary>���� �������� ���� �������� Ȯ��</summary>
    public bool allowedMovement => !isPreventedMovement;
    [Tooltip("�ȱ� �ӵ�")][SerializeField] private float moveSpeed = 3.0f;
    [Tooltip("�޸��� �ӵ�")][SerializeField] private float runSpeed = 6.0f;
    [SerializeField] private Rigidbody myRigid;
    private float h, v;
    [SerializeField] private bool isRunning = false;

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
        isPreventedMovement.ResetStack();
    }
    private void Update()
    {
        //if (!canMove)
        //{
        //    h = 0; v = 0;
        //    return;
        //}
        if (!allowedMovement)
        {
            h = 0; v = 0;
            return;
        }
        /* player �̵� */
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        /* player �޸��� */
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }
    private void FixedUpdate()
    {
        /* ȸ�� */
        forward.transform.eulerAngles = Camera.main.transform.eulerAngles;
        gameObject.transform.eulerAngles = new Vector3(0, forward.transform.eulerAngles.y, 0);

        /* �̵� */
        Vector3 moveDir = transform.forward * v + transform.right * h;
        Vector3 moveAmount;
        if (!isRunning)
        {
            moveAmount = moveDir.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            moveAmount = moveDir.normalized * runSpeed * Time.deltaTime;
        }
        myRigid.MovePosition(transform.position + moveAmount);
    }
    //public bool Get_canMove()
    //{
    //    return canMove;
    //}
    public void PreventMovement_AddStack()
    {
        isPreventedMovement.AddStack();
        if (isPreventedMovement.currentStack == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void PreventMovement_SubtractStack()
    {
        isPreventedMovement.SubtractStack();
        if (isPreventedMovement.currentStack == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    //public void Set_canMove_Bool(bool _canMove)
    //{
    //    canMove = _canMove;
    //    if(!canMove)
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;
    //    }
    //}
}