using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private bool slicing;
    private AudioSource audioSource;

    public Vector3 direction { get; private set; }
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;

        // Reproducir sonido de slicing al iniciar
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;

        // Detener sonido de slicing al detenerse
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;

        // Continuar el sonido mientras se está en movimiento
        if (!audioSource.isPlaying && velocity > minSliceVelocity)
        {
            audioSource.Play();
        }
        else if (audioSource.isPlaying && velocity <= minSliceVelocity)
        {
            audioSource.Stop();
        }
    }
}
