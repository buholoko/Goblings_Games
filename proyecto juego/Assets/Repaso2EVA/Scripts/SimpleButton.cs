using UnityEngine;

public class SimpleButton : MonoBehaviour
{
    [Tag] public string targetTag = "Player1"; // Tu selector de Tags automático
    public DoorController doorScript; // Arrastra la puerta aquí directamente

    [Header("Ajuste Visual (Opcional)")]
    public float pressedDepth = 0.2f;
    private Vector3 upPos;
    private Vector3 downPos;

    void Start()
    {
        upPos = transform.position;
        downPos = upPos + new Vector3(0, -pressedDepth, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            transform.position = downPos; // El botón baja visualmente
            if (doorScript != null) doorScript.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            transform.position = upPos; // El botón sube
            if (doorScript != null) doorScript.CloseDoor();
        }
    }
}
