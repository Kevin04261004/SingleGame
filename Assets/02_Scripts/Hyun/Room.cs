using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("Room -> Stage, Area")]
public class Room : MonoBehaviour
{
    [HideInInspector]
    public CCTV[] cctvList { get; private set; }
    public string roomName = string.Empty;

    private void Awake()
    {
        FindAndFillCCTVList();
    }
    public void FindAndFillCCTVList()
    {
        List<CCTV> _cctvList = new List<CCTV>(64);
        Queue<Transform> t_queue = new Queue<Transform>(64);
        t_queue.Enqueue(transform);
        while (t_queue.Count > 0)
        {
            Transform current = t_queue.Dequeue();
            if (current.gameObject.TryGetComponent<CCTV>(out CCTV cctv))
            {
                //cctv.SetRoomInstance(this);
                _cctvList.Add(cctv);
            }
            for (int j = 0; j < current.childCount; j++)
            {
                t_queue.Enqueue(current.GetChild(j));
            }
        }
        cctvList = _cctvList.ToArray();
    }
}
