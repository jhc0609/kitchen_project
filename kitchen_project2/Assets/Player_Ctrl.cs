using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Ctrl : MonoBehaviour
{
    Rigidbody rb;

    [Header("Rotate")]
    public float mouseSpeed;
    float yRotation;
    float xRotation;
    Camera cam;

    [Header("Move")]
    public float moveSpeed;
    float h;
    float v;

    [Header("Jump")]
    public float jumpForce;

    [Header("Ground Check")]
    public float playerHeight;
    bool grounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;                     

        rb = GetComponent<Rigidbody>();             
        rb.freezeRotation = true;                   

        cam = Camera.main;                          
    }

    void Update()
    {
        
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);

        Move();
        Rotate();

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;   
        xRotation -= mouseY;    

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); 
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             
    }

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal"); 
        v = Input.GetAxisRaw("Vertical");   

        
        Vector3 moveVec = transform.forward * v + transform.right * h;

        
        transform.position += moveVec.normalized * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
