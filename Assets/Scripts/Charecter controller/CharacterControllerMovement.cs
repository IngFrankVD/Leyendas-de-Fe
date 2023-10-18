using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    [Header("Mobil Joystick")]
    public VirtualJoystick floatingJoystick;

    [Header("Orietation")]
    public Transform forwardTransform; // Asigna el transform deseado en el inspector de Unity
    public Transform player;
    public Transform playerObj;
        
    [Header("Walk settings")]
    [SerializeField]private float moveSpeed = 5.0f;
    [SerializeField]private float rotationSpeed = 20.0f;
    private CharacterController controller;
    Vector3 moveDirection ;
    float horizontalInput ;
    float verticalInput;

    [Header("Gravity settings")]
    [SerializeField]private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;
    

    [Header("Jump settings")]
    [SerializeField] private float maxNumberOfJumps = 2f;
    [SerializeField] private float jumpPower = 2f;
    private float _numberOfJumps ;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDirection = new Vector3(0,0,0);
        
        // Obtenemos la entrada del jugador del joystick para mover al personaje.
        horizontalInput = floatingJoystick.Horizontal;
        verticalInput = floatingJoystick.Vertical;

        ApplyDirection();
        ApplyGravity();
        ApplyMovement();

    }
    public void ApplyDirection(){
        // Calculamos la dirección de movimiento en base a la entrada del jugador.
        Vector3 forwardMovement = forwardTransform.forward * verticalInput;
        Vector3 rightMovement = forwardTransform.right * horizontalInput;

        // Sumamos las direcciones para obtener el movimiento final.
        moveDirection = forwardMovement + rightMovement;
    }
    public void ApplyMovement(){
        if(floatingJoystick.Direction.magnitude > 0.3 || floatingJoystick.Direction.magnitude == 0){
            // Movemos al personaje utilizando Character Controller.
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        // Rotamos el personaje en base a la entrada del jugador.
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        // roate player object
        Vector3 inputDir = forwardTransform.forward * verticalInput + forwardTransform.right * horizontalInput;
        if (inputDir != Vector3.zero){
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
    public void ApplyGravity(){
        // Funcion de gravedad
        if(IsGrounded() && _velocity < 0.0f){
            _velocity = -1.0f;
        }else{
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }
        // Aplicamos la gravedad
        moveDirection.y = _velocity;
    }

    public void Jump()
    {
        if (!IsGrounded() && _numberOfJumps >= maxNumberOfJumps) return;
        if (_numberOfJumps == 0) StartCoroutine(WaitForLanding());
        
        _numberOfJumps++;
        _velocity = jumpPower;
    }

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);

        _numberOfJumps = 0;
    }

    private bool IsGrounded() => controller.isGrounded;
}
