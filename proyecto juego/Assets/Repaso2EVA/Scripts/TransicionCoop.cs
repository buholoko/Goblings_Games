using UnityEngine;

public class TransicionCoop : MonoBehaviour
{
    public int jugadoresNecesarios = 2; // Cuántos jugadores hacen falta
    private int jugadoresEnZona = 0;    // Cuántos jugadores hay ahora mismo en la caja

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el objeto que entró tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            jugadoresEnZona++; // Sumamos 1 al contador

            // Verificamos si ya están todos
            if (jugadoresEnZona >= jugadoresNecesarios)
            {
                ActivarTransicion();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Si un jugador se desespera y sale de la zona, lo restamos del contador
        if (collision.CompareTag("Player"))
        {
            jugadoresEnZona--;
            if (jugadoresEnZona < 0) jugadoresEnZona = 0; // Por seguridad
        }
    }

    void ActivarTransicion()
    {
        Debug.Log("¡Los dos jugadores están listos! Cambiando de zona...");

        // AQUÍ ES DONDE LLAMAS A TU CÁMARA
        // Reemplaza este comentario por el código que usas para mover tu cámara.
    }
}