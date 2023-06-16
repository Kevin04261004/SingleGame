using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetManager : MonoBehaviour
{
    public void Start()
    {
        FindObjectOfType<BGMManager>().BGM_Set();
    }
}