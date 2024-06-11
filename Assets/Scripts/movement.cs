using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    public float move;
    public float maxX = 7.5f; // Maximum X position the paddle can move to


    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can go here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal input from the player
        move = Input.GetAxis("Horizontal");
        // Move the paddle if the input is within the allowed range
        if ((move > 0 && transform.position.x < maxX) || (move < 0 && transform.position.x > -maxX))
        {
            transform.position += Vector3.right * move * speed * Time.deltaTime;
        }

        // Debug feature: increase speed when the 'H' key is pressed
        if (Input.GetKeyDown(KeyCode.H))
        {
            speed = 500;
        }
    }
}
