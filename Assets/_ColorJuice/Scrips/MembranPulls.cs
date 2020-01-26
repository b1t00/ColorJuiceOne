using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembranPulls : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 localSc;
    float count;

    private bool musicOn;
    void Start()
    {
        localSc = gameObject.transform.localScale;
        musicOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        count++;
            Debug.Log(count);
        if (musicOn)
        {
        if (count % 30 == 0)
        {
            localSc.x = 0.84f;
            localSc.y = 0.8f;
            gameObject.transform.localScale = localSc;
        } if(count % 30 == 20)
        {
            localSc.x = 0.6f;
            localSc.y = 0.6f;
            gameObject.transform.localScale = localSc;
        }

        }

    }
        public void SetMusicOn(bool on)
        {
            musicOn = on;
        }
        
}
