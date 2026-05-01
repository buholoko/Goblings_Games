using UnityEngine;

public class CoopElevator : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float heightOffset = 5f; // Cuánto va a subir
    [SerializeField] private float speed = 2f;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 targetPos;

    [Header("Estado")]
    [SerializeField] private int playersOnTop = 0; // Contador de jugadores

    void Awake()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, heightOffset, 0);
        targetPos = startPos;
    }

    void Update()
    {
        // Solo se mueve si hay 2 jugadores, de lo contrario vuelve al inicio
        if (playersOnTop >= 2)
        {
            targetPos = endPos;
        }
        else
        {
            targetPos = startPos;
        }

        // Movimiento suave
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si lo que subió es un jugador (usando tus Tags)
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            playersOnTop++;

            // Truco importante: Hacer al jugador "hijo" de la plataforma 
            // para que no se resbale mientras esta sube
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            playersOnTop--;
            if (playersOnTop < 0) playersOnTop = 0;

            // El jugador deja de ser hijo de la plataforma al bajar
            collision.transform.SetParent(null);
        }
    }
}