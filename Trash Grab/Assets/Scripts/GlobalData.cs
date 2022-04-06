using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GlobalData : MonoBehaviour
{
    public GameObject panel;
    public GameObject quitGamePanel;
    public UIController[] keyBindings;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            for (int i = 0; i < keyBindings.Length; i++)
            {
                if (PlayerPrefs.GetString(keyBindings[i].input) == "")
                {
                    PlayerPrefs.SetString(keyBindings[i].input, keyBindings[i].defaultInput);
                }
            }
        }
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
