using UnityEngine;

public class ElevadorSincronizado : MonoBehaviour
{
    [Header("Conexión")]
    public string tagDelJugador = "Player1"; 
    public ElevadorSincronizado otroElevador; 

    [Header("Movimiento")]
    public float distanciaSubida = 5f;
    public float velocidad = 3f;

    private Vector3 posicionFinal;
    public bool estoyListo = false; 
    private bool activado = false;  

    void Start()
    {
        posicionFinal = transform.position + new Vector3(0, distanciaSubida, 0);
        
        // Chivato 1: Comprobar que los elevadores están conectados
        if (otroElevador == null) 
            Debug.LogWarning("hey OYE: Al elevador de " + tagDelJugador + " le falta que le conectes el otro elevador en el Inspector.");
    }

    void Update()
    {
        if (!activado && estoyListo && otroElevador != null && otroElevador.estoyListo)
        {
            activado = true;
            otroElevador.activado = true;
            Debug.Log(" ¡Sincronización exitosa! Subiendo...");
        }

        if (activado)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionFinal, velocidad * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activado && other.CompareTag(tagDelJugador))
        {
            estoyListo = true;

            // ¡NUEVA LÍNEA! Pegamos el jugador al elevador
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!activado && other.CompareTag(tagDelJugador))
        {
            estoyListo = false;

            // ¡NUEVA LÍNEA! Despegamos al jugador (null significa que ya no tiene padre)
            other.transform.SetParent(null);
        }
    }
}
