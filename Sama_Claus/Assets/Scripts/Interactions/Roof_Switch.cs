using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof_Switch : MonoBehaviour, I_Interactable
{

    public bool first_use;

    public Animator animator;

    [SerializeField] private GameObject snowPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float duration = 10f;
    private float heightOffset = 40f;

    [SerializeField] AudioClip wallSwitchClip;

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
        // coroutine to destroy it after some time
        StartCoroutine(DestroyAfterTime(snowInstance, duration));
        first_use = false;
    }
  }

  private IEnumerator DestroyAfterTime(GameObject obj, float time)
  {
    yield return new WaitForSeconds(time);
    Destroy(obj);
  }
}
