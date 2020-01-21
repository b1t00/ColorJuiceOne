using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembranPulls : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 localSc;
    float count;
    void Start()
    {
        localSc = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        count++;
            Debug.Log(count);
        if (count % 8 == 0)
        {
            localSc.x = 0.8f;
            gameObject.transform.localScale = localSc;
        } if(count % 8 == 4)
        {
            localSc.x = 0.6f;
            gameObject.transform.localScale = localSc;
        }
        
    }
}
