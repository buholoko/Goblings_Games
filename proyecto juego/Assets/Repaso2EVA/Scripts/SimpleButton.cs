using UnityEngine;

public class SimpleButton : MonoBehaviour
{
    // Escribe "Player2" en el Inspector de Unity para que solo él pueda activarlo
    [Tag] public string targetTag = "Player2";

    // Aquí arrastraremos la plataforma que queremos mover
    public ControladorPlataforma plataformaScript;

    [Header("Ajuste Visual (Opcional)")]
    public float pressedDepth = 0.2f;
    private Vector3 upPos;
    private Vector3 downPos;

    void Start()
    {
        upPos = transform.position;
        downPos = upPos + new Vector3(0, -pressedDepth, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            transform.position = downPos; // El botón baja visualmente
            if (plataformaScript != null) plataformaScript.Subir(); // Le dice a la plataforma que suba
        }
    }

    // ¡CORREGIDO! Ahora dice OnTriggerExit2D
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            transform.position = upPos; // El botón sube visualmente
            if (plataformaScript != null) plataformaScript.Bajar(); // Le dice a la plataforma que baje
        }
    }
}
