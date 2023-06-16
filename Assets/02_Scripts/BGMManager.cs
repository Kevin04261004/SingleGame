using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMManager : Singleton<BGMManager>
{
    public AudioClip Title_BGM;
    public AudioClip[] BGM;
    public int BGMCount = 0;
    [SerializeField] private AudioSource soundSource;
    public Slider BGMSlider;
    public void Start()
    {
        BGM_Set();
    }
    public void BGM_Set()
    {
        soundSource = GetComponent<AudioSource>();
        BGMSlider.value = DontDestroyManager.instance.tempBGMSound;
        StartCoroutine(Playlist());
    }
    private void Update()
    {
        DontDestroyManager.instance.tempBGMSound = BGMSlider.value;
        soundSource.volume = BGMSlider.value;
    }

    IEnumerator Playlist()
    {
        if (SceneManager.GetActiveScene().name == "00_TitleScene")
        {
            soundSource.clip = Title_BGM;
            soundSource.Play();
        }
        else
        {
            if(soundSource.isPlaying)
            {
                soundSource.Stop();
            }
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                if (!soundSource.isPlaying)
                {
                    BGMCount++;
                    if (BGMCount >= BGM.Length)
                    {
                        BGMCount = 0;
                    }
                    soundSource.PlayOneShot(BGM[BGMCount]);
                }
            }
        }
    }
}