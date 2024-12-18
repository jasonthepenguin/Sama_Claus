using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrigger : MonoBehaviour
{

    private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            if(player != null)
            {
                player.nearLadder = true;
            }
            //AmbientSoundManager.instance.SetInside(true);
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            if(player != null)
            {
                player.nearLadder = false;
            }
            //AmbientSoundManager.instance.SetInside(false);
        }
    }
}
