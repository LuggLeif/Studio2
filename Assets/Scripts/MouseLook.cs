using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    [SerializeField] private Global Global;
    
    public float mouseSensitivity = 250f;

    float xRotation = 40.91f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse2))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, 0f, 90f);
            
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
            
        if (Input.GetMouseButtonDown(2))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Global.Busy = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            Cursor.lockState = CursorLockMode.None;
            Global.Busy = false;
        }
    }
}
