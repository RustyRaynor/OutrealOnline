using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float gravity;
    public float jumpHeight;
    public bool cantMove;

    [SerializeField] float mouseSensitivityX;
    [SerializeField] float mouseSensitivityY;
    [SerializeField] float maxX;
    [SerializeField] float minX;
    float xRotation;

    float mouseX;
    float mouseY;

    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform myCamera;
    [SerializeField] Transform bottom;

    [HideInInspector] public PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;
    PlayerControls.MouseMovementActions mouseMovement;

    CharacterController move;

    Vector2 walkingVector;
    Vector3 yAxisMovement;

    bool jump;
    bool sprint;

    bool isGrounded;

    void Start()
    {
        isGrounded = true;

        //Cursor.lockState = CursorLockMode.Locked;

        move = GetComponent<CharacterController>();

        groundMovement = controls.GroundMovement;
        mouseMovement = controls.MouseMovement;

        groundMovement.Walking.performed += ctx => walkingVector = ctx.ReadValue<Vector2>();
        groundMovement.Jump.performed += ctx => Jump();
        groundMovement.Sprint.started += ctx => sprint = true;
        groundMovement.Sprint.canceled += ctx => sprint = false;
        mouseMovement.MouseX.performed += ctx => mouseX = ctx.ReadValue<float>() * mouseSensitivityX;
        mouseMovement.MouseY.performed += ctx => mouseY = ctx.ReadValue<float>() * mouseSensitivityY;

        StartCoroutine(SendPositionAndRotationOverNetwork());
    }

    void Update()
    {
        Move();
        MouseMove();
    }

    void Move()
    {
        isGrounded = Physics.CheckSphere(bottom.position, 0.4f, groundMask);

        if (isGrounded && yAxisMovement.y < 0)
        {
            yAxisMovement.y = -2f;
        }

        if (jump)
        {
            yAxisMovement.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            jump = false;
        }

        yAxisMovement.y += gravity * Time.deltaTime;

        if (!cantMove)
        {
            if (!sprint)
            {
                move.Move((transform.right * walkingVector.x + transform.forward * walkingVector.y) * speed * Time.deltaTime);
            }
            else
            {
                move.Move((transform.right * walkingVector.x + transform.forward * walkingVector.y) * (speed + 5) * Time.deltaTime);
            }

            move.Move(yAxisMovement * Time.deltaTime);
        }
    }

    void MouseMove()
    {
        transform.Rotate(Vector3.up, mouseX  * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minX, maxX);
       
        myCamera.eulerAngles = new Vector3(xRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void Jump()
    {
        if(isGrounded)
        {
            jump = true;
        }
    }

    IEnumerator SendPositionAndRotationOverNetwork()
    {
        while (true)
        {
            NetworkManager.Instance.SendPlayerLocation(transform.position, transform.rotation);
            yield return new WaitForSeconds(0.03f);
        }
    }

    IEnumerator CheckIfGrounded()
    {
        yield return new WaitForSeconds(0.5f);
        while (!Physics.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y - 1.25f, transform.position.z), groundMask))
        {
            Debug.Log("Not Grounded");
            yield return null;
        }

        Debug.Log("Grounded");
        isGrounded = true;
    }

    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - 1.25f, transform.position.z));
    }
}
