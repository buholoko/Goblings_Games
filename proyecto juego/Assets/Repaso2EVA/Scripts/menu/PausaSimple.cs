using UnityEngine;

public class PausaSimple : MonoBehaviour
{
    // Variable para saber si el juego está pausado
    public static bool JuegoPausado = false;

    void Update()
    {
        // Detecta la tecla ESC o el botón del mando
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            Debug.Log("¡Botón presionado!"); // <-- Agrega esta línea

            if (JuegoPausado)
            {
                Reanudar();
                Debug.Log("Juego Reanudado"); // <-- Agrega esta línea
            }
            else
            {
                Pausar();
                Debug.Log("Juego Pausado"); // <-- Agrega esta línea
            }
        }
    }

    // Funciones más simples, ya no hay UI que ocultar/mostrar
    void Reanudar()
    {
        Time.timeScale = 1f;  // El tiempo vuelve a correr a velocidad normal (1)
        JuegoPausado = false; // Actualizamos el estado
    }

    void Pausar()
    {
        Time.timeScale = 0f;  // Congela el tiempo del juego (velocidad 0)
        JuegoPausado = true;  // Actualizamos el estado
    }


}