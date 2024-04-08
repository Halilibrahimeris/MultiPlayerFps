using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dash : MonoBehaviour
{
    public CharacterMovement ThirdPersonMovement;
    public Slider DashSlider;
    [Space]
    private float timer;
    public float DashCooldown;
    public float DashTime;
    public float DashForce;

    private void Start()
    {
        DashSlider.maxValue = DashCooldown;
        DashSlider.value = 0;
        DashSlider.minValue = 0;
    }
    private void FixedUpdate()
    {
        if (timer <= DashCooldown + 0.3f)
        {
            timer += Time.deltaTime;
            DashSlider.value = timer;
        }
        if (Input.GetKey(KeyCode.LeftShift) && timer >= DashCooldown)
        {
            timer = 0;
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        float startTime = Time.time;

        while(Time.time < startTime + DashTime)
        {
            ThirdPersonMovement.controller.Move(ThirdPersonMovement.moveDir * DashForce * Time.deltaTime);

            yield return null;
        }
    }
}
