using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    [SerializeField] GameObject FlashLightLight;
    private bool FlashLightActive = true;

    // Start is called before the first frame update
    void Start()
    {
        FlashLightActive = true;
        FlashLightLight.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        // turn on and off flashlight with F
        if(Input.GetKeyDown(KeyCode.F) ){

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
