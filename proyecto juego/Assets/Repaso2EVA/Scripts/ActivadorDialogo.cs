using UnityEngine;

public class ActivadorDialogo2D : MonoBehaviour
{
    public GameObject objetoBurbuja;

    // Fíjate que ahora dice OnTriggerEnter2D y Collider2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objetoBurbuja.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objetoBurbuja.SetActive(false);
        }
    }
}