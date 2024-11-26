using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorChangingText : MonoBehaviour
{
    public TMP_Text textComponent;  // El componente TextMeshPro
    public Color[] colors;          // Array de colores
    public float colorChangeSpeed = 1f;  // Velocidad de cambio de color

    private int currentColorIndex = 0;
    private float timer = 0f;

    void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();
        }

        if (colors.Length > 0)
        {
            StartCoroutine(ChangeColor());
        }
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            textComponent.color = colors[currentColorIndex];
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            yield return new WaitForSeconds(colorChangeSpeed);
        }
    }

    void Update()
    {
        if (colors.Length > 0)
        {
            timer += Time.deltaTime;

            if (timer >= colorChangeSpeed)
            {
                timer = 0f;
                currentColorIndex = (currentColorIndex + 1) % colors.Length;
                Debug.Log("Current Color: " + colors[currentColorIndex]);  // Imprime el color actual
                textComponent.color = colors[currentColorIndex];
            }
        }
    }
}
