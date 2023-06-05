using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

using Random = UnityEngine.Random;
public class LightManager : MonoBehaviour
{
    public Light[] lights;

    private void Start()
    {
        StartCoroutine(RandomBlink());
        SecondFloorLight_Off();
    }
    public void SecondFloorLight_Off()
    {
        light_Off(4);
        light_Off(5);
        light_Off(6);
    }
    IEnumerator RandomBlink()
    {
        while (true)
        {
            int randomnum = Random.Range(0, lights.Length);
            StartCoroutine(Blink(randomnum));
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }
    IEnumerator Blink(int index)
    {
        float temp = lights[index].intensity;

        for (int i = 0; i < 10; i++)
        {
            lights[index].intensity = Random.Range(temp -1, temp);
            
            yield return new WaitForSeconds(Random.Range(0.2f,0.3f));
        }
        lights[index].intensity = temp;
    }
    public void light_Off(int index)
    {
        lights[index].enabled = false;
    }
    public void light_On(int index)
    {
        lights[index].enabled = true;
    }
}
