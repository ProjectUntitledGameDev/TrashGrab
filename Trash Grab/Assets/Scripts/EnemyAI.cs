using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
    public Transform[] activityPoints;
    public AIPath thisPath;
    public AIDestinationSetter thisDestination;
    private int lastPoint;
    public int sightRad;
    public int sightAngle;
    public LayerMask target;
    public LayerMask obsticle;
    private float delay = 15f;
    private bool seen;
    public UIController blackScreen;
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    void FindVisibleTarget()
    {
        Collider2D[] targetsInViewRad = Physics2D.OverlapCircleAll(transform.position, sightRad, target);
        for (int i = 0; i < targetsInViewRad.Length; i++)
        {
            Transform target = targetsInViewRad[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, dirToTarget) < sightAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, dirToTarget, distance, obsticle))
                {
                    thisDestination.target = target;
                    delay = 15f;
                    seen = true;
                    thisPath.maxSpeed = 4;
                }
            }
        }

    }
    private void Awake()
    {
        int i = Random.Range(0, 8);
        thisDestination.target = activityPoints[i];
        lastPoint = i;
        StartCoroutine(FindTargetWithDelay(0.5f));
    }
    private void Update()
    {
        if (seen)
        {
            if (delay > 0)
            {

                if (delay < 0.5)
                {
                    delay = 0;
                    int i = Random.Range(0, 8);
                    if (i == lastPoint)
                        i++;
                    thisDestination.target = activityPoints[i];
                    delay = 15f;
                    seen = false;
                    thisPath.maxSpeed = 3;
                }
                else
                {
                    delay = Mathf.Lerp(delay, 0, 1 * Time.deltaTime);
                }
            }
        }
        if (thisPath.reachedDestination)
        {
            if (!seen)
            {
                int i = Random.Range(0, 8);
                if (i == lastPoint)
                    i++;
                thisDestination.target = activityPoints[i];
            }
            else
            {
                blackScreen.StartFade(0, true, false, true) ;
            }
           
        }
    }

    
    public Vector2 DirFromAngle(float angleInDeg, bool global)
    {
        if (!global)
        {
            angleInDeg -= transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }

}
