using UnityEngine;

public class Door : MonoBehaviour // <-- El archivo se debe llamar Door.cs
{
    [SerializeField] private Vector3 openPositionOffset = new Vector3(0, 5, 0);
    [SerializeField] private float speed = 3f;

    private Vector3 closedPosition;
    private Vector3 targetPosition;

    void Start()
    {
        closedPosition = transform.position;
        targetPosition = closedPosition;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    // Estas funciones DEBEN ser 'public' para que aparezcan en el Inspector
    public void OpenDoor()
    {
        targetPosition = closedPosition + openPositionOffset;
    }

    public void CloseDoor()
    {
        targetPosition = closedPosition;
    }
}