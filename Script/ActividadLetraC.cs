using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla una actividad interactiva de preguntas y respuestas relacionadas con la letra C.
   /// Detiene el tiempo cuando se activa, muestra preguntas, valida respuestas,
  /// permite un número máximo de intentos y reinicia el juego o destruye al enemigo según el resultado.
 /// </summary>

public class ActividadLetraC : MonoBehaviour
{
     // Referencias a los elementos de UI
    public GameObject panelPregunta;
    public TMP_InputField campoRespuesta;
    public TextMeshProUGUI textoRetroalimentacion;
    public TextMeshProUGUI textoIntentos;
    public TextMeshProUGUI textoPregunta;

// Listas para almacenar las preguntas y sus respuestas correctas
    private List<string> preguntas = new List<string>();
    private List<string> respuestas = new List<string>();

    // Variables de control
    private int indiceActual = 0;
    private int intentos = 0;
    private int maxIntentos = 3;

    private GameObject enemigoActual;
    private bool actividadEnCurso = false;

    /// <summary>
    /// Inicia la actividad cuando el jugador toca un enemigo.
    /// </summary>

    public void IniciarActividad(GameObject enemigo, List<string> nuevasPreguntas, List<string> nuevasRespuestas)
    {
         // No permite iniciar otra actividad si ya hay una en curso
        if (actividadEnCurso) return;

        // Verifica que las listas de preguntas y respuestas coincidan en cantidad
        if (nuevasPreguntas.Count != nuevasRespuestas.Count)
        {
            Debug.LogError("Las preguntas y respuestas no coinciden en cantidad.");
            return;
        }

        enemigoActual = enemigo;
        preguntas = nuevasPreguntas;
        respuestas = nuevasRespuestas;
        indiceActual = 0;
        intentos = 0;
        actividadEnCurso = true;

        Time.timeScale = 0f;
        panelPregunta.SetActive(true);
        MostrarPreguntaActual();
    }

    /// <summary>
    /// Muestra la pregunta actual en pantalla.
    /// </summary>
    
    private void MostrarPreguntaActual()
    {
        if (indiceActual >= preguntas.Count)
        {
            Debug.LogWarning("Ya se completaron todas las preguntas.");
            return;
        }

        textoPregunta.text = preguntas[indiceActual];
        campoRespuesta.text = "";
        textoRetroalimentacion.text = "";
        textoIntentos.text = $"Intentos: {intentos}/{maxIntentos}";
    }

     /// <summary>
    /// Evalúa la respuesta ingresada por el jugador.
    /// </summary>

    public void EvaluarRespuesta()
    {
        if (indiceActual >= respuestas.Count) return;

        string respuestaUsuario = campoRespuesta.text.Trim().ToLower();
        string respuestaCorrecta = respuestas[indiceActual].Trim().ToLower();

        if (respuestaUsuario == respuestaCorrecta)
        {
            textoRetroalimentacion.text = "Correcto";
            indiceActual++;
            intentos = 0;
          // Si ya respondió todas, finaliza la actividad
            if (indiceActual >= preguntas.Count)
            {
                StartCoroutine(FinalizarActividad(1.5f));
            }
            else
            {
                StartCoroutine(SiguientePregunta());
            }
        }
        else
        {
            intentos++;
            textoRetroalimentacion.text = "Incorrecto";
            textoIntentos.text = $"Intentos: {intentos}/{maxIntentos}";

            if (intentos >= maxIntentos)
            {
                textoRetroalimentacion.text = "Fin del juego";
                StartCoroutine(ReiniciarJuego(1.5f));
            }
        }
    }
    
    /// <summary>
    /// Espera y luego muestra la siguiente pregunta.
    /// </summary>
    

    IEnumerator SiguientePregunta()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        MostrarPreguntaActual();
    }

    /// <summary>
    /// Finaliza la actividad: destruye al enemigo, oculta el panel y reanuda el juego.
    /// </summary>
    
    IEnumerator FinalizarActividad(float segundos)
    {
        yield return new WaitForSecondsRealtime(segundos);
        Time.timeScale = 1f;
        panelPregunta.SetActive(false);
        actividadEnCurso = false;

        if (enemigoActual != null)
        {
            Destroy(enemigoActual);
            enemigoActual = null;
        }
    }

    /// <summary>
    /// Reinicia la escena actual después de un tiempo.
    /// </summary>

    IEnumerator ReiniciarJuego(float segundos)
    {
        yield return new WaitForSecondsRealtime(segundos);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


