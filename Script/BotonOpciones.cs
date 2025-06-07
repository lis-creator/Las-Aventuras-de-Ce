using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Script que permite configurar botones con distintas acciones desde el inspector:
/// - Salir del juego
/// - Silenciar/activar la música
/// - Cambiar de escena
/// </summary>

public class BotonOpciones : MonoBehaviour
{
     // Define un listado de acciones posibles para el botón

    public enum TipoDeAccion { Salir, MuteMusica, CambiarEscena}
     // Nombre de la escena que se cargará si la acción es CambiarEscena
    public TipoDeAccion accion;

    [Header("Solo para CambiarEscena")]
    public string nombreEscena;

    // Referencia al botón, asignado desde el Inspector
    public Button boton; 

    // Referencia al AudioSource que contiene la música de fondo
    private AudioSource musica;

     /// <summary>
    /// Se ejecuta al iniciar la escena. Configura el botón y referencia la música si es necesario.
    /// </summary>
    void Start()
    {
        musica = GameObject.FindGameObjectWithTag("Musica")?.GetComponent<AudioSource>();

        if (accion == TipoDeAccion.MuteMusica && musica == null)
        {
            Debug.LogWarning("No se encontr� un objeto con la etiqueta 'Musica'.");
        }
        // Si se asignó un botón desde el Inspector, conecta su evento onClick con el método EjecutarAccion

        if (boton != null)
        {
            boton.onClick.AddListener(EjecutarAccion);
        }
        else
        {
            Debug.LogWarning("No se asign� el bot�n en el inspector.");
        }
    }

       /// <summary>
      /// Método que ejecuta la acción seleccionada según el enum.
      /// </summary>
      /// 
    public void EjecutarAccion()
    {
        switch (accion)
        {
            case TipoDeAccion.Salir:
         // Sale de la aplicación (no visible en el editor, sí en compilados)
                Application.Quit();
                break;

            case TipoDeAccion.MuteMusica:
               // Silencia o activa la música según su estado actua
                if (musica != null)
                {
                    musica.mute = !musica.mute;
                }
                break;

            case TipoDeAccion.CambiarEscena:
             // Si se asignó un nombre de escena válido, carga esa escena
                if (!string.IsNullOrEmpty(nombreEscena))
                {
                    SceneManager.LoadScene(nombreEscena);
                }
                break;
        }
    }
}
