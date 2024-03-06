using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HUDManager : MonoBehaviour
{
    [SerializeField] public TMPro.TMP_Text puntos;
    [SerializeField] private TMPro.TMP_Text vidas;
    [SerializeField] private TMPro.TMP_Text time;

    [SerializeField] private TMPro.TMP_Text puntuacion;
    [SerializeField] GameObject panelControl;
    [SerializeField] GameObject panelJuego;
    [SerializeField] TMPro.TMP_Text textoMarcador;

    
    private void Start()
    {
        //Mostramos el marcador record en el menu de inicio
        if(SceneManager.GetActiveScene().name=="Menu")
        {
            GameManager.Instance.SetMarcador(textoMarcador);
        }
        
        GameManager.Instance.HUD = this;
        
    }

    //Cambiamos a la escena de juego
       public void Jugar()
    {
        GameManager.Instance.cambiarEscena("Level01");
    }

    //Salimos del juego
    public void Salir()
    {
        Application.Quit();
    }

    //Mostramos la info del juego
    public void Controles()
    {
        panelControl.SetActive(true);
        panelJuego.SetActive(false);
    }

    //Ocultamos la info del juego
    public void SalirControl()
    {
        panelControl.SetActive(false);
        panelJuego.SetActive(true);
    }

    //Mostramos los enemigos matados
    public void MostrarPuntos(int enemys)
    {
        puntos.text = enemys.ToString();
    }

    //Mostramos las vidas restantes
        public void MostrarVidas(int lifes)
    {
        vidas.text = lifes.ToString();
    }

    //Mostramos el tiempo restante
    public void MostrarTiempo(float tiempo)
    {
        time.text=$"Tiempo: {Mathf.Floor(tiempo)}";
    }

    //Mostramos la puntuacion al final de la partida
    public void MostrarPuntuacion(int puntosFinal)
    {
        
        puntuacion.text="Puntuacion: " + puntosFinal.ToString();
    }
}
