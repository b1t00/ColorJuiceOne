using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest : MonoBehaviour
{
    int numb = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 30 * Time.deltaTime, numb);
        //transform.Translate(0, 0, 3 * Time.deltaTime);
        //transform.Translate(0, 0, 3 * Time.deltaTime);
        //Time.deltaTime der Wert in Sekunden den es gebraucht hat um das letzte Frame zu laden (entschläunigt alles)
    }
}
