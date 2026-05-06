using UnityEngine;
using UnityEngine.Events;

public class SpecificPressurePlate : MonoBehaviour
{
    [Header("Configuración de Activación")]
    // Cambiamos [Tag] por [TagField] para que el Drawer del Editor lo reconozca
    [TagField] 
    [SerializeField] private string targetTag = "Untagged"; 

    [Header("Eventos")]
    public UnityEvent onActivate; //    Evento para activar la placa
    public UnityEvent onDeactivate; //    Evento para desactivar la placa

    private void OnTriggerEnter(Collider other)
    {
        // Comparamos con el tag seleccionado en el desplegable
        if (other.CompareTag(targetTag))
        {
            onActivate.Invoke(); // Activamos el evento de la placa
            Debug.Log($"Placa presionada por: {targetTag}");    // Solo para confirmar en la consola qué tag activó la placa
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            onDeactivate.Invoke();
            Debug.Log($"Placa liberada por: {targetTag}");
        }
    }
}