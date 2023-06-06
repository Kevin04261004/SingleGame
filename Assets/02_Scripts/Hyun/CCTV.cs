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
            Debug.LogError("�ʼ��� �Ҵ��ؾ��ϴ� ������ null�Դϴ�.");
            return;
        }
        camComp = GetComponent<Camera>();
    }

    public RenderTexture GetRenderTexture() => renderTexture;
    public Material GetMaterialMadeByRenderTexture() => materialByRenderTexture;
}
