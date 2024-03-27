using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float WalkSpeed = 4f;
    public float SprintSpeed = 6f;
    public float MaxVelocityChange = 10f;
    [Space]
    public float JumpPower = 5f;
    [Space]
    public float AirControl = 0.5f;
    [Space]
    public Weapon Weapon;
    private Vector2 input;
    private Rigidbody _rb;

    private bool sprinting;
    private bool jumping;
    private bool grounded;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
        sprinting = Input.GetButton("Sprint");
        jumping = Input.GetButton("Jump");
    }

    private void OnTriggerStay(Collider other)
    {
        grounded = true;
    }

    private void FixedUpdate()
    {
        if (grounded)
        {
            if (jumping)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, JumpPower, _rb.velocity.z);
                //Weapon.SpreadIntensity = 7f;
            }
            else if (input.magnitude > 0.5f)
            {
                _rb.AddForce(CalculateMovement(sprinting ? SprintSpeed : WalkSpeed), ForceMode.VelocityChange);
                //Weapon.SpreadIntensity = 4f;
            }
            else
            {
                var velocity1 = _rb.velocity;
                velocity1 = new Vector3(velocity1.x * 0.2f * Time.deltaTime, velocity1.y, velocity1.z * 0.2f * Time.deltaTime);
                _rb.velocity = velocity1;
                //Weapon.SpreadIntensity = 2.5f;
            }
        }
        else
        {
            if (input.magnitude > 0.5f)
            {
                _rb.AddForce(CalculateMovement(sprinting ? SprintSpeed * AirControl : WalkSpeed * AirControl), ForceMode.VelocityChange);
               // Weapon.SpreadIntensity = 10f;
            }
            else
            {
                var velocity1 = _rb.velocity;
                velocity1 = new Vector3(velocity1.x * 0.2f * Time.deltaTime, velocity1.y, velocity1.z * 0.2f * Time.deltaTime);
                _rb.velocity = velocity1;
                //Weapon.SpreadIntensity = 7.5f;
            }
        }

        grounded = false;
    }

    public Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = _rb.velocity;

        if(input.magnitude >= 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocity.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
            velocity.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);

            velocityChange.y = 0;

            return (velocityChange);
        }
        else
        {
            return new Vector3();
        }

    }
}
