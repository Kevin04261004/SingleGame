using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Phe_13_invisible : Phenomenon
{
    [SerializeField] Volume volComp;
    protected override void PhenomenonEnd()
    {
        
    }

    protected override void PhenomenonStart()
    {
        
    }

    protected override void Solution()
    {
        GetComponent<Collider>().enabled = false;
        volComp.enabled = false;
        TryFixThisPhenomenon();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Solution();
        }
    }
}
