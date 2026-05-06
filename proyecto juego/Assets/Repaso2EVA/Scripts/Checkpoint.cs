using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Esta es la "memoria" del juego. Al ser 'static', todos los scripts 
    // pueden leerla y su valor se mantiene en toda la escena.
    public static Vector3 ultimaPosicionSegura;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el objeto que entra tiene el Tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Guardamos la posición EXACTA de este checkpoint
            ultimaPosicionSegura = transform.position;
            Debug.Log("¡Checkpoint guardado en: " + ultimaPosicionSegura + "!");
        }
    }
}