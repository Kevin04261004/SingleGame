using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_9_Guard : Phenomenon
{
    [SerializeField] float needStayTime = 10f;
    [SerializeField, ReadOnly] float counter_nst = -1;
    [SerializeField] Area_DetectPlayer prefab_detectZone;
    [SerializeField] Transform forward;
    [Header("순회")]
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] Vector3[] patrolPos;
    Vector3 cur_target_patrol_pos;
    [SerializeField] float detect_range = 6f;
    [SerializeField] float rotateSpeed = 180f;
    Area_DetectPlayer detectZone;

    protected override void PhenomenonEnd()
    {
        detectZone.event_time_playerInArea -= CheckStayTime;
        Destroy(detectZone.gameObject);
    }

    protected override void PhenomenonStart()
    {
        detectZone = Instantiate(prefab_detectZone);
        detectZone.event_time_playerInArea += CheckStayTime;
        counter_nst = needStayTime;
    }
    // todo: 순찰도는 기능 추가
    List<Ray> showRay = new List<Ray>();
    private void Update()
    {
        Collider[] d =
            Physics.OverlapBox((forward.transform.position + forward.forward * detect_range * 0.5f), new Vector3(detect_range, 0.3f, 3f) * 0.5f, Quaternion.identity, (1 << LayerMask.NameToLayer("Player")));
        if (d.Length > 0)
        {
            for (int i = 0; i < d.Length; i++)
            {
                Ray rr = new Ray(forward.transform.position, (d[i].transform.position - forward.transform.position) * detect_range); showRay.Add(rr);
                if (Physics.Raycast(forward.transform.position, d[i].transform.position - forward.transform.position, out RaycastHit hit, detect_range, (int.MaxValue ^ 1 << LayerMask.NameToLayer("Area"))))
                {
                    Debug.Log($"{hit.transform.gameObject.name}");
                    if (hit.collider.CompareTag("Player") && hit.collider == d[i])
                    {
                        DetectPlayer();
                    }
                }
            }
        }
        for (int i = 0; i < showRay.Count; i++)
        {
            Debug.DrawRay(showRay[i].origin, showRay[i].direction);
        }
    }

    void DetectPlayer()
    {
        Debug.Log("플레이어가 경비원에게 발각당하였습니다.");
        GameManager.instance.Died(GameManager.CauseOfDeath.detectedBySomething, "규칙 9: 시야 내에 들어오면");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((forward.transform.position + forward.forward * detect_range * 0.5f), new Vector3(detect_range, 0.3f, 3f));
    }

    protected override void Solution()
    {
        // Ano9의 경우 현상이 Guard하나라 상관 없지만
        // 현상이 추가될 경우 event를 빨리 빼거나 bool isAlreadyDone을 두어야 함
        if (counter_nst <= float.Epsilon)
        {
            TryFixThisPhenomenon();
        }
    }

    void CheckStayTime(float deltaTime)
    {
        counter_nst -= deltaTime;
        Solution();
    }
}
