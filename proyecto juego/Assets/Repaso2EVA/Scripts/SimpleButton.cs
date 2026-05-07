using UnityEngine;

public class SimpleButton : MonoBehaviour
{
    // Cambiado automáticamente a "Player2"
    [Tag] public string targetTag = "Player2";

    // Controlador de la plataforma
    public ControladorPlataforma plataformaScript;

    [Header("Ajuste Visual (Opcional)")]
    public float pressedDepth = 0.2f;
    private Vector3 upPos;
    private Vector3 downPos;

    // Candado para que solo se presione una vez
    private bool yaPresionado = false;

    void Start()
    {
        upPos = transform.position;
        downPos = upPos + new Vector3(0, -pressedDepth, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si es el Player 2 Y el botón NO ha sido presionado aún
        if (other.CompareTag(targetTag) && !yaPresionado)
        {
            yaPresionado = true; // Bloqueamos el botón para siempre
            transform.position = downPos; // El botón baja visualmente

            if (plataformaScript != null) plataformaScript.Subir(); // La plataforma sube
        }
    }

    // ¡Hemos borrado por completo la función OnTriggerExit2D!
}