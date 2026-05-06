using UnityEngine;
using UnityEngine.SceneManagement; // Esto es vital para cambiar de escenas

public class MenuManager : MonoBehaviour
{
    // Esta es la función que llamará tu botón
    public void Jugar()
    {
        // Reemplaza "Nivel1" por el nombre exacto de la escena a la que quieres ir
        SceneManager.LoadScene("MainLevel");
    }
}