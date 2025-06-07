using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

  /// <summary>
 /// Controla una actividad de completar palabra. El jugador debe escribir la palabra correcta
/// en un campo de texto. Tiene un límite de intentos y reinicia la escena si falla 3 veces. 
 /// </summary>

public class ActividadCompletar : MonoBehaviour
{
    // Elementos de la interfaz de usuario
    public GameObject panelActividad;
    public TMP_InputField campoRespuesta;
    public TextMeshProUGUI textoRetroalimentacion;

     // Variables de control
    private GameObject enemigoActual;
    private int intentos = 0;
    public int intentosMaximos = 3;
    public string respuestaCorrecta = "cielo";


        /// <summary>
       /// Al iniciar la escena, se asegura de ocultar el panel de actividad
      /// y reanudar el tiempo en caso de que estuviera pausado.
     /// </summary>
    
    void Start()
    {
        panelActividad.SetActive(false);
        Time.timeScale = 1f; // Por seguridad, reinicia el tiempo si qued� pausado
    }
      /// <summary>
     /// Activa la actividad, congela el tiempo y guarda referencia al enemigo.
    /// </summary>
    public void IniciarActividad(GameObject enemigo)
    {
        enemigoActual = enemigo;
        panelActividad.SetActive(true);
        Time.timeScale = 0f; // Congela el juego
    }

       /// <summary>
      /// Evalúa la respuesta ingresada por el jugador.
     /// </summary>
    
    public void EvaluarRespuesta()
    {
        string respuesta = campoRespuesta.text.Trim().ToLower();

        if (respuesta == respuestaCorrecta.ToLower())
        {
            textoRetroalimentacion.text = "�Correcto!";
            Time.timeScale = 1f;
            Destroy(enemigoActual);
            panelActividad.SetActive(false);
        }
        else
        {
            intentos++;
            textoRetroalimentacion.text = "Incorrecto. Intenta de nuevo.";
           // Si supera el máximo de intentos, reinicia el juego
            if (intentos >= intentosMaximos)
            {
                textoRetroalimentacion.text = "Fin del juego. Has fallado 3 veces.";
                StartCoroutine(ReiniciarJuego());
            }
        }
    }

       /// <summary>
      /// Espera un par de segundos y luego reinicia la escena actual.
     /// </summary>
    IEnumerator ReiniciarJuego()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

