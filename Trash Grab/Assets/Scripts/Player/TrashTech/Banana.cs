using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    float totalTime = 1.5f;
    GameObject caughtEnemy;
    private bool doOnce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!doOnce)
            {
                doOnce = true;
                this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
                caughtEnemy = collision.gameObject;
                collision.GetComponent<EnemyAI>().thisPath.maxSpeed = 0;
                StartCoroutine("Countdown");
            }
            
        }
    }
    


    private IEnumerator Countdown()
    {
        while (totalTime >= 0)
        {
            float displayFloat = (Mathf.Round(totalTime * 100) / 100);
            totalTime -= Time.deltaTime;
            yield return null;
        }
        caughtEnemy.GetComponent<EnemyAI>().thisPath.maxSpeed = 5;
    }
}
