using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(3, 10)] // Esto crea un cuadro de texto grande en el Inspector
    public string dialogueText;
    public bool hasBeenTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si es el jugador el que entró
        if (other.CompareTag("Player") && !hasBeenTriggered)
        {
            ShowDialogue();
            hasBeenTriggered = true; // Para que no se repita cada vez que pases
        }
    }

    void ShowDialogue()
    {
        // Aquí conectas con tu UI (usando TextMeshPro o tu manager de diálogos)
        Debug.Log("Mensaje: " + dialogueText);

        
    }
}