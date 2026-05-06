using UnityEngine;

public class ControladorPlataforma : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 3f;           // Qué tan rápido se mueve
    public float distanciaSubida = 5f;     // Cuántos cuadros (unidades) va a subir

    private Vector3 posicionAbajo;         // Posición de reposo
    private Vector3 posicionArriba;        // Posición máxima
    private bool debeSubir = false;        // Interruptor lógico

    void Start()
    {
        // Guardamos la posición inicial donde la pusiste en el mapa
        posicionAbajo = transform.position;
        // Calculamos la posición de arriba sumando la distancia en el eje Y
        posicionArriba = posicionAbajo + new Vector3(0, distanciaSubida, 0);
    }

    void Update()
    {
        // Movemos la plataforma suavemente hacia el objetivo según el interruptor
        if (debeSubir)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionArriba, velocidad * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionAbajo, velocidad * Time.deltaTime);
        }
    }

    // Estas funciones son llamadas por el botón
    public void Subir()
    {
        debeSubir = true;
    }

    public void Bajar()
    {
        debeSubir = false;
    }
}