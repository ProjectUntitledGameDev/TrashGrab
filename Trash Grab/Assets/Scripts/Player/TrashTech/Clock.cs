using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private GlobalData global;
    void Start()
    {

        global = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<GlobalData>();
        for (int i = 0; i < global.enemies.Length; i++)
        {
            if (!global.enemies[i].GetComponent<EnemyAI>().seen)
                global.enemies[i].GetComponent<EnemyAI>().thisDestination.target = this.gameObject.transform;
        }
    }


}
