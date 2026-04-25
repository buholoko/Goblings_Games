using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Vector3 offSet = new Vector3(0, 3, 0); // Cuánto sube
    public float speed = 2f;

    private Vector3 closedPosition; // Posición cerrada de la puerta
    private Vector3 openPosition;   // Posición abierta de la puerta
    private bool isOpen = false; // Estado actual de la puerta

    void Start() // Inicializamos las posiciones de la puerta
    {
        closedPosition = transform.position; // Guardamos la posición inicial como la posición cerrada
        openPosition = closedPosition + offSet; // Calculamos la posición abierta sumando el offset a la posición cerrada
    }

    void Update() // Movemos la puerta cada frame hacia su posición objetivo
    {
        // Movemos la puerta hacia el objetivo actual
        Vector3 target = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed); // Lerp suaviza el movimiento hacia la posición objetivo
    }

    public void SetDoorState(bool state) // Método público para cambiar el estado de la puerta (abierta o cerrada)
    {
        isOpen = state; // Cambiamos el estado de la puerta según el parámetro recibido (true para abrir, false para cerrar)
    }
}