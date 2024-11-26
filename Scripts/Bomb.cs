using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false; // Evita que el sonido se reproduzca al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproducir el sonido de explosión solo cuando la bomba es cortada
            audioSource.Play();

            // Asegurarse de que la bomba se destruya después de que el sonido haya terminado
            Destroy(gameObject, audioSource.clip.length);

            // Llamar al método Explode del GameManager
            FindObjectOfType<GameManager>().Explode();
        }
    }
}
