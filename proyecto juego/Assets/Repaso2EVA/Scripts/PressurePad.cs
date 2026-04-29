using UnityEngine;

public partial class PressurePad : MonoBehaviour
{
    [Header("Configuración de la Puerta")] // Para que el botón sepa a qué puerta controlar
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
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * speed); //  Lerp para suavizar el movimiento
    }

    private void OnTriggerEnter(Collider other) //  Detectamos cuando algo entra en el área del botón
    {
        // Esta línea imprimirá en la consola de Unity quién entró al botón
        Debug.Log("ALGO ENTRÓ: " + other.name + " | TAG: " + other.tag);

        if (other.CompareTag("Player")) //  Solo reaccionamos si el objeto que entró tiene la etiqueta "Player"
        {
            isPressed = true; //    Marcamos el botón como presionado
            if (door != null) door.SetDoorState(true);  //  Si tenemos una referencia a la puerta, le decimos que se abra (true)
        }
    }

    private void OnTriggerExit(Collider other) //   Detectamos cuando algo sale del área del botón
    {
        // También es útil saber cuándo sale
        Debug.Log("ALGO SALIÓ: " + other.name);

        if (other.CompareTag("Player"))//   Solo reaccionamos si el objeto que salió tiene la etiqueta "Player" 
        {
            isPressed = false; //   Marcamos el botón como no presionado
            if (door != null) door.SetDoorState(false); //  Si tenemos una referencia a la puerta, le decimos que se cierre (false)
        }
    }
}