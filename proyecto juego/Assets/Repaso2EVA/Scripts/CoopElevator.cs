using UnityEngine;

public class CoopElevator : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float heightOffset = 5f; // Cuánto va a subir
    [SerializeField] private float speed = 2f;  // Velocidad de movimiento

    private Vector3 startPos; // Posición inicial de la plataforma
    private Vector3 endPos;  // Posición final (calculada a partir de startPos + heightOffset)
    private Vector3 targetPos; // Posición a la que se moverá actualmente (startPos o endPos)

    [Header("Estado")] // Información sobre el estado actual de la plataforma
    [SerializeField] private int playersOnTop = 0; // Contador de jugadores

    void Awake()
    {
        startPos = transform.position; // Guardamos la posición inicial
        endPos = startPos + new Vector3(0, heightOffset, 0); // Calculamos la posición final sumando el offset a la posición inicial
        targetPos = startPos; // Empezamos en la posición inicial
    }

    void Update()
    {
        // Solo se mueve si hay 2 jugadores, de lo contrario vuelve al inicio
        if (playersOnTop >= 2)
        {
            targetPos = endPos;
        }
        else // Si no hay suficientes jugadores, volvemos a la posición inicial
        {
            targetPos = startPos;
        }

        // Movimiento suave
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) // Detectamos cuando un jugador sube a la plataforma
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

    private void OnCollisionExit(Collision collision) // Detectamos cuando un jugador baja de la plataforma
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