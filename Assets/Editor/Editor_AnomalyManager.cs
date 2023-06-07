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
        idx = EGL.IntField("������ �ε��� (-1: ����)", idx);
        if (GUILayout.Button("����"))
        {
            if (!Application.isPlaying)
            {
                Debug.LogWarning($"{amanager.GetType().Name}: ������ �������϶����� ������ �� �ֽ��ϴ�.");
                return;
            }
            if (amanager.everyAnoIsExecuting)
            {
                Debug.LogWarning($"Anomaly�� �������� ���߽��ϴ�.\n List�� ��� Anomaly�� �������� �� �ֽ��ϴ�.");
                return;
            }
            if (!amanager.ExecuteAnomaly(idx))
            {
                if (idx == -1)
                {
                    int tryCnt = 0;
                    while (!amanager.ExecuteAnomaly(idx) && ++tryCnt < 10) { }
                    if (tryCnt == 10)
                    {
                        Debug.LogWarning($"Anomaly�� �������� ���߽��ϴ�.\n {idx}��° Anomaly�� ���� ������ �������� �ʾ��� �� �ֽ��ϴ�.");
                    }
                }
                else
                {
                    Debug.LogWarning($"Anomaly�� �������� ���߽��ϴ�.\n {idx}��° Anomaly�� �̹� �������� �� �ֽ��ϴ�.");
                    return;
                }
            }
        }
        if (GUILayout.Button("��� Anomaly����"))
        {
            if (!Application.isPlaying)
            {
                Debug.LogWarning($"{amanager.GetType().Name}: ������ �������϶����� ������ �� �ֽ��ϴ�.");
                return;
            }
            if (amanager.everyAnoIsExecuting)
            {
                Debug.LogWarning($"�� ���� Anomaly�� �������� ���߽��ϴ�.\n List�� ��� Anomaly�� �������� �� �ֽ��ϴ�.");
                return;
            }
            for (int i = 0; i < amanager.anomalyList.Length; i++)
            {
                amanager.ExecuteAnomaly(i);
            }
        }
    }
}
