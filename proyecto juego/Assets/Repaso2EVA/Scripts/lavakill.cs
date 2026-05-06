using UnityEngine;

public class MuertePorLava : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos si lo que tocó la lava es el jugador
        if (other.CompareTag(playerTag))
        {
            Morir(other.transform);
        }
    }

    void Morir(Transform jugadorTransform)
    {
        Debug.Log("¡El jugador ha caído en la lava! Regresando al checkpoint...");

        // Aquí es donde ocurre la magia: Teletransportamos al jugador 
        // a la posición que guardó nuestro script Checkpoint.
        jugadorTransform.position = Checkpoint.ultimaPosicionSegura;
    }
}