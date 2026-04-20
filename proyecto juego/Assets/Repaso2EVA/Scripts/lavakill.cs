using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar el nivel

public class MuertePorLava : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si lo que tocó la lava es el jugador
        if (other.CompareTag(playerTag))
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("¡El jugador ha caído en la lava!");

        // Opción A: Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        /* Opción B: Si tienes un sistema de salud, aquí podrías llamar a:
           other.GetComponent<Health>().TakeDamage(9999);
        */
    }
}