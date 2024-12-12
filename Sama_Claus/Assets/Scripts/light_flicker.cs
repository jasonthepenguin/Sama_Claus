using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_flicker : MonoBehaviour
{
    [Tooltip("Minimum time between flickers.")]
    public float minWaitTime = 0.05f;
    
    [Tooltip("Maximum time between flickers.")]
    public float maxWaitTime = 0.2f;

    [Tooltip("How long the flicker stays active (light off) before returning.")]
    public float flickerDuration = 0.1f;
    
    private Light spotLight;
    private bool isFlickering = false;

    void Start()
    {
        spotLight = GetComponent<Light>();
        if (spotLight == null)
        {
            Debug.LogError("No Light component found on this GameObject. Please add a Light component.");
        }
    }

    void Update()
    {
        // Start the flicker sequence if we’re not already flickering
        if (!isFlickering && spotLight != null)
        {
            StartCoroutine(Flicker());
        }
    }

    IEnumerator Flicker()
    {
        isFlickering = true;
        
        // Wait for a random time before flickering again
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);

        // Turn off the light to simulate a flicker
        spotLight.enabled = false;

        // Wait for the flicker duration
        yield return new WaitForSeconds(flickerDuration);

        // Turn the light back on
        spotLight.enabled = true;

        isFlickering = false;
    }
}
