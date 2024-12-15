using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    [SerializeField] GameObject FlashLightLight;
    [SerializeField] AudioClip buttonPressClip;
    private bool FlashLightActive = true;
    private float defaultIntensity;

    // Start is called before the first frame update
    void Start()
    {
       // defaultIntensity = FlashLightLight.
        FlashLightActive = true;
        FlashLightLight.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        // turn on and off flashlight with F
        if(Input.GetKeyDown(KeyCode.F) ){

            // play button sound
            SoundManager.instance.PlaySoundFXClip(buttonPressClip, transform, 1f);

            if(FlashLightActive)
            {
                FlashLightLight.gameObject.SetActive(false);
                FlashLightActive = false;
            }
            else{
                FlashLightLight.gameObject.SetActive(true);
                FlashLightActive = true;
            }
           
        }
    }
}
