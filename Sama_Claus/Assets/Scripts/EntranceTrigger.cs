using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            AmbientSoundManager.instance.SetInside(true);
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AmbientSoundManager.instance.SetInside(false);
        }
    }
    
    
}
