using UnityEngine;

public class GemaMultijugador : MonoBehaviour
{
    [Header("Configuración de Dueño")]
    [TagField] public string tagDelDueño = "Untagged";

    [Header("Ajustes de Movimiento")]
    public float distanciaDeteccion = 5.0f;
    public float distanciaParaRecoger = 0.6f; // Un poco más de margen
    public float velocidadVuelo = 8.0f;
    public float velocidadGiro = 150f;

    private Transform playerObjetivo;
    private Collider playerCollider;

    void Update()
    {
        // 1. Buscamos al dueño y su Collider si aún no los tenemos
        if (playerObjetivo == null)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag(tagDelDueño);
            if (jugador != null)
            {
                playerObjetivo = jugador.transform;
                // Intentamos obtener el collider una sola vez para ahorrar recursos
                playerCollider = jugador.GetComponent<Collider>();
            }
            return;
        }

        // 2. Giro visual
        transform.Rotate(Vector3.up * velocidadGiro * Time.deltaTime);

        // 3. Determinar el punto exacto a donde debe volar la gema
        // Si el player tiene collider, volamos a su centro (pecho), si no, a su posición (pies)
        Vector3 puntoObjetivo = (playerCollider != null) ? playerCollider.bounds.center : playerObjetivo.position;

        float distancia = Vector3.Distance(transform.position, puntoObjetivo);

        // 4. Lógica de "Imán" (Vuelo hacia el centro del cuerpo)
        if (distancia < distanciaDeteccion)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                puntoObjetivo,
                velocidadVuelo * Time.deltaTime
            );
        }

        // 5. Lógica de Recolección
        if (distancia < distanciaParaRecoger)
        {
            Recoger();
        }
    }

    void Recoger()
    {
        Debug.Log($"¡Gema recogida por el centro de {tagDelDueño}!");
        Destroy(gameObject);
    }
}

// Mantenemos esto al final del archivo para el selector de Tags
public class TagFieldAttribute : PropertyAttribute { }