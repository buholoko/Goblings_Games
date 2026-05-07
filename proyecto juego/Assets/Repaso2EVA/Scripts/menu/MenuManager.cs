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

    public void Salir()
        {
            Debug.Log("Saliendo del juego...");
        // Esto cerrará la aplicación. En el editor no hará nada, pero en un build sí.
        Application.Quit();
    }

}