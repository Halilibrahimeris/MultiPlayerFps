using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FPSDisplay : MonoBehaviour
{
    TextMeshProUGUI textmeshProUGUI;
    private float timer;

    private void Start()
    {
        textmeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timer += (Time.unscaledDeltaTime - timer) * 0.1f;

        float fps = 1.0f / timer;

        textmeshProUGUI.text = string.Format("{0:0.} FPS", fps);
    }
}
