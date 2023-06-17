using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageSystem
{
    [System.Serializable, RequireComponent(typeof(Collider))]
    public class Area : MonoBehaviour
    {
        public enum AreaType
        {
            controlRoom, mainHall, counter,
            corrider_1F, corrider_2F, corrider_3F,
            landing_1_5F, landing_2_5F, landing_3_5F
        }
        public enum EnterOrExit
        {
            enter, exit
        }
        public string areaName = "DefaultArea";
        int triggerCount = 0;

        public delegate void EnteringArea(Area area, EnterOrExit enterOrExit);
        EnteringArea when_player_enterOrExit_area;
        Collider[] colliders;

        [field: SerializeField] public AreaType areaType { get; private set; } = AreaType.controlRoom;

        private void Awake()
        {
            colliders = GetComponents<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].isTrigger == false) colliders[i].isTrigger = true;
            }
            SetCollidersEnable(false);
        }

        public void Init(EnteringArea callback_enteringArea)
        {
            when_player_enterOrExit_area = callback_enteringArea;
            SetCollidersEnable(true);
        }
        public void SetCollidersEnable(bool value)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = value;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                when_player_enterOrExit_area(this, EnterOrExit.enter);
                triggerCount++;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && --triggerCount == 0)
            {
                when_player_enterOrExit_area(this, EnterOrExit.exit);
            }
        }
    }
}
