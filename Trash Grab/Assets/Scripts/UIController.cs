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
    public GameObject blackScreen;
    private float fadeSpeed = 5;
    private float fadeAmount;
    public bool fade;

    [Header("Rebind Keys")]
    public bool rebind;
    public bool setBinding;
    public TextMeshProUGUI textBox;
    public string input;
    public string defaultInput;
    private void Awake()
    {
        if (fade)
            StartCoroutine(FadeToBlack(0, false, false));
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
        StartCoroutine(FadeToBlack(scene, true, true));
    }
    public void StartFade()
    {
        StartCoroutine(FadeToBlack(0, true, false));
    }
    public IEnumerator FadeToBlack(int scene, bool fadeInOrOut, bool changeScene)
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
