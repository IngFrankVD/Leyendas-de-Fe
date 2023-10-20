using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    public Animator animator;
    public VirtualJoystick Joystick;
    public Rigidbody rb;
    
    private float valorSuavizado;
    private float velocidad;
    Vector3 lastPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        velocidad = (transform.position - lastPosition).magnitude * 7;
        lastPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {   
        if (Joystick.Direction.magnitude > 0.3){
            valorSuavizado = Mathf.Lerp(valorSuavizado, velocidad, 0.05f);
        } else {
            valorSuavizado = Mathf.Lerp(valorSuavizado, 0f, 0.05f); // Si no está en el suelo, suaviza a 0.
        }

        animator.SetFloat("Blend", valorSuavizado);

    }
}
