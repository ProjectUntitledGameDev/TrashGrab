using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed = 5;
    public bool interactible;
    public Interactive currentInteraction;
    public AudioSource gimme;
    public UIController screenController;
    private string forward;
    private string backward;
    private string left;
    private string right;
    private string interact;
    private Vector2 movement;
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
        /*
        if (Input.GetButton(forward))
        {
            movement.x = Input.GetAxisRaw(forward);
        }
        if (Input.GetButton(backward))
        {
            movement.x = -Input.GetAxisRaw(backward);
        }
        if (Input.GetButton(left))
        {
            movement.y = Input.GetAxisRaw(left);
        }
        if (Input.GetButton(right))
        {
            movement.y = -Input.GetAxisRaw(right);
        }
        */
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButton(interact) && currentInteraction != null)
        {
            if (currentInteraction.entrance)
            {
                fadeInOrOut = true;
                StartCoroutine("FadeToBlack");
                
            }
            else
            {

            }
        }
    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    private float fadeSpeed = 5;
    private float fadeAmount;
    private bool fadeInOrOut;
    public GameObject blackScreen;
    IEnumerator FadeToBlack()
    {
        blackScreen.SetActive(true);
        Color screenColour = blackScreen.GetComponent<Image>().color;
        if (fadeInOrOut)
        {
            while (screenColour.a < 1)
            {
                fadeAmount = screenColour.a + (fadeSpeed * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeAmount);
                blackScreen.GetComponent<Image>().color = screenColour;
                yield return null;
            }
            fadeInOrOut = false;
            this.transform.position = currentInteraction.entryPoint.position;
            StartCoroutine("FadeToBlack");
        }
        else
        {
            while (screenColour.a > 0)
            {
                fadeAmount = screenColour.a - (fadeSpeed * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeAmount);
                blackScreen.GetComponent<Image>().color = screenColour;
                yield return null;
            }
            blackScreen.SetActive(false);
        }
    }
}
