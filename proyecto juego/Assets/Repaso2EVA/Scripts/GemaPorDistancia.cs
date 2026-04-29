using UnityEngine;

public class GemaPorDistancia : MonoBehaviour
{
    private Transform playerTransform;
    public float distanciaParaRecoger = 1.5f; // Qué tan cerca debe estar el player
    public float velocidadGiro = 100f;

    void Start()
    {
        // Buscamos al jugador por su Tag al iniciar
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up * velocidadGiro * Time.deltaTime);

        if (playerTransform != null)
        {
            float distancia = Vector3.Distance(transform.position, playerTransform.position);

            // Si el player está cerca, la gema vuela hacia él (efecto imán)
            if (distancia < 5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 10f * Time.deltaTime);
            }

            // Si está pegada, la recolectamos
            if (distancia < 0.5f)
            {
                Recoger();
            }
        }
    }



    void Recoger()
    {
        Debug.Log("Gema recolectada por proximidad");
        // Aquí puedes sumar tus puntos
        Destroy(gameObject);
    
    }
}