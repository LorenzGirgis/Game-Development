using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class bouncyball : MonoBehaviour
{
    public float minY = -5.5f;
    Rigidbody2D rb;
    public float maxspeed = 2000f;
    int score = 0;
    int lives = 2;
    public TextMeshProUGUI scoretext;
    public GameObject[] livesimage;
    public GameObject gameover;
    public GameObject W;
    public GameObject death_screen;
    public GameObject pause_screen;
    public float bouncespeed = 10f;
    public float startpotionX;
    public float startpotionY;
    public float startpotionZ;
    int brickcount;
    public float pitchadd;
    public AudioSource Blockbreacksound;
    public AudioSource death;
    public AudioSource Winscreen;
    public AudioSource Gameoverscreen;
    public SpriteRenderer ballsprite;
    public PowerUpManager powerUpManager;
    private bool isPaused = false;

    void Start()
    {
        transform.position = new Vector3(startpotionX, startpotionY, startpotionZ);
        rb = GetComponent<Rigidbody2D>();
        brickcount= FindObjectOfType<levelgen>().transform.childCount;
        rb.velocity = Vector2.down * bouncespeed;
    }

    
    void Update()
    {
        if(transform.position.y < minY)
        {
            if (lives <= 0)
            {
                Gameover();
            }
            else
            {   
                death.Play();
                death_screen.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space)) 
                {
                    death_screen.SetActive(false);
                    transform.position = new Vector3(startpotionX,startpotionY,startpotionZ);
                    lives--;
                    livesimage[lives].SetActive(false);          
                    rb.velocity = Vector2.down * bouncespeed;
                } 
            }
        }
        
        if (rb.velocity.magnitude > maxspeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxspeed);
        }

        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused) 
            {
                Time.timeScale = 1;
                isPaused = false;
                pause_screen.SetActive(false);

            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
                pause_screen.SetActive(true);
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("B"))
        {
            Destroy(collision.gameObject);
            score += 101;
            scoretext.text = score.ToString("00000");
            brickcount--;
            ballsprite = GetComponent<SpriteRenderer>();
            ballsprite.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); // doesnt change the color of the ball to a dark color
            Blockbreacksound.pitch = Blockbreacksound.pitch - pitchadd;
            Blockbreacksound.Play();
            if( brickcount <= 0)
            {
                Time.timeScale = 0;
                W.SetActive(true);
                Winscreen.Play();

            }
            
            if (++score % 5 == 0)
            {
                powerUpManager.ActivatePowerUp();
            }


        }
    }

    void Gameover()
    {
        Time.timeScale = 0;
        gameover.SetActive(true);
        Destroy(gameObject);
        Gameoverscreen.Play();
    }
}
