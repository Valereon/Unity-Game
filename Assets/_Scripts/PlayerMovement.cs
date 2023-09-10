using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] ParticleSystem[] thrustersExhaust;

    Rigidbody2D playerRb;
    Vector2 direction;
    float speed = 5f;
    float dampener = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Thrusters();
    }


    void FixedUpdate() {
        playerRb.AddForce(direction * speed, ForceMode2D.Force);
    }


    void Thrusters(){
        if(Input.GetKey(KeyCode.W)){
            thrustersExhaust[0].Play();
        }
        if(Input.GetKey(KeyCode.S)){
            thrustersExhaust[1].Play();
        }
        if(Input.GetKey(KeyCode.A)){
            thrustersExhaust[2].Play();
        }
        if(Input.GetKey(KeyCode.D)){
            thrustersExhaust[3].Play();
        }
    }
}
