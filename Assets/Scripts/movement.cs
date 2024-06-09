using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    public float move;
    public float maxX = 7.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        if ((move>0 && transform.position.x < maxX) || (move<0 && transform.position.x > -maxX)) { 
        transform.position += Vector3.right * move * speed * Time.deltaTime;}

        if(Input.GetKeyDown(KeyCode.H)) 
        {
            speed = 500;
        }
    }
}
