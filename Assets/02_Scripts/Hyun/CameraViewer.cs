using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
/// <summary>
/// 모든 Room을 찾고, 그 안에 해당하는 CCTV들을 모두 표시
/// </summary>
public class CameraViewer : MonoBehaviour
{
    public CCTV[] cctvList;

    private void Start()
    {
        cctvList = GetAllCCTVFromRooms();
        for (int i = 0; i < cctvList.Length; i++)
        {
            //GameObject go = new GameObject($"Screen_{cctvList[i].attachedRoom.roomName}");
            //go.transform.SetParent(transform);

            //MeshFilter mf = go.AddComponent<MeshFilter>();
            //mf.mesh = CreateScreenMesh(2f);

            //Material mt = new Material(Shader.Find("Unlit/Texture"));
            //RenderTexture rt = cctvList[i].renderTexture;
            //mt.mainTexture = rt;

            //MeshRenderer mr = go.AddComponent<MeshRenderer>();
            //mr.material = mt;

            //go.transform.localPosition = new Vector3((i % 3) * 2, 0, (i / 3) * 2);
        }
    }
    /// <summary>
    /// Room Component를 포함하는 게임 오브젝트에 포함된 CCTV를 반환<br/>
    /// ※ Room.cctvList를 참조하기 때문에 각 Room의 초기화 상태, 호출 순서에 주의.
    /// </summary>
    public CCTV[] GetAllCCTVFromRooms()
    {
        Room[] rooms = GameObject.FindObjectsOfType<Room>();
        List<CCTV> _cctvList = new List<CCTV>(64);

        for (int i = 0; i < rooms.Length; i++)
        {
            CCTV[] cur = rooms[i].cctvList;
            for (int j = 0; j < cur.Length; j++)
            {
                _cctvList.Add(cur[j]);
            }
        }
        return _cctvList.ToArray();
    }
    /// <summary>
    /// 좌측, 상단 -> 우측, 하단 방향의 Plane Mesh를 반환
    /// </summary>
    public Mesh CreateScreenMesh(float meshScale = 1f)
    {
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(meshScale * -0.5f, 0f, meshScale * 0.5f),
            new Vector3(meshScale * 0.5f, 0f, meshScale * 0.5f),
            new Vector3(meshScale * -0.5f, 0f, meshScale * -0.5f),
            new Vector3(meshScale * 0.5f, 0f, meshScale * -0.5f)
        };
        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(0, 0),
            new Vector2(1, 0)
        };
        int[] triangles = new int[6]
        {
            0, 3, 2,
            0, 1, 3
        };
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        return mesh;
    }
}
