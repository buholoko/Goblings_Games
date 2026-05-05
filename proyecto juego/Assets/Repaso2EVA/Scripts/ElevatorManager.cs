using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    [Header("Referencias")]
    public PlataformaIndividual plataforma1;
    public PlataformaIndividual plataforma2;

    [Header("Configuración")]
    public float yOffset = 5f;
    public float speed = 3f;

    private Vector3 plat1Start, plat2Start;
    private Vector3 plat1End, plat2End;
    private Vector3 target1, target2;

    void Start()
    {
        // Guardamos las posiciones iniciales reales
        plat1Start = plataforma1.transform.position;
        plat2Start = plataforma2.transform.position;

        // Calculamos los destinos sumando el offset a la Y inicial
        plat1End = new Vector3(plat1Start.x, plat1Start.y + yOffset, plat1Start.z);
        plat2End = new Vector3(plat2Start.x, plat2Start.y + yOffset, plat2Start.z);
    }

    void Update()
    {
        // Condición: Ambos jugadores deben estar en sus plataformas
        if (plataforma1.estaOcupada && plataforma2.estaOcupada)
        {
            Debug.Log("Ascensor subendo...");
            target1 = plat1End;
            target2 = plat2End;
        }
        else
        {
            Debug.Log("Ascensor bajando...");
            target1 = plat1Start;
            target2 = plat2Start;
        }

        // Movimiento usando MoveTowards (más estable que Lerp para físicas)
        plataforma1.transform.position = Vector3.MoveTowards(plataforma1.transform.position, target1, speed * Time.deltaTime);
        plataforma2.transform.position = Vector3.MoveTowards(plataforma2.transform.position, target2, speed * Time.deltaTime);
    }
}