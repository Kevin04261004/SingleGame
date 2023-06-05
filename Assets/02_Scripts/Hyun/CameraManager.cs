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
    /// <summary>Scene�� ��� Camera�ν��Ͻ��� Dictionary�� �߰�/summary>
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
