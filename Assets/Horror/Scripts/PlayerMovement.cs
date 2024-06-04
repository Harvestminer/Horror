/*
 * Harvestminer
 * https://github.com/Harvestminer
 */

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
#region Fields
    public float MoveSpeed = 5f;
    public float LookSensitivity = 100f;
    public Transform player;

    private float yRotation = 0f;
#endregion // Fields

    void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
        MouseRotation();
	}

    private void MouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * LookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * LookSensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
