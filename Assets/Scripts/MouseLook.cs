using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Global global;
    public Transform playerBody;
    
    public float mouseSensitivity = 250f;

    private float xRotation;

    private void Awake()
    {
        xRotation = Camera.main.transform.rotation.x;
    }

    void Update()
    {
        if (global.disabled)
            return;
        
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
            global.busy = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            Cursor.lockState = CursorLockMode.None;
            global.busy = false;
        }
    }
}
