using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalData : MonoBehaviour
{
    public GameObject panel;
    public GameObject quitGamePanel;
    public UIController[] keyBindings;
    public bool gameOver;
    public UIController blackScreen;
    public GameObject[] enemies;
    public AudioSource audioManager;
    public float volume;
    public Slider slider;
    public GameObject pause;
    [Header("Character Select")]
    public TextMeshProUGUI[] trashUIName;
    public TextMeshProUGUI[] trashUIDesc;
    public Image[] trashUIImage;
    public Image menuImage;
    public Sprite[] raccoons;
    public GameObject[] characters;
    
    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
    private void Awake()
    {
        if(PlayerPrefs.GetInt("First") != 0)
        {
            volume = PlayerPrefs.GetFloat("Volume");
            slider.value = volume;
            audioManager.volume = volume;
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 0.5f);
            audioManager.volume = volume;
            PlayerPrefs.SetInt("First", 1);
        }
        if(menuImage != null)
        {
            menuImage.sprite = raccoons[PlayerPrefs.GetInt("Raccoon")];
            characters[PlayerPrefs.GetInt("Raccoon")].GetComponent<UIController>().GetGlobal();
            characters[PlayerPrefs.GetInt("Raccoon")].GetComponent<UIController>().PickItem();
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            for (int i = 0; i < keyBindings.Length; i++)
            {
                if(keyBindings[i] != null)
                {
                    if (PlayerPrefs.GetString(keyBindings[i].input) == "")
                    {
                        PlayerPrefs.SetString(keyBindings[i].input, keyBindings[i].defaultInput);
                    }
                }
                
            }
        }
    }

    public void UpdateVolume(Slider slider)
    {
        
        volume = slider.value;
        volume = (Mathf.Round(volume * 1000)) / 1000;
        PlayerPrefs.SetFloat("Volume", volume);
        audioManager.volume = volume;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(panel != null)
            {
                if (panel.activeInHierarchy)
                {
                    panel.SetActive(false);
                }
                else
                {
                    panel.SetActive(true);
                }
                
            }
            else
            {
                quitGamePanel.SetActive(true);
                panel = quitGamePanel;
            }

        }
    }
}
