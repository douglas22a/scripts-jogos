using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] Rigidbody playerRb;
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask groundLayer;

    bool isGrounded;

    int speed = 5;
    int jumpForce = 5;

    float mouseSense = 3f;
    float mouseX, mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 inputs = new Vector3(moveX, 0f, moveZ).normalized;
        Vector3 movement = transform.TransformDirection(inputs) * speed;

        movement.y = playerRb.velocity.y;

        playerRb.velocity = movement;

        //Mouse and Sensibility!
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            mouseX += Input.GetAxis("Mouse X") * mouseSense;
            mouseY += -Input.GetAxis("Mouse Y") * mouseSense;

            //limit movment!
            mouseY = Mathf.Clamp(mouseY, -90, 90);

            //movi player rotation!
            transform.rotation = Quaternion.Euler(0f, mouseX, 0f);

            //movi player camera vertical!
            playerCamera.transform.localRotation = Quaternion.Euler(mouseY, 0f, 0f);
        }

        //Jump
        isGrounded = IsGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
        }
    }

    //Check if it's on the Ground!
    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, groundLayer);
    }
}
