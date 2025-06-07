using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// ControladorPreguntaManual gestiona un panel de preguntas de selección de palabras correctas.
/// Se usa para pausar el juego, mostrar preguntas, evaluar respuestas, contabilizar aciertos/errores,
/// y reiniciar la escena o destruir un enemigo según los resultados.
/// </summary>

public class ControladorPreguntaManual : MonoBehaviour
{
   
 // Panel que contiene las preguntas y botones de respuesta.
    public GameObject panelPregunta;
    
 // Texto de retroalimentación para mostrar aciertos y errores.
    public TMP_Text retroalimentacionTexto;
 
   // Botones de respuesta en el panel.
    public Button[] botones;

 // Referencia al enemigo con el que se activa este panel.
    public GameObject enemigoActual;

// Contadores de aciertos y errores.
    private int aciertos = 0;
    private int errores = 0;

    private const int maxAciertos = 3;
    private const int maxErrores = 3;

    // Listas de palabras correctas e incorrectas para el ejercicio.
    private List<string> palabrasCorrectas = new List<string> { "Casa", "Cielo", "Cama", "Correr", "Comida", "Calle", "Cola", "Copa" };
    private List<string> palabrasIncorrectas = new List<string> { "Kasa", "Sielo", "Kama", "Korrer", "Komida", "Caye", "Kola", "Kopa" };

    /// <summary>
    /// Inicializa el panel desactivado y limpia los eventos de los botones.
    /// </summary>
    /// 
    private void Start()
    {
        panelPregunta.SetActive(false);
        foreach (Button boton in botones)
        {
            boton.onClick.RemoveAllListeners();
        }
    }

    /// <summary>
    /// Muestra el panel de preguntas y pausa el juego.
    /// Recibe una referencia al enemigo que se está enfrentando.
    /// </summary>
    
    public void MostrarPanel(GameObject enemigo)
    {
        enemigoActual = enemigo;
        aciertos = 0;
        errores = 0;
        retroalimentacionTexto.text = "";
        panelPregunta.SetActive(true);
        Time.timeScale = 0f;

        GenerarPalabrasAleatorias();
    }
    /// <summary>
    /// Genera una combinación aleatoria de palabras correctas e incorrectas para mostrar en los botones.
    /// </summary>
    void GenerarPalabrasAleatorias()
    {
        // Mezclamos y seleccionamos 4 correctas y 4 incorrectas
        List<string> correctas = ObtenerAleatorias(palabrasCorrectas, 4);
        List<string> incorrectas = ObtenerAleatorias(palabrasIncorrectas, 4);
        List<string> combinadas = new List<string>();
        combinadas.AddRange(correctas);
        combinadas.AddRange(incorrectas);
        Shuffle(combinadas);

        for (int i = 0; i < botones.Length && i < combinadas.Count; i++)
        {
            string palabra = combinadas[i];
            TMP_Text texto = botones[i].GetComponentInChildren<TMP_Text>();
            texto.text = palabra;

            botones[i].onClick.RemoveAllListeners();

            if (palabrasCorrectas.Contains(palabra))
            {
                botones[i].onClick.AddListener(() => RespuestaCorrecta());
            }
            else
            {
                botones[i].onClick.AddListener(() => RespuestaIncorrecta());
            }
        }
    }
    /// <summary>
    /// Devuelve una lista con una cantidad aleatoria de palabras de la lista dada.
    /// </summary>

    List<string> ObtenerAleatorias(List<string> lista, int cantidad)
    {
        List<string> copia = new List<string>(lista);
        List<string> resultado = new List<string>();

        for (int i = 0; i < cantidad && copia.Count > 0; i++)
        {
            int index = Random.Range(0, copia.Count);
            resultado.Add(copia[index]);
            copia.RemoveAt(index);
        }

        return resultado;
    }
     /// <summary>
    /// Mezcla aleatoriamente los elementos de una lista.
    /// </summary>

    void Shuffle(List<string> lista)
    {
        for (int i = lista.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (lista[i], lista[j]) = (lista[j], lista[i]);
        }
    }

    /// <summary>
    /// Se ejecuta al presionar una palabra correcta.
    /// Incrementa los aciertos, actualiza la retroalimentación y desactiva el botón.
    /// Si se alcanza el máximo de aciertos, destruye al enemigo y cierra el panel.
    /// </summary>
    
    public void RespuestaCorrecta()
    {
        aciertos++;
        retroalimentacionTexto.text = "�Correcto! Aciertos: " + aciertos;

        // Proteger contra errores al acceder al bot�n actual
        GameObject botonPresionado = UnityEngine.EventSystems.EventSystem.current?.currentSelectedGameObject;

        if (botonPresionado != null)
        {
            Button boton = botonPresionado.GetComponent<Button>();
            if (boton != null)
            {
                boton.interactable = false;
            }
        }

        if (aciertos >= maxAciertos)
        {
            CerrarPanelYDestruir();
        }
    }

    /// <summary>
    /// Vuelve a pausar el juego si el panel sigue activo (no se usa actualmente).
    /// </summary>
    void Repausar()
    {
        if (panelPregunta.activeSelf)
            Time.timeScale = 0f;
    }

  /// <summary>
    /// Se ejecuta al presionar una palabra incorrecta.
    /// Incrementa los errores, actualiza la retroalimentación y desactiva el botón.
    /// Si se alcanzan los errores máximos, reinicia la escena.
    /// </summary>
    
    public void RespuestaIncorrecta()
    {
        errores++;
        retroalimentacionTexto.text = "Incorrecto. Errores: " + errores;

        GameObject botonPresionado = UnityEngine.EventSystems.EventSystem.current?.currentSelectedGameObject;

        if (botonPresionado != null)
        {
            Button boton = botonPresionado.GetComponent<Button>();
            if (boton != null)
            {
                boton.interactable = false;
            }
        }

        if (errores >= maxErrores)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    /// <summary>
    /// Cierra el panel de preguntas, destruye al enemigo y reanuda el juego.
    /// </summary>


    private void CerrarPanelYDestruir()
    {
        Time.timeScale = 1f;
        panelPregunta.SetActive(false);
        if (enemigoActual != null)
        {
            Destroy(enemigoActual);
        }
    }
}


