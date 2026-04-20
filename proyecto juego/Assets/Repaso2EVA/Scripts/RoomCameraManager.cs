using UnityEngine;
using Unity.Cinemachine; // ¡Importante! En Unity 6 el namespace cambió a este

public class RoomCameraManager : MonoBehaviour
{
    [Header("Configuración de Cámara")]
    [Tooltip("Arrastra aquí la Cinemachine Camera de esta habitación específica")]
    public CinemachineCamera roomCamera;

    // Se activa cuando algo entra en el Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos si lo que entró tiene el Tag "Player"
        if (other.CompareTag("Player"))
        {
            // Subimos la prioridad para que el Brain enfoque esta cámara
            roomCamera.Priority = 10;
        }
    }

    // Se activa cuando algo sale del Trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Bajamos la prioridad para que deje paso a la siguiente cámara
            roomCamera.Priority = 0;
        }
    }
}