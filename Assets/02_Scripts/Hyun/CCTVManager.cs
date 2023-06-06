using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVManager : Singleton<CCTVManager>
{
    CCTV[] cctvList = null;
    Dictionary<string, CCTV> nameToCCTV = new Dictionary<string, CCTV>(8);

    protected override void Awake()
    {
        base.Awake();
        FindAndFillCCTVList();
    }

    void FindAndFillCCTVList()
    {
        cctvList = GameObject.FindObjectsOfType<CCTV>();
        for (int i = 0; i < cctvList.Length; i++)
        {
            nameToCCTV.Add(cctvList[i].cctvName, cctvList[i]);
        }
    }
    public CCTV GetCCTVWithName(string cctvName)
    {
        if (nameToCCTV.TryGetValue(cctvName, out CCTV cctv)) return cctv;
        else
        {
            Debug.LogWarning($"'{cctvName}'이름의 CCTV는 Scene에 존재하지 않습니다.");
            return null;
        }
    }
}
