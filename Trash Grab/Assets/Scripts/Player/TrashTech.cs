using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTech : MonoBehaviour
{
    public GameObject banana;
    public GameObject clock;
    public string[] techOne;
    public string[] techTwo;
    public Sprite[] allsprites;
    
    private void Start()
    {
        
        if(PlayerPrefs.GetString("TT1") == "")
        {
            PlayerPrefs.SetString("TT1", "Banana");
        }
        if(PlayerPrefs.GetString("TT2") == "")
        {
            PlayerPrefs.SetString("TT2", "Snack");
        }
    }
    public void Banana()
    {
        Instantiate(banana, this.gameObject.transform.position, Quaternion.identity);
    }
    public void Snack()
    {
        Instantiate(clock, this.gameObject.transform.position, Quaternion.identity);
    }
}
