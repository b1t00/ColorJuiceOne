using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public GameObject cube; //public Variable vom Typ GameObject. Kann im Inspector initialisiert werden
    private GameObject supercube; //private
    private GameObject tagtCube;
    private GameObject[] tagtCubes;
    // Start is called before the first frame update
    void Start()
    {
        supercube = GameObject.Find("SuperCube");  // GameObject kann man Find() Methode über den Namen gefunden werden

        tagtCube = GameObject.FindGameObjectWithTag("CubeTag");
        tagtCubes = GameObject.FindGameObjectsWithTag("CubeTag");

        Debug.Log("ich bin " + gameObject.name); // gameObject ist gameObject auf dem sich das Script befindet
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.Rotate(0, 2, 3);
        supercube.transform.Rotate(0, 2, 0);

        foreach (GameObject cube in tagtCubes) {
            cube.transform.Rotate(3, 0, 0);
        }

    }
}
