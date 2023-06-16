using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageSystem
{
    public class Stage : MonoBehaviour
    {
        [SerializeField, ReadOnly] Area[] areasInStage = null;
        [SerializeField, ReadOnly] Area playerPos = null;
        [Header("Debug")]
        [SerializeField, ReadOnly] string playerPos_name;
        public Area.AreaType playerLocatedArea => playerPos.areaType;
        public void CallBack_PlayerEnteringArea(Area area, Area.EnterOrExit enterOrExit)
        {
            if (enterOrExit == Area.EnterOrExit.enter)
            {
                // ������ ���� �� �ݶ��̴� �̿� ��� �ݶ��̴� ��Ȱ��ȭ
                for (int i = 0; i < areasInStage.Length; i++)
                {
                    areasInStage[i].SetCollidersEnable((areasInStage[i] == area));
                }
                playerPos = area;
                playerPos_name = playerPos.areaName;
            }
            else
            {
                // ������ ������ ���� �ݶ��̴� �̿� ��� �ݶ��̴� Ȱ��ȭ
                for (int i = 0; i < areasInStage.Length; i++)
                {
                    areasInStage[i].SetCollidersEnable(!(areasInStage[i] == area));
                }
                playerPos = null;
                playerPos_name = string.Empty;
            }
        }

        public void Init()
        {
            for (int i = 0; i < areasInStage.Length; i++)
            {
                areasInStage[i].Init(CallBack_PlayerEnteringArea);
            }
        }

        private void Awake()
        {
            FindAndFillAreaComponentsFromAllChildren();
        }
        private void Start()
        {
            // StageManager�� �����ϰ� �� ��� Start���� ����
            Init();
        }
        private void FindAndFillAreaComponentsFromAllChildren()
        {
            int rounds = 0;
            Stack<Transform> s_targets = new Stack<Transform>(64);
            List<Area> l_area = new List<Area>(32);

            s_targets.Push(transform);

            while (s_targets.Count > 0)
            {
                ++rounds;
                Transform cur = s_targets.Pop();
                if (cur.TryGetComponent<Area>(out Area area))
                {
                    l_area.Add(area);
                }
                for (int i = 0; i < cur.childCount; i++)
                {
                    s_targets.Push(cur.GetChild(i));
                }
            }
            Debug.Log($"{rounds}���� ������Ʈ�� ��ȸ�� Stage�� ������ �ִ� {l_area.Count}���� {typeof(Area).Name}������Ʈ�� ã�ҽ��ϴ�.");
            areasInStage = l_area.ToArray();
        }
    }
}