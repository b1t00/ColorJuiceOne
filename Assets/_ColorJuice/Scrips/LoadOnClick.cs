using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadOnClick : MonoBehaviour
{  
    public string scene;

    public void NewScene(){
        SceneManager.LoadScene(scene);
        print("click");
    }
}
