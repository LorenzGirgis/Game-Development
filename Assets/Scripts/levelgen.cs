using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelgen : MonoBehaviour
{
    public Vector2Int size;
    public Vector2 offset;
    public GameObject brickfab;
    public Gradient gradient;
    // Start is called before the first frame update

       // Called when the script instance is being loaded
    private void Awake()
    {
        // Generate the grid of bricks
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                // Instantiate a new brick
                GameObject newbrick = Instantiate(brickfab, transform);
                // Set the position of the new brick
                newbrick.transform.position = transform.position + new Vector3((float)((size.x - 0.4) * 0.2f - i) * offset.x, j * offset.y, 0);
                // Set the color of the new brick based on the gradient
                newbrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)j / (size.y - 1));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can go here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Code to update each frame can go here if needed
    }

    // Method to restart the game
    public void Restart()
    {
        // Set the time scale back to normal speed
        Time.timeScale = 1;
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}