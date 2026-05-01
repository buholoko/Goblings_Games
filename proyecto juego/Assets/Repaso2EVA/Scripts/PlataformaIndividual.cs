using UnityEngine;

public class PlataformaIndividual : MonoBehaviour
{
    [Tag] public string targetTag = "Player1"; // El tag asignado a esta plataforma
    public bool estaOcupada = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            estaOcupada = true;
            // El jugador se mueve con la plataforma
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            estaOcupada = false;
            // El jugador recupera su independencia
            collision.transform.SetParent(null);
        }
    }
}