using UnityEngine;

public class DoorController : MonoBehaviour
{
    public enum DoorState { Cerrada, Abriendo, Abierta, Cerrando }

    [Header("Configuración")]
    public DoorState currentState = DoorState.Cerrada;
    public float yOffset = 5f;
    public float speed = 3f;

    private Vector3 closedPos;
    private Vector3 openPos;

    void Awake()
    {
        // Guardamos las posiciones exactas al iniciar el juego
        closedPos = transform.position;
        openPos = new Vector3(closedPos.x, closedPos.y + yOffset, closedPos.z);
    }

    void Update()
    {
        if (currentState == DoorState.Abriendo)
        {
            // Movemos hacia la posición abierta
            transform.position = Vector3.MoveTowards(transform.position, openPos, speed * Time.deltaTime);

            // Si ya llegó, cambiamos estado a Abierta para que deje de calcular
            if (Vector3.Distance(transform.position, openPos) < 0.001f)
                currentState = DoorState.Abierta;
        }
        else if (currentState == DoorState.Cerrando)
        {
            // Movemos hacia la posición cerrada
            transform.position = Vector3.MoveTowards(transform.position, closedPos, speed * Time.deltaTime);

            // Si ya llegó, cambiamos estado a Cerrada
            if (Vector3.Distance(transform.position, closedPos) < 0.001f)
                currentState = DoorState.Cerrada;
        }
    }

    public void OpenDoor()
    {
        currentState = DoorState.Abriendo;
    }

    public void CloseDoor()
    {
        currentState = DoorState.Cerrando;
    }
}