using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTriggerSounds : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetLocalControllerRotation(OVRInput.GetActiveController()).x > 40 || OVRInput.GetLocalControllerRotation(OVRInput.GetActiveController()).z > 40)
        {
          
           //indObjectOfType<AudioManager>().Play("Shake");

            Debug.Log("Jetzt Shaken");
            GetComponent<AudioSource>().Play();
        }
    }
}
