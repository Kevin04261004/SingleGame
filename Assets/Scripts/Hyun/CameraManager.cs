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
    /// <summary>Scene의 모든 Camera인스턴스를 Dictionary에 추가</summary>
    void FindAllCamerasInCurrentScene()
    {
        Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
        for (int i = 0; i < cameras.Length; i++)
        {
            Camera cam = cameras[i];
            string key = cam.gameObject.name;
            if (camList.ContainsKey(key))
            {
                Debug.LogWarning($"'{key}'이름을 가진 카메라 오브젝트가 두 개 이상 존재합니다.");
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
