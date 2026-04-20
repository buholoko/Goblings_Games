using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [Header("Configuración de Altura")]
    [SerializeField] private float alturaNormal = 2.0f;
    [SerializeField] private float alturaAgachado = 1.0f;
    [SerializeField] private float velocidadAgachado = 10f;

    [Header("Referencias")]
    [SerializeField] private Transform camara; // Arrastra tu cámara aquí
    [SerializeField] private CharacterController controller;

    private float alturaObjetivo;
    private float posicionCamaraOriginal;
    private float posicionCamaraAgachado;

    void Start()
    {
        if (controller == null) controller = GetComponent<CharacterController>();

        alturaObjetivo = alturaNormal;
        posicionCamaraOriginal = camara.localPosition.y;
        posicionCamaraAgachado = posicionCamaraOriginal * (alturaAgachado / alturaNormal);
    }

    void Update()
    {
        // Detectar si el jugador mantiene presionada la tecla
        if (Input.GetKey(KeyCode.LeftControl))
        {
            alturaObjetivo = alturaAgachado;
        }
        else
        {
            // Opcional: Solo levantarse si no hay nada encima del jugador
            if (!HayTechoEncima())
            {
                alturaObjetivo = alturaNormal;
            }
        }

        // Aplicar los cambios suavemente (Lerp)
        controller.height = Mathf.Lerp(controller.height, alturaObjetivo, Time.deltaTime * velocidadAgachado);

        // Ajustar la posición de la cámara proporcionalmente
        float nuevaYCamara = Mathf.Lerp(camara.localPosition.y,
            (alturaObjetivo == alturaAgachado) ? posicionCamaraAgachado : posicionCamaraOriginal,
            Time.deltaTime * velocidadAgachado);

        camara.localPosition = new Vector3(camara.localPosition.x, nuevaYCamara, camara.localPosition.z);
    }

    // Función extra: Evita que el jugador se levante si está bajo un túnel
    bool HayTechoEncima()
    {
        float radio = controller.radius * 0.9f;
        Vector3 origen = transform.position + Vector3.up * (controller.height - radio);
        return Physics.SphereCast(origen, radio, Vector3.up, out RaycastHit hit, 0.5f);
    }
}