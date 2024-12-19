using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof_Switch : MonoBehaviour, I_Interactable
{

    public bool first_use;

    public Animator animator;

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
        first_use = false;
    }
  }
}
