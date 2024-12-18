using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour
{
    public static AmbientSoundManager instance;
    public AudioClip outdoorAmbience;
    private AudioSource outdoorSource;

    private bool isInside;

    void Awake()
    {
        // singleton pattern
        if(instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isInside = false;
        outdoorSource = SoundManager.instance.PlayLoopingAmbientSound(outdoorAmbience, 1f, true);
        
    }

    public void SetInside(bool inside)
    {
        if(inside)
        {
            StartCoroutine(FadeAudio(outdoorSource, 1f, 0f, 1f));
        }
        else
        {
            StartCoroutine(FadeAudio(outdoorSource, 0f, 1f, 1f));
        }

    }

    private IEnumerator FadeAudio(AudioSource source, float startVolume, float endVolume, float duration)
    {
        float elapsed = 0f;
        source.volume = startVolume;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, endVolume, elapsed / duration);
            yield return null;
        }
        source.volume = endVolume;
    }
}
