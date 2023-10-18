using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorController : MonoBehaviour
{
    public Animator animator;
    public VirtualJoystick Joystick;
    public CharacterController controller;

    private float valorSuavizado;
    private float velocidad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        velocidad = controller.velocity.magnitude * 0.1f;

        if (Joystick.Direction.magnitude > 0.3){
            valorSuavizado = Mathf.Lerp(valorSuavizado, velocidad, 0.05f);
        } else {
            valorSuavizado = Mathf.Lerp(valorSuavizado, 0f, 0.05f); // Si no está en el suelo, suaviza a 0.
        }

        animator.SetFloat("Blend", valorSuavizado);

    }
}
