using UnityEngine;

public class BotonPlataforma : MonoBehaviour
{
    // Por defecto configurado para Player2
    [Tag] public string targetTag = "Player2";

    // Aquí arrastras la plataforma que quieres mover
    public ControladorPlataforma plataformaScript;

    [Header("Ajuste Visual")]
    public float pressedDepth = 0.2f;
    private Vector3 upPos;
    private Vector3 downPos;

    void Start()
    {
        // Guardamos la posición original y la posición hundida del botón
        upPos = transform.position;
        downPos = upPos + new Vector3(0, -pressedDepth, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el Player 2 se sube al botón
        if (other.CompareTag(targetTag))
        {
            transform.position = downPos; // El botón se hunde
            if (plataformaScript != null) plataformaScript.Subir(); // La plataforma sube
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Si el Player 2 se baja del botón
        if (other.CompareTag(targetTag))
        {
            transform.position = upPos; // El botón vuelve a subir
            if (plataformaScript != null) plataformaScript.Bajar(); // La plataforma baja
        }
    }
}