using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    [SerializeField] private GameObject forward;
    [Tooltip("�ȱ� �ӵ�")][SerializeField] private float moveSpeed = 3.0f;
    [Tooltip("�޸��� �ӵ�")][SerializeField] private float runSpeed = 6.0f;
    [Tooltip("���� �Ŀ�")][SerializeField] private float jumpPower = 4;
    [SerializeField] private Rigidbody myRigid;
    private float h, v;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isRunning = false;
    [Tooltip("�� �ִ� ����")][SerializeField] private float jumpTime;
    [Tooltip("���� ��Ÿ��")][SerializeField] private float jumpCoolTime;
    [SerializeField] private RaycastHit hit;
    [Tooltip("�� �ִ� ����")][SerializeField] private float handLength;
    [Tooltip("�� �⺻ ����")][SerializeField] private Color baseColor;
    [Tooltip("�� ��ȣ�ۿ� �Ҷ� ����")][SerializeField] private Color changeColor;

    private void Update()
    {
        /* player �̵� */
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        /* player ���� */
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            myRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        /* player �޸��� */
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        /* RuleBook on/off */
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.instance.Set_RuleBook_BackGround_TrueOrFalse();
        }

        /* CCTV on/off */
        if (Input.GetKeyDown(KeyCode.M))
        {
            UIManager.instance.Set_CCTV_BackGround_TrueOrFalse();
        }

        /* Raycast �߻� (��ȣ�ۿ�) */
        if (Physics.Raycast(forward.transform.position, forward.transform.forward, out hit))
        {
            Debug.DrawRay(forward.transform.position, forward.transform.forward * hit.distance, Color.red);
            if(hit.distance > handLength)
            {
                UIManager.instance.Set_middlePoint_Image_Color(baseColor);
                return;
            }
            if (hit.collider.CompareTag("doorknob"))
            {
                UIManager.instance.Set_middlePoint_Image_Color(changeColor);

                if(Input.GetKeyDown(KeyCode.F))
                {
                    hit.collider.gameObject.transform.GetComponentInParent<Door>().ToggleDoor();
                }
            }

        }
        else
        {
            Debug.DrawRay(forward.transform.position, forward.transform.forward * 1000f, Color.red);
            UIManager.instance.Set_middlePoint_Image_Color(baseColor);
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

        /* ���� */
        if (isJumping)
        {
            jumpTime += Time.deltaTime;
            if(jumpTime > jumpCoolTime)
            {
                jumpTime = 0;
                isJumping = false;
            }
        }
    }
}