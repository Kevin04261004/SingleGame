using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public AudioClip OpenDoor_Sound;
    [SerializeField] private AudioSource soundSource;
    public Slider EffectSlider;

    private void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        soundSource.volume = EffectSlider.value;
    }
    public void PlayOpenDoor_Sound()
    {
        soundSource.PlayOneShot(OpenDoor_Sound);
    }
}
