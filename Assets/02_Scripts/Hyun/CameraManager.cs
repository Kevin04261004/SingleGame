using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RenderTextureData
{
    public string cam_name;
    public RenderTexture rt;
}

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] UnityEngine.UI.Image tempImage;
    [SerializeField] Material tempMat;
    public RenderTextureData[] cameraList;

    private static Dictionary<string, Camera> camList = new Dictionary<string, Camera>(32);

    protected override void Awake()
    {
        base.Awake();
        FindAllCamerasInCurrentScene();
    }
    /// <summary>Scene의 모든 Camera인스턴스를 Dictionary에 추가/summary>
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
    int temp_idx;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (++temp_idx > cameraList.Length - 1) temp_idx = 0;
            Material mat = new Material(tempMat);
            mat.mainTexture = cameraList[temp_idx].rt;
            tempImage.material = mat;
        }
    }
}
