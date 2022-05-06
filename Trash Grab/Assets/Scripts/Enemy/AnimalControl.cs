using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnimalControl : MonoBehaviour
{
    public TextMeshProUGUI display;
    private float fadeSpeed = 5;
    private float fadeAmount;
    private bool fadeInOrOut;
    public GameObject blackScreen;
    public GameObject cam;
    public Animator anim;
    public float totalTime = 30;
    public bool pauseTimer;
    public GameObject enemy;
    private GlobalData gd;
    private void Start()
    {
        gd = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<GlobalData>();
    }
    private IEnumerator Countdown()
    {
        while (totalTime >= 0)
        {
            if (!pauseTimer)
            {
                float displayFloat = (Mathf.Round(totalTime * 100) / 100);
                display.text = displayFloat.ToString();
                totalTime -= Time.deltaTime;
            }
            yield return null;
        }
        StartCoroutine("FadeToBlack");
    }



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
            cam.transform.position = this.transform.position;
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
            anim.SetTrigger("MoveCar");
            gd.gameOver = true;
        }
    }

    public void SpawnEnemy()
    {
        GameObject temp = Instantiate(enemy, this.gameObject.transform.position, Quaternion.identity);
        temp.GetComponent<EnemyAI>().aC = true;
        temp.GetComponent<EnemyAI>().blackScreen = gd.blackScreen;
        temp = Instantiate(enemy, this.gameObject.transform.position, Quaternion.identity);
        temp.GetComponent<EnemyAI>().aC = true;
        temp.GetComponent<EnemyAI>().blackScreen = gd.blackScreen;
    }


}
