using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Scriptkomponente, die auf den Objecten für die Farbauswahl liegt

public class PointerEventCansBoden : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    //Farben werden im Inspector festgelegt
    [SerializeField] private Color normalColor; // Die Farbe die zum Sprühen verwendet wird
    [SerializeField] private Color enterColor;  // Farbe die angezeigt wird, wenn man mit pointer über object geht
    //[SerializeField] private Color downColor; // Farbe die angezeigt wird, wenn man bestätigt (pointer down)
    [SerializeField] private UnityEvent OnClick = new UnityEvent();

    private GameObject can; //sprühdose/object das mit bei der Farbauswahl auswählt

    private MeshRenderer meshRenderer = null;

    private Vector3 locPos;

    private GameObject[] bodenCans;

    private bool enter = false;
    private bool stay = false;

    private void Awake()
    {
        //can = GameObject.FindGameObjectWithTag("HandCan");
        meshRenderer = GetComponent<MeshRenderer>();
        Debug.Log(meshRenderer.name);

        var chooseRenderer = gameObject.GetComponent<Renderer>();
        chooseRenderer.material.SetColor("_Color", normalColor); // Beim Laden wird dem Object die Farbe normalColor zugewiesen
        
        locPos = gameObject.transform.localPosition;
    }

    private void Update()
    {

        if (enter || stay)
        {
            locPos.y = -1f;
            gameObject.transform.localPosition = locPos;
        }
        else
        {
            locPos.y = -1.64f;
            gameObject.transform.localPosition = locPos;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        meshRenderer.material.color = enterColor;
        //print("Enter");

        enter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        meshRenderer.material.color = normalColor;
        //print("Exit");
        enter = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //meshRenderer.material.color = downColor;
        //print("Down");
        enter = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //  meshRenderer.material.color = enterColor;
        //print("Up");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
        //print("Click");

        /*bodenCans = GameObject.FindGameObjectsWithTag("BodenCan"); //holt sich alle kacheln

        foreach (GameObject cani in bodenCans)
        {
            if (cani.name == gameObject.name) //schaut ob kachel aus array diese kachel ist
            {
                this.stay = true;
            }
            else
            {
                cani.GetComponent<PointerEventCansBoden>().SetStay(false);
            }
        }

    */
    }

    public Color GetNormalColor() //public methode um farbe zu übergeben. Wichtig für die Farbauswahl
    {
        return normalColor;
    }

    public void SetStay(bool stay)
    {
        this.stay = stay;
    }
}
