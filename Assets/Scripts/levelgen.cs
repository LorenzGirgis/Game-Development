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

    private void Awake()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newbrick = Instantiate(brickfab, transform);
                newbrick.transform.position = transform.position + new Vector3((float)((size.x - 0.4) * 0.2f - i) * offset.x, j * offset.y, 0);
                newbrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)j / (size.y - 1));
            }
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}