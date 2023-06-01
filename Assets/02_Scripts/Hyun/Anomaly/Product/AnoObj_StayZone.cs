using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnoObj_StayZone : MonoBehaviour
{
    public float requireRemainTime = 5f;
    [SerializeField] float counter_remaintime = -1f;

    public Collider zone;
    private void OnCollisionStay(Collision collision)
    {
        
    }

}
