using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float speed = 1f;

    public float maxLeft;
    public float maxRight;

    public GameObject shootPrefab;
    public SpriteRenderer laserSpriteRenderer;

    private bool _canFire = true;

    public GameObject enemyPrefab;

    private Animator _animator;
    [SerializeField] Sprite[] spriteArray;
    private SpriteRenderer changeLifes;
    private AudioSource audioSource;


     void Start()
    {
        //Obtenemos el Componente Animator
        _animator = gameObject.GetComponent<Animator>();

        //Obtenemos el Componente SpriteRenderer del GameObject hijo del Player
        changeLifes = GameObject.FindWithTag("Vidas").GetComponent<SpriteRenderer>();
        //Obtenemos el audioSource
        audioSource=gameObject.GetComponent<AudioSource>();
    }

  
    void Update()
    {

        //Empezamos la cuenta atras
        GameManager.Instance.CuentaAtras();
        
        if (Input.GetKey(KeyCode.A))
        {
            //transform.position = new Vector3( transform.position.x - (speed * Time.deltaTime), transform.position.y);
            transform.position = new Vector3(Mathf.Max(maxLeft, transform.position.x - (speed * Time.deltaTime)), transform.position.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y);
            transform.position = new Vector3(Mathf.Min(maxRight, transform.position.x + (speed * Time.deltaTime)), transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canFire)
            {
                _canFire = false;
                GameObject go = Instantiate(shootPrefab);
                //go.transform.position = this.transform.position;
                go.transform.position = laserSpriteRenderer.gameObject.transform.position;
                go.GetComponent<ControlShoot>().controlPlayer = this;
                laserSpriteRenderer.enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Si colisiona con el enemigo pierde una vida
        if(col.tag=="Enemy")
        {
                GameManager.Instance.LoseLife();
                if(GameManager.Instance.lifes-1!=-1){
                    
                changeLifes.sprite=spriteArray[GameManager.Instance.lifes-1];           //Cambiamos el numero de vidas
            }
                //Reproducimos la animacion de explosion y el sonido de explosion
                _animator.Play("Explotion");
                audioSource.Play();
        }
    }

    public void CanFire()
    {
        _canFire = true;
        laserSpriteRenderer.enabled = true;
    }
}
