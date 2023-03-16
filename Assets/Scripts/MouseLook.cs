using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Global global;
    public Transform playerBody;
    
    public float mouseSensitivity = 250f;

    private float xRotation, yRotation;

    private void Awake()
    {
        xRotation = Camera.main.transform.rotation.x;
        yRotation = playerBody.rotation.y;
    }

    void Update()
    {
        if (global.disabled)
            return;
        
        if (Input.GetKey(KeyCode.Mouse2))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            
            yRotation += mouseX;
            xRotation -= mouseY;
            
            yRotation = Mathf.Clamp(yRotation, 150f, 200f);
            xRotation = Mathf.Clamp(xRotation, 0f, 90f);
            
            playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
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
