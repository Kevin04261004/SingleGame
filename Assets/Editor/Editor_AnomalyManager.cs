using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using EGL = UnityEditor.EditorGUILayout;

[CustomEditor(typeof(AnomalyManager))]
public class Editor_AnomalyManager : Editor
{
    private int idx = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20);
        AnomalyManager amanager = (AnomalyManager)target;
        idx = EGL.IntField("실행할 인덱스 (-1: 랜덤)", idx);
        if (GUILayout.Button("실행"))
        {
            if (!Application.isPlaying)
            {
                Debug.LogWarning($"{amanager.GetType().Name}: 게임을 실행중일때에만 생성할 수 있습니다.");
                return;
            }
            if (amanager.everyAnoIsExecuting)
            {
                Debug.LogWarning($"Anomaly를 실행하지 못했습니다.\n List의 모든 Anomaly가 실행중일 수 있습니다.");
                return;
            }
            if (!amanager.ExecuteAnomaly(idx))
            {
                if (idx == -1)
                {
                    while (!amanager.ExecuteAnomaly(idx)) { }
                }
                else
                {
                    Debug.LogWarning($"Anomaly를 실행하지 못했습니다.\n {idx}번째 Anomaly가 이미 실행중일 수 있습니다.");
                    return;
                }
            }
            
        }
    }
}
