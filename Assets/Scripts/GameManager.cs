using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    public int enemys = 0;
    public int lifes=3;

    public float tiempo=240f;

    public bool muertoAntes;
    public int puntuacion;
    public bool mostrado;
    
    public HUDManager HUD;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        //Hacemos que no se destruya el GameManager al cargar otras escenas y ademas si en otra escena ya hay un
        //GameManager el más nuevo se destruye
        if (Instance !=null)
        {
            Debug.Log("Encontrado mas de un GameManager de persistencia de datos, destruyendo el mas nuevo");
            Destroy(this.gameObject);
            return;
        }
        Instance=this;

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

        //Condiciones de Acabar Partida
        if(SceneManager.GetActiveScene().name=="Level01" && tiempo<=0)
        {
            cambiarEscena("MenuFinal");
            
        }else if(SceneManager.GetActiveScene().name=="Level01" && lifes==0)
        {
            muertoAntes=true;
            cambiarEscena("MenuFinal");
            
        }
        else if(SceneManager.GetActiveScene().name=="Level01" && enemys==27)
        {
            muertoAntes=false;
            cambiarEscena("MenuFinal");
            
        }
        //Mostramos la puntuacion
        if(SceneManager.GetActiveScene().name=="MenuFinal" && !mostrado)
        {
            mostrado=true;
            EndGame();
        }
        //Si sale del menu final al menu inicial restablecemos las variables
        if(SceneManager.GetActiveScene().name=="MenuFinal" && Input.GetKey(KeyCode.A))
        {
            mostrado=false;
            enemys = 0;
            lifes=3;
            tiempo=240f;
            cambiarEscena("Menu");
        }


    }

    //Metodo para cambiar de escena
     public void cambiarEscena(string nombreEscena)
    {
        //Cambiamos la escena
        SceneManager.LoadScene(nombreEscena, LoadSceneMode.Single);
    }

   

    public void AddEnemy()
    {
        enemys++;
        HUD.MostrarPuntos(enemys);
    }

    //Metodo para perder vidas
    public void LoseLife()
    {
        lifes--;
        HUD.MostrarVidas(lifes);
    }

    //Metodo para hacer la suma de la puntuacion
    public void EndGame()
    {
        //Si ha matado a todos los enemigos la puntuacion será los enemigos matados y los segundos restantes
        if(!muertoAntes)
        {
            puntuacion= (int)(tiempo + enemys);
            HUD.MostrarPuntuacion(puntuacion);
            //Si no solo se suma a la puntuacion los enemigos matados
        }else
        {
            puntuacion=enemys;
            HUD.MostrarPuntuacion(puntuacion);
        }
    }

    //Metodo para la cuenta atras
    public void CuentaAtras()
    {
        tiempo-=Time.deltaTime;
        HUD.MostrarTiempo(tiempo);
    }

    public void Marcador(TMP_Text textoMarcador)
    {
        //Si existe una clave en el PlayerPrefs de marcador obtenemos su valor y se la pasamos al texto para mostrarla
        if(PlayerPrefs.HasKey("Marcador"))
        {
            int puntuacion=PlayerPrefs.GetInt("Marcador");
            textoMarcador.text="Record De Puntuación: " + puntuacion.ToString();
        //Si no existe creamos una clave y mostramos por pantalla un marcador de 0
        }else
        {
            textoMarcador.text="Marcador: 0";
            PlayerPrefs.SetInt("Marcador", 0);
        }
    }

    //Metodo para comparar las puntuaciones
    public void CompareMarcador()
    {
        //Si existe en el PlayerPrefs una clave de marcador y la puntuacion que acaba de obtener el jugador es mayor a la que
        //Existe en el PlayerPrefs cambiamos el valor del PlayerPrefs por esta nueva puntuacion
        if(PlayerPrefs.HasKey("Marcador") && puntuacion>PlayerPrefs.GetInt("Marcador"))
        {
            PlayerPrefs.SetInt("Marcador", puntuacion);
        }
    }

    public void SetMarcador(TMP_Text textoMarcador)
    {
        CompareMarcador();
        Marcador(textoMarcador);
        
    }


}
