using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bouncyball : MonoBehaviour
{
public float minY = -5.5f; // Minimum Y position where the ball is considered out of bounds
Rigidbody2D rb; // Reference to the Rigidbody2D component of the ball
public float maxspeed = 15f; // Maximum speed the ball can travel
int score = 0; // Current player score
int lives = 2; // Remaining lives of the player
public TextMeshProUGUI scoretext; // TextMeshProUGUI object to display the score
public GameObject[] livesimage; // Array of GameObjects representing player lives
public GameObject gameover; // GameObject representing the game over screen
public GameObject W; // GameObject representing the win screen
public GameObject death_screen; // GameObject representing the death screen
public GameObject pause_screen; // GameObject representing the pause screen
public float bouncespeed = 10f; // Speed at which the ball bounces
public float startpotionX; // Initial X position of the ball
public float startpotionY; // Initial Y position of the ball
public float startpotionZ; // Initial Z position of the ball
int brickcount; // Number of bricks in the level
public float pitchadd; // Amount by which pitch is added to the block break sound
public AudioSource Blockbreacksound; // AudioSource for block break sound
public AudioSource death; // AudioSource for death sound
public AudioSource Winscreen; // AudioSource for win screen sound
public AudioSource Gameoverscreen; // AudioSource for game over screen sound
public SpriteRenderer ballsprite; // SpriteRenderer component of the ball
private bool isPaused = false; // Flag to indicate if the game is paused
public PowerUpManager powerUpManager; // Reference to the PowerUpManager script

 void Start()
    {
        // Set the ball to the starting position
        transform.position = new Vector3(startpotionX, startpotionY, startpotionZ);
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Count the number of bricks in the level
        brickcount = FindObjectOfType<levelgen>().transform.childCount;
        // Give the ball an initial downward velocity
        rb.velocity = Vector2.down * bouncespeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the ball is below the minimum y position
        if (transform.position.y < minY)
        {
            if (lives <= 0)
            {
                // If no lives are left, trigger game over
                Gameover();
            }
            else
            {
                // If lives are left, lose a life and reset the ball
                death.Play();
                death_screen.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    death_screen.SetActive(false);
                    transform.position = new Vector3(startpotionX, startpotionY, startpotionZ);
                    lives--;
                    livesimage[lives].SetActive(false);
                    rb.velocity = Vector2.down * bouncespeed;
                }
            }
        }

        // Limit the ball's speed to the maximum speed
        if (rb.velocity.magnitude > maxspeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxspeed);
        }

        // Pause or resume the game when the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
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

        // Ensure the vertical speed of the ball is significant
        float minVerticalSpeed = 2f;
        if (Mathf.Abs(rb.velocity.y) < minVerticalSpeed)
        {
            float newVerticalSpeed = Mathf.Sign(rb.velocity.y) * minVerticalSpeed;
            rb.velocity = new Vector2(rb.velocity.x, newVerticalSpeed);
        }
    }

    // Called on collision
   void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("B"))
    {
        Destroy(collision.gameObject);
        score += 101;
        scoretext.text = score.ToString("00000");
        brickcount--;
        ballsprite = GetComponent<SpriteRenderer>();
        ballsprite.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); // doesn't change the color of the ball to a dark color
        Blockbreacksound.pitch = Blockbreacksound.pitch - pitchadd;
        Blockbreacksound.Play();
        if (brickcount <= 0)
        {
            Time.timeScale = 0;
            W.SetActive(true);
            Winscreen.Play();
        }

        // Call CheckBricksForPowerUp method from PowerUpManager script
        powerUpManager.CheckBricksForPowerUp(brickcount);

        // Activate ball size increase power-up at 10 bricks
        if (brickcount % 10 == 0 && brickcount != 0)
        {
            powerUpManager.ActivateBallSizeIncrease();
        }
    }
}


    // Game over function
    void Gameover()
    {
        // Stop the game and show the game over screen
        Time.timeScale = 0;
        gameover.SetActive(true);
        Destroy(gameObject);
        Gameoverscreen.Play();
    }
}
