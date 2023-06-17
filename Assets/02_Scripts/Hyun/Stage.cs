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

        public delegate void Player_Area_Enter(Area.AreaType areaType);
        public event Player_Area_Enter event_player_area_enter;
        public delegate void Player_Area_Exit(Area.AreaType areaType);
        public event Player_Area_Exit event_player_area_exit;

        public void CallBack_PlayerEnteringArea(Area area, Area.EnterOrExit enterOrExit)
        {
            if (enterOrExit == Area.EnterOrExit.enter)
            {
                // 구역에 들어가면 들어간 콜라이더 이외 모든 콜라이더 비활성화
                for (int i = 0; i < areasInStage.Length; i++)
                {
                    areasInStage[i].SetCollidersEnable((areasInStage[i] == area));
                }
                playerPos = area;
                event_player_area_enter?.Invoke(playerPos.areaType);
                playerPos_name = playerPos.areaName;
            }
            else
            {
                // 구역을 나가면 나간 콜라이더 이외 모든 콜라이더 활성화
                for (int i = 0; i < areasInStage.Length; i++)
                {
                    areasInStage[i].SetCollidersEnable(!(areasInStage[i] == area));
                }
                event_player_area_exit?.Invoke(playerPos.areaType);
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
            event_player_area_enter += TEST_CALLBACK_PLAYER_ENTER_AREA;
            event_player_area_exit += TEST_CALLBACK_PLAYER_EXIT_AREA;
        }

        private void Awake()
        {
            FindAndFillAreaComponentsFromAllChildren();
        }
        private void Start()
        {
            // StageManager로 생성하게 될 경우 Start에서 제외
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
            Debug.Log($"{rounds}개의 오브젝트를 순회해 Stage의 하위에 있는 {l_area.Count}개의 {typeof(Area).Name}컴포넌트를 찾았습니다.");
            areasInStage = l_area.ToArray();
        }

        void TEST_CALLBACK_PLAYER_ENTER_AREA(Area.AreaType areaType)
        {
            Debug.Log($"플레이어가 {areaType}구역에 진입하였습니다.");
        }
        void TEST_CALLBACK_PLAYER_EXIT_AREA(Area.AreaType areaType)
        {
            Debug.Log($"플레이어가 {areaType}구역을 이탈하였습니다.");
        }
    }
}
