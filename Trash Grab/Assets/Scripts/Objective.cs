using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Objective : MonoBehaviour
{
    readonly int range;
    public GameObject interact;
    public TextMeshProUGUI text;
    private void Awake()
    {
        text.text = "Press '" + PlayerPrefs.GetString("Interact").ToUpper() + "' to Interact";
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            interact.SetActive(true);
            other.GetComponent<Movement>().interactible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interact.SetActive(false);
            other.GetComponent<Movement>().interactible = false;
        }
    }
}
