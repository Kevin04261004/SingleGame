using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    public Room attachedRoom { get; private set; } = null;
    Camera camComp = null;
    public RenderTexture renderTexture { get; private set; } = null;
    private void Awake()
    {
        camComp = GetComponent<Camera>();
        renderTexture = new RenderTexture(Screen.width, Screen.height, 32);
        camComp.targetTexture = renderTexture;
    }
    public void SetRoomInstance(Room instance) => attachedRoom = instance;
}
