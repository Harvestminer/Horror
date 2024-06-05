/*
 * Harvestminer
 * https://github.com/Harvestminer
 */

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
#region Fields
    [Header("Player Movement & Camera")]
    public float LookSensitivity = 100f;

    [SerializeField]
    private float MoveSpeed = 12f;
    [SerializeField]
    private float JumpHeight = 3f;
    [SerializeField]
    private float GroundDistance = 0.1f;

    private bool isGrounded;

    private float yRotation = 0f;
    private float gravity = -9.81f;
    private Vector3 velocity;
    
    private Transform groundCheck;
    private Transform player;
    private CharacterController controller;
#endregion // Fields

    void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;

        var parent = this.transform.parent;

        var go = new GameObject("Ground_Check").transform;
        go.parent = parent;
        go.localPosition = Vector3.down;
        groundCheck = go;

        player = parent;
        controller = player.GetComponent<CharacterController>();
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
        Vector3 move = player.transform.right * x + player.transform.forward * z;
        controller.Move(MoveSpeed * Time.deltaTime * move);

        isGrounded = Physics.CheckSphere(groundCheck.position, GroundDistance);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void MouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * LookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * LookSensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90, 90);

        this.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
