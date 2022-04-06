using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject panel;
    public bool openOrClose;
    private GlobalData globalData;

    [Header("FadeToBlack")]
    public GameObject blackScreen;
    private float fadeSpeed = 5;
    private float fadeAmount;
    public bool fade;


    [Header("Animal Control Dialogue")]
    public GameObject dialogue;
    public TextMeshProUGUI victimDialogue;
    public TextMeshProUGUI acDialogue;
    public string[] victimText;
    public string[] acText;
    private float typingSpeed = 0.05f;
    public Image animalControl;
    public Sprite picture;

    [Header("Rebind Keys")]
    public bool rebind;
    public bool setBinding;
    public TextMeshProUGUI textBox;
    public string input;
    public string defaultInput;
    private void Awake()
    {
        if (fade)
            StartCoroutine(FadeToBlack(0, false, false, false));
        globalData = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<GlobalData>();
        if (rebind)
        {
            if (PlayerPrefs.GetString(input) != "")
            {
                textBox.text = PlayerPrefs.GetString(input).ToUpper();
            }
            else
            {
                PlayerPrefs.SetString(input, defaultInput);
                textBox.text = PlayerPrefs.GetString(input).ToUpper() ;
            }
            
        }

    }

    private void Update()
    {
        if (setBinding)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(kcode))
                    {
                        PlayerPrefs.SetString(input, kcode.ToString().ToLower());
                        textBox.text = PlayerPrefs.GetString(input).ToUpper();
                        PanelInteraction();
                    }
                        
                }
            }
        }
    }

    public void PanelInteraction()
    {
        panel.SetActive(openOrClose);
        if (openOrClose)
            globalData.panel = panel;

        if(panel.GetComponent<UIController>() != null)
            if (panel.GetComponent<UIController>().setBinding)
            {
                panel.GetComponent<UIController>().textBox = textBox;
                panel.GetComponent<UIController>().input = input;
            } 
    }

    public void OpenScene(int scene)
    {
        StartCoroutine(FadeToBlack(scene, true, true, false));
    }
    public void StartFade()
    {
        StartCoroutine(FadeToBlack(0, true, false, true));
    }
    public IEnumerator FadeToBlack(int scene, bool fadeInOrOut, bool changeScene, bool caught)
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
            if(changeScene)
                SceneManager.LoadScene(scene);
            if (caught)
            {
                dialogue.SetActive(true);
                Color dialogueColour = dialogue.GetComponent<Image>().color;
                while (dialogueColour.a < 1)
                {
                    fadeAmount = dialogueColour.a + (fadeSpeed * Time.deltaTime);
                    dialogueColour = new Color(dialogueColour.r, dialogueColour.g, dialogueColour.b, fadeAmount);
                    dialogue.GetComponent<Image>().color = dialogueColour;
                    yield return null;
                }
                StartTyping();
            }
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
    private bool doOnce;
    private void StartTyping()
    {
        if (!doOnce)
        {
            doOnce = true;
            StartCoroutine("DisplayTextVictim");
        }
            
    }
    private IEnumerator DisplayTextVictim()
    {
        victimDialogue.text = "";
        foreach(char letter in victimText[0].ToCharArray())
        {
            victimDialogue.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        if(victimDialogue.text == victimText[0])
        {
            StartCoroutine("DisplayTextAC");
        }
    }
    private IEnumerator DisplayTextAC()
    {
        animalControl.sprite = picture;
        acDialogue.text = "";
        foreach (char letter in acText[0].ToCharArray())
        {
            acDialogue.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        if (acDialogue.text == acText[0])
        {
            
        }
    }
    

    public void ExitGame()
    {
        Application.Quit();
    }
    private void OnDisable()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
            globalData.panel = null;
    }

}
