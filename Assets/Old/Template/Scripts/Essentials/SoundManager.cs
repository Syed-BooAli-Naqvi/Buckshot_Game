using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonDoNotDestroy<SoundManager>
{
    public SoundData data;
    public AudioSource BG;

    public bool test;
    private void OnValidate()
    {
        if (test)
        {
            test = false;
            PlaySound(SoundName.ButtonClick);
        }
    }

    private void Start()
    {
        PlayBG(SoundName.Background);
    }

    public void PlayBG(SoundName name)
    {
        for (int i = 0; i < data.sounds.Count; i++)
        {
            if (data.sounds[i].soundName == name)
            {
                BG.loop = data.sounds[i].loop;
                BG.playOnAwake = false;
                BG.clip = data.sounds[i].clip;
                float finalVolume = PlayerPrefs.GetFloat(SharedPrefs.MusicLevel, 0.3f);
                Debug.LogError(finalVolume);
                BG.volume = finalVolume;
                BG.Play();
            }
        }
    }
    public void setVloume(float volume)
    {
        BG.volume = volume;
    }


    public void PlaySound(SoundName name = SoundName.None, GameObject targetObj = null)
    {
        bool destroy = false;
        if (!targetObj)
        {
            destroy = true;
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            targetObj = obj;
        }
        if (!targetObj.GetComponent<AudioSource>())
        {
            targetObj.AddComponent<AudioSource>();
        }
        for (int i = 0; i < data.sounds.Count; i++)
        {
            if (data.sounds[i].soundName == name)
            {
                targetObj.GetComponent<AudioSource>().loop = data.sounds[i].loop;
                targetObj.GetComponent<AudioSource>().playOnAwake = false;
                targetObj.GetComponent<AudioSource>().clip = data.sounds[i].clip;
                float finalVolume = PlayerPrefs.GetFloat(SharedPrefs.SoundLevel, 1);
                targetObj.GetComponent<AudioSource>().volume = finalVolume;
                targetObj.GetComponent<AudioSource>().Play();
            }
        }
        if (destroy && !targetObj.GetComponent<AudioSource>().loop)
            StartCoroutine(DestroyObject(targetObj, targetObj.GetComponent<AudioSource>().clip.length));
    }
    IEnumerator DestroyObject(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        if (obj)
            Destroy(obj);
    }
    public void SetMusicLevel(float level)
    {
        PlayerPrefs.SetFloat(SharedPrefs.MusicLevel, level);
        BG.volume = level;
    }
    public void SetSoundLevel(float level)
    {
        PlayerPrefs.SetFloat(SharedPrefs.SoundLevel, level);
    }
}