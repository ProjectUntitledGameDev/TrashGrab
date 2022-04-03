using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public CharacterController controller;
    public int speed = 5;
    // Update is called once per frame
    private void Update()
    {
       

        float xMov = Input.GetAxisRaw("Vertical");
        float zMov = -Input.GetAxisRaw("Horizontal");
        UnityEngine.Vector3 move = transform.up * xMov - transform.right * zMov;

        controller.Move(move * speed * Time.deltaTime);


    }
}
