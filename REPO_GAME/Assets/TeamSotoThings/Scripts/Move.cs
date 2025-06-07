using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCamera;
     public GameObject winner;

    //public Animator animador;
    private float slower = 1.5f; // Velocidad reducida en el arbusto
    private float currentSpeed=1.5f;
    public float normalSpeed=1.5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;
        private int collisionsCount= 0;
    private Vector3 velocity;
    
    private float xRotation = 0f;


    void Start()
    {
        //animador = GetComponent<Animator>();
        //transform.position= new Vector3(158,0.200000003f,72);
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al centro de la pantalla
    }

    void Update()
    {
if(collisionsCount >= 2){
winner.SetActive(true);
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //animador.SetBool("isWaking", x != 0 || z != 0);
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotación vertical

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collisionsCount+=1;
            
            Debug.Log("Catched!");
        }

    }
}