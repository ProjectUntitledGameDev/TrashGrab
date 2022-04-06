using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyAI))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyAI ai = (EnemyAI)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(ai.transform.position, Vector3.forward, Vector3.up, 360, ai.sightRad);
        Vector3 viewAngle = ai.DirFromAngle(-ai.sightAngle / 2, false);
        Vector3 viewAngleB = ai.DirFromAngle(ai.sightAngle / 2, false);
        Handles.DrawLine(ai.transform.position, ai.transform.position + viewAngle * ai.sightRad);
        Handles.DrawLine(ai.transform.position, ai.transform.position + viewAngleB * ai.sightRad);
    }
}
