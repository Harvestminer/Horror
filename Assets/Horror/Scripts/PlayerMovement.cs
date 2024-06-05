/*
 * Harvestminer
 * https://github.com/Harvestminer
 */

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
#region Fields
    public float LookSensitivity = 100f;
    public float MoveSpeed = 12f;
    public float JumpHeight = 3f;
    
    public Transform Player;
    public CharacterController Controller;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    private bool isGrounded;

    private float yRotation = 0f;
    private float gravity = -9.81f;
    private Vector3 velocity;
#endregion // Fields

    void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
    {
        MouseRotation();
        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Move player
        Vector3 move = Player.transform.right * x + Player.transform.forward * z;
        Controller.Move(MoveSpeed * Time.deltaTime * move);

        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);


        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        Controller.Move(velocity * Time.deltaTime);
    }

    private void MouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * LookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * LookSensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90, 90);

        this.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }
}
