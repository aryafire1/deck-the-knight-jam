using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 3;
    public int score = 0;
    public KeyCode[] keys = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
    public Transform[] positions = new Transform[4];
    float speed = 5.0f;
    public KeyCode[] keys2 = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
    public float dashCooldown = 1.0f;
    public float dashTimer = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(0, 0, 0);
        /*if (Input.GetKey(keys[0]))
        {
            Vector3 temp = positions[0].position;
            temp.x = transform.position.x;
            transform.position = temp;

        }
        if (Input.GetKey(keys[1]))
        {
            Vector3 temp = positions[1].position;
            temp.x = transform.position.x;
            transform.position = temp;
        }
        if (Input.GetKey(keys[2]))
        {
            Vector3 temp = positions[2].position;
            temp.y = transform.position.y;
            transform.position = temp;
        }
        if (Input.GetKey(keys[3]))
        {
            Vector3 temp = positions[3].position;
            temp.y = transform.position.y;
            transform.position = temp;
        }*/
        bool jump = false;
        if (Input.GetKey(keys2[0]))
        {
            jump = true;
            
        }
        if (Input.GetKey(keys2[1]))
        {
            move.y = -1;

        }
        if (Input.GetKey(keys2[2]))
        {
            move.x = -1;
            if (Input.GetKey(KeyCode.Space)&& dashTimer >= dashCooldown)
            {
                Vector3 temp = positions[2].position;
                transform.parent.position = temp;
                dashTimer = 0.0f;
            }
        }
        if (Input.GetKey(keys2[3]))
        {
            move.x = 1;

            if (Input.GetKey(KeyCode.Space)&& dashTimer >= dashCooldown)
            {
                Vector3 temp = positions[3].position;
                transform.parent.position = temp;
                dashTimer = 0.0f;
            }
        }
        dashTimer += Time.deltaTime;
        dash(move, jump);
        transform.parent.position += move * speed * Time.deltaTime;
    }
    public void dash(Vector3 move, bool jump){
        if(dashTimer >= dashCooldown){
            if (jump){
            Vector3 temp = positions[0].position;
            if (move.x == -1){
                temp.x = positions[2].position.x;
            }
            else if (move.x == 1){
                temp.x = positions[3].position.x;
            }
            transform.parent.position = temp;
            }
            else if (move.x == -1){
                Vector3 temp = positions[2].position;
                temp.x = positions[2].position.x;
                transform.parent.position = temp;
            }
            else if (move.x == 1){
                Vector3 temp = positions[3].position;
                temp.x = positions[3].position.x;
                transform.parent.position = temp;

            }
            dashTimer = 0.0f;

        }
        

    }
}
