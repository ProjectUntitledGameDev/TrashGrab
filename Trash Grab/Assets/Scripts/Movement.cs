using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public CharacterController controller;
    public int speed = 5;
    public bool interactible;
    public AudioSource gimme;
    private string forward;
    private string backward;
    private string left;
    private string right;
    private string interact;
    // Update is called once per frame
    private void Awake()
    {
        forward = PlayerPrefs.GetString("Forward");
        backward = PlayerPrefs.GetString("Backward");
        left = PlayerPrefs.GetString("Left");
        right = PlayerPrefs.GetString("Right");
        interact = PlayerPrefs.GetString("Interact");
        
    }
    private void Update()
    {
        float zMov = 0;
        float xMov = 0;
        
        if (Input.GetButton(forward))
        {
            xMov = 1;
        }
        if (Input.GetButton(backward))
        {
            xMov = -1;
        }
        if (Input.GetButton(left))
        {
            zMov = 1;
        }
        if (Input.GetButton(right))
        {
            zMov = -1;
        }
        
        UnityEngine.Vector3 move = transform.up * xMov - transform.right * zMov;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButton(interact))
        {
            gimme.Play();
        }
    }
}
