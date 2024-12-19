using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof_Switch : MonoBehaviour, I_Interactable
{

    public bool first_use;

    public Animator animator;

    public GameObject santa;
    public GameObject chris;

    [SerializeField] private GameObject snowPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private Light directionalLight;
    private float duration = 60f;
    private float heightOffset = 40f;

    [SerializeField] AudioClip wallSwitchClip;
    [SerializeField] AudioClip song;

    void Start()
    {
        first_use = true;
    }

  public void Interact()
  {
    if(first_use){
        //print("we are interacting!");
        SoundManager.instance.PlaySoundFXClip(wallSwitchClip, transform, 1f);
        animator.SetTrigger("FLIP");




        // Spawn the snow above the players head
        Vector3 spawnPos = player.position + Vector3.up * heightOffset;
        GameObject snowInstance = Instantiate(snowPrefab, spawnPos, Quaternion.identity);

        // directional light intensity
        if (directionalLight != null)
        {
          directionalLight.intensity = 0.6f;
        }

        // coroutine for song play delay
        StartCoroutine(PlaySongDelayed());
        // coroutine to destroy it after some time
        StartCoroutine(DestroyAfterTime(snowInstance, duration));
        first_use = false;
    }
  }

  private IEnumerator DestroyAfterTime(GameObject obj, float time)
  {
    yield return new WaitForSeconds(time);
    if(directionalLight != null)
    {
      directionalLight.intensity = 0.1f;
    }
    Destroy(obj);
  }

  private IEnumerator PlaySongDelayed()
  {
    // wait 1 sec
    yield return new WaitForSeconds(1f);
    // now play
    SoundManager.instance.PlaySoundFXClip(song, transform, 1f);

    Animator santa_anim = santa.GetComponent<Animator>();
    chris.GetComponent<SpriteRenderer>().enabled = true;
    if(santa_anim != null)
    {
      santa_anim.SetTrigger("FLY");
    }
  }
}
