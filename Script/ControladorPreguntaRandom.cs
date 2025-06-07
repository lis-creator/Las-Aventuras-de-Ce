using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador que gestiona preguntas aleatorias cuando el jugador colisiona con un enemigo.
/// Presenta una palabra y opciones, evaluando si comienza con la letra C.
/// </summary>

public class ControladorPreguntaRandom : MonoBehaviour
{
    /// <summary>Panel de la interfaz donde se muestra la pregunta y opciones.</summary>
    public GameObject panelPregunta;

      /// <summary>Texto que muestra la pregunta.</summary>
    
    public Text preguntaTexto;

      /// <summary>Botones de opciones de respuesta.</summary>
    
    public Button[] botonesOpciones;
     /// <summary>Texto que muestra retroalimentación al jugador.</summary>
    
    public Text retroalimentacionTexto;
    /// <summary>Referencia al script que gestiona la vida del jugador.</summary>
    
    public Vida jugadorVida; 
      /// <summary>Almacena el enemigo actual que activó la pregunta.</summary>

    private GameObject enemigoActual;

     /// <summary>Controla si se puede quitar vida al jugador por una respuesta incorrecta.</summary>
    
    private bool puedeQuitarVida = true;

     /// <summary>Palabras correctas que comienzan con la letra C.</summary>
    private string[] palabrasCorrectas = { "Casa", "Carro", "Cielo", "Conejo", "Cocina" };
     /// <summary>Palabras incorrectas.</summary>
    private string[] palabrasIncorrectas = { "Perro", "Mano", "Silla", "Reloj", "Zapato" };
    /// <summary>
    /// Muestra el panel de pregunta al jugador, detiene el tiempo y genera una nueva pregunta.
    /// </summary>
    /// <param name="enemigo">Enemigo que activó la pregunta.</param>
    public void MostrarPregunta(GameObject enemigo)
    {
        enemigoActual = enemigo;
        panelPregunta.SetActive(true);
        Time.timeScale = 0f;
        puedeQuitarVida = true;
        GenerarPregunta();
        retroalimentacionTexto.text = "";
    }
     
    /// <summary>
    /// Genera aleatoriamente una pregunta con una palabra correcta y dos incorrectas.
    /// Asigna los listeners a los botones de opción.
    /// </summary>

    void GenerarPregunta()
    {
        // Crear opciones mezcladas como antes (ejemplo simple aqu�)
        string correcta = palabrasCorrectas[Random.Range(0, palabrasCorrectas.Length)];
        string[] opciones = new string[3];
        opciones[0] = correcta;
        opciones[1] = palabrasIncorrectas[Random.Range(0, palabrasIncorrectas.Length)];
        opciones[2] = palabrasIncorrectas[Random.Range(0, palabrasIncorrectas.Length)];

        // Mezclar opciones
        for (int i = 0; i < opciones.Length; i++)
        {
            string temp = opciones[i];
            int rnd = Random.Range(i, opciones.Length);
            opciones[i] = opciones[rnd];
            opciones[rnd] = temp;
        }

        preguntaTexto.text = "�Cu�l palabra empieza con la letra C?";

        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            string opcion = opciones[i];
            botonesOpciones[i].GetComponentInChildren<Text>().text = opcion;
            botonesOpciones[i].onClick.RemoveAllListeners();

            if (opcion == correcta)
                botonesOpciones[i].onClick.AddListener(RespuestaCorrecta);
            else
                botonesOpciones[i].onClick.AddListener(RespuestaIncorrecta);
        }
    }

    /// <summary>
    /// Acción al seleccionar una respuesta correcta.
    /// Cierra el panel, destruye al enemigo y reanuda el tiempo.
    /// </summary>
    void RespuestaCorrecta()
    {
        retroalimentacionTexto.text = "�Correcto!";
        Time.timeScale = 1f;
        panelPregunta.SetActive(false);
        Destroy(enemigoActual);
    }
    /// <summary>
    /// Acción al seleccionar una respuesta incorrecta.
    /// Muestra retroalimentación y quita una vida si es posible.
    /// </summary>
    void RespuestaIncorrecta()
    {
        retroalimentacionTexto.text = "Intenta de nuevo...";
        if (puedeQuitarVida)
        {
            if (jugadorVida != null)
            {
                jugadorVida.TakeDamage(1);
            }
            puedeQuitarVida = false; // No quitar m�s vida hasta que cambie la pregunta
        }
    }
}
