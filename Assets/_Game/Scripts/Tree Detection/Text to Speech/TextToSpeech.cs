using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextToSpeech : MonoBehaviour
{
    public AudioSource _audio;
    public InputField inputTExt;
    public AudioSource targetAudioSource;
    public VoiceChangerFilter targetFilter;
    // Start is called before the first frame update
    void Start()
    {
        _audio = gameObject.GetComponent<AudioSource>();        
    }

    IEnumerator DownloadAudio()
    {
        string url = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q="+inputTExt.text+"&tl=En-gb";
            WWW www = new WWW(url);
        yield return www;
        _audio.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        targetAudioSource.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        //_audio.Play();
        targetAudioSource.Play();
    }
    public void OnButtonClick()
    {
        StartCoroutine(DownloadAudio());
    }
}
