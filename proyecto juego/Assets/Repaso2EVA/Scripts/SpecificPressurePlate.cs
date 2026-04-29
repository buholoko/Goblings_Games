using UnityEngine;
using UnityEngine.Events;

public class SpecificPressurePlate : MonoBehaviour
{
    [Header("Configuración de Activación")]
    // Cambiamos [Tag] por [TagField] para que el Drawer del Editor lo reconozca
    [TagField] 
    [SerializeField] private string targetTag = "Untagged"; 

    [Header("Eventos")]
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        // Comparamos con el tag seleccionado en el desplegable
        if (other.CompareTag(targetTag))
        {
            onActivate.Invoke();
            Debug.Log($"Placa presionada por: {targetTag}");
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