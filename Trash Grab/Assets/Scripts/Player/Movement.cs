using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed = 5;
    public bool interactible;
    public Interactive currentInteraction;
    public AudioSource gimme;
    public UIController screenController;
    public GameObject trashLeft;
    private string interact;
    private string tech1;
    private string tech2;
    private Vector2 movement;
    public Animator anim;
    private int trash;
    public TextMeshProUGUI text;
    private bool used1;
    private bool used2;
    // Update is called once per frame
    private void Awake()
    {
        
        if(PlayerPrefs.GetString("Interact") == "")
        {
            interact = "e";
        }
        else
        {
            interact = PlayerPrefs.GetString("Interact");
        }
        if(PlayerPrefs.GetString("Tech1") == "")
        {
            tech1 = "q";
            tech2 = "f";
            PlayerPrefs.SetString("Tech1", tech1);
            PlayerPrefs.SetString("Tech2", tech2);
        }
        else
        {
            tech1 = PlayerPrefs.GetString("Tech1");
            tech2 = PlayerPrefs.GetString("Tech2");
        }
       
    }
    private void Update()
    {
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
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            anim.gameObject.transform.localRotation = new Quaternion(0, Input.GetAxisRaw("Horizontal") * 180 - 180, 0, 0);
            anim.SetBool("Horizontal", true);
        }
        else
        {
            anim.SetBool("Horizontal", false);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
           
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
        }
        else if(Input.GetAxisRaw("Vertical") < 0)
        {
            anim.SetBool("Down", true);
            anim.SetBool("Up", false);
        }
        if(Input.GetAxisRaw("Vertical") == 0)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
        if (Input.GetButton(interact) && currentInteraction != null)
        {
            if (currentInteraction.exit)
            {
                if(trash < 3)
                {
                    trashLeft.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene(2);
                }
            }
            else
            {
                if (currentInteraction.entrance)
                {
                    fadeInOrOut = true;

                    StartCoroutine("FadeToBlack");

                }
                else
                {
                    if (!currentInteraction.used)
                    {
                        currentInteraction.used = true;
                        currentInteraction.gameObject.GetComponent<SpriteRenderer>().sprite = currentInteraction.empty;
                        currentInteraction = null;
                        trash++;
                        text.text = "There is " + (3 - trash) + " Trash Left";
                    }
                    
                    
                }
            }
            
        }
        if (Input.GetButton(tech1) && !used1)
        {
            used1 = true;
            this.gameObject.GetComponent<TrashTech>().Invoke(PlayerPrefs.GetString("TT1"), 0f);
        }
        else if (Input.GetButton(tech2) && !used2)
        {
            used2 = true;
            this.gameObject.GetComponent<TrashTech>().Invoke(PlayerPrefs.GetString("TT2"), 0f);
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
            Vector3 temp = currentInteraction.entryPoint.position;
            while (screenColour.a < 1)
            {
                fadeAmount = screenColour.a + (fadeSpeed * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeAmount);
                blackScreen.GetComponent<Image>().color = screenColour;
                yield return null;
            }
            fadeInOrOut = false;
            this.transform.position = temp;
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




