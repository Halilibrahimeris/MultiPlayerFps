using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float SwayClamp = 0.09f;

    public float Smoothing = 3f;

    private Vector3 origin;

    private void Start()
    {
        origin = transform.localPosition;
    }
    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        input.x = Mathf.Clamp(input.x, -SwayClamp, SwayClamp);
        input.y = Mathf.Clamp(input.y, -SwayClamp, SwayClamp);

        Vector3 target = new Vector3(-input.x, -input.y, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, target + origin, Time.deltaTime * Smoothing);

    }
}
