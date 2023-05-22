using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private static Dictionary<string, Camera> camList = new Dictionary<string, Camera>(32);

    protected override void Awake()
    {
        base.Awake();
        FindAllCamerasInCurrentScene();
    }
    /// <summary>Scene�� ��� Camera�ν��Ͻ��� Dictionary�� �߰�</summary>
    void FindAllCamerasInCurrentScene()
    {
        Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
        for (int i = 0; i < cameras.Length; i++)
        {
            Camera cam = cameras[i];
            string key = cam.gameObject.name;
            if (camList.ContainsKey(key))
            {
                Debug.LogWarning($"'{key}'�̸��� ���� ī�޶� ������Ʈ�� �� �� �̻� �����մϴ�.");
                continue;
            }
            camList.Add(key, cam);
        }
    }
    public Camera FindCameraInstanceWithObjectName(string objName)
    {
        if (camList.TryGetValue(objName, out Camera cam)) return cam;
        Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].gameObject.name == objName) return cameras[i];
        }
        return null;
    }
}
