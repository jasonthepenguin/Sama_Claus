using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        // TODO: fill in once statemachine is created
    }
    public enum AnimationTriggerType{
        HandToFace,
        TorchDown

    }


}
