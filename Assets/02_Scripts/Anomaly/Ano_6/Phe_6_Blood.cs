using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phe_6_Blood : Phenomenon, IInteratable
{
    [SerializeField] float need_interactTime = 4f;
    [SerializeField, ReadOnly] float cnt_interactTime = -1f;
    AudioSource adSrc;

    private void Awake()
    {
        adSrc = GetComponent<AudioSource>();
    }

    public void Interact() { }

    public void Interact_Hold()
    {
        Solution();
    }

    public void Interact_Hold_End() { }

    public bool IsInteractable() => true;

    [SerializeField] float splatSoundDel = 0.7f;
    [SerializeField, ReadOnly] float cnt_splatSoundDel;

    private void Update()
    {
        if (cnt_splatSoundDel > float.Epsilon)
        {
            cnt_splatSoundDel -= Time.deltaTime;
        }
    }

    protected override void PhenomenonEnd()
    {
        if (adSrc.isPlaying)
        {
            adSrc.Stop();
        }
    }

    protected override void PhenomenonStart()
    {
        cnt_interactTime = need_interactTime;
    }

    protected override void Solution()
    {
        if (cnt_splatSoundDel <= float.Epsilon)
        {
            cnt_splatSoundDel += splatSoundDel;
            adSrc.Play();
        }
        if ((cnt_interactTime -= Time.deltaTime) <= float.Epsilon)
        {
            TryFixThisPhenomenon();
        }
    }
}
