using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    Camera camComp = null;
    [field: SerializeField] public string cctvName { get; private set; }
    public RenderTexture renderTexture = null;
    public Material materialByRenderTexture = null;

    private void Awake()
    {
        if (renderTexture == null || materialByRenderTexture == null)
        {
            Debug.LogError("필수로 할당해야하는 정보가 null입니다.");
            return;
        }
        camComp = GetComponent<Camera>();
    }

    public RenderTexture GetRenderTexture() => renderTexture;
    public Material GetMaterialMadeByRenderTexture() => materialByRenderTexture;
}
