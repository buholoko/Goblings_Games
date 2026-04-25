using UnityEngine;

public partial class PressurePad : MonoBehaviour
{
    [Header("Configuración de la Puerta")]
    public DoorController door;

    [Header("Configuración del Botón")]
    public float sinkAmount = 0.1f; // Cuánto se hunde (ej: 10cm)
    public float speed = 5f;        // Qué tan rápido baja/sube

    private Vector3 upPosition;
    private Vector3 downPosition;
    private bool isPressed = false;

    void Start()
    {
        // Guardamos la posición inicial (arriba)
        upPosition = transform.localPosition;
        // Calculamos la posición hundida
        downPosition = upPosition + new Vector3(0, -sinkAmount, 0);
    }

    void Update()
    {
        // Movemos el botón suavemente hacia su objetivo
        Vector3 targetPos = isPressed ? downPosition : upPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Esta línea imprimirá en la consola de Unity quién entró al botón
        Debug.Log("ALGO ENTRÓ: " + other.name + " | TAG: " + other.tag);

        if (other.CompareTag("Player"))
        {
            isPressed = true;
            if (door != null) door.SetDoorState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // También es útil saber cuándo sale
        Debug.Log("ALGO SALIÓ: " + other.name);

        if (other.CompareTag("Player"))
        {
            isPressed = false;
            if (door != null) door.SetDoorState(false);
        }
    }
}