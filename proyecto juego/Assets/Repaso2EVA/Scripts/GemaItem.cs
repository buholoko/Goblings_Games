using UnityEngine;

public class GemaItem : MonoBehaviour
{
    [SerializeField] private int valorGema = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si lo que entró en el trigger es el Player
        if (other.CompareTag("Player 2"))
        {
            Recoger();
        }
    }

    void Recoger()
    {
        // Aquí podrías sumar puntos a un contador global
        Debug.Log("¡Gema recogida! + " + valorGema);

        // Destruimos la gema
        Destroy(gameObject);

        // Opcional: Instanciar un efecto de partículas o sonido aquí
    }
    void Update()
    {
        // Rotación constante
        transform.Rotate(Vector3.up * 100f * Time.deltaTime);

        // Movimiento de arriba a abajo suave
        float nuevoY = Mathf.Sin(Time.time * 2f) * 0.1f;
        transform.position += new Vector3(0, nuevoY, 0) * Time.deltaTime;
    }
}