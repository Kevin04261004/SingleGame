using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    [SerializeField] private GameObject forward;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float runSpeed = 6.0f;
    [SerializeField] private float jumpPower = 4;
    private Rigidbody myRigid;
    private float h, v;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isRunning = false;
    private float jumpTime;
    [SerializeField] private float jumpCoolTime;
    [SerializeField] private RaycastHit hit;
    [SerializeField] private float handLength; // 

    private void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /* player 이동 */
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            myRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.instance.Set_RuleBook_BackGround_TrueOrFalse();
        }
        if (Physics.Raycast(forward.transform.position, forward.transform.forward, out hit))
        {
            Debug.DrawRay(forward.transform.position, forward.transform.forward * hit.distance, Color.red);
            if(hit.distance > handLength)
            {
                return;
            }
            if(hit.collider.CompareTag("doorknob") && Input.GetKeyDown(KeyCode.F))
            {
                hit.collider.gameObject.transform.GetComponentInParent<Door>().ToggleDoor();
            }

        }
        else
        {
            Debug.DrawRay(forward.transform.position, forward.transform.forward * 1000f, Color.red);
        }
    }
    private void FixedUpdate()
    {
        /* 회전 */
        forward.transform.eulerAngles = Camera.main.transform.eulerAngles;
        gameObject.transform.eulerAngles = new Vector3(0, forward.transform.eulerAngles.y, 0);

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