using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Scriptkomponente, die auf den Objecten für die Farbauswahl liegt

public class PointerEventFarbkachel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    //Farben werden im Inspector festgelegt
    [SerializeField] private Color normalColor; // Die Farbe die zum Sprühen verwendet wird
    //[SerializeField] private Color enterColor;  // Farbe die angezeigt wird, wenn man mit pointer über object geht
    //[SerializeField] private Color downColor; // Farbe die angezeigt wird, wenn man bestätigt (pointer down)
    [SerializeField] private UnityEvent OnClick = new UnityEvent();

    private GameObject can; //sprühdose/object das mit bei der Farbauswahl auswählt

    private MeshRenderer meshRenderer = null;

    private Vector3 locPos;

    private GameObject[] kacheln;

    bool stay;
    bool enter;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Debug.Log(meshRenderer.name);

        var chooseRenderer = gameObject.GetComponent<Renderer>();
        chooseRenderer.material.SetColor("_Color", normalColor); // Beim Laden wird dem Object die Farbe normalColor zugewiesen

        stay = false;
        enter = false;
        
    }
    private void Start()
    {
        kacheln = GameObject.FindGameObjectsWithTag("Farbkachel");
    }

    private void Update()
    {
        locPos = gameObject.transform.localPosition;


        if (enter || stay)
        {
            locPos.y = 0.8f;
            gameObject.transform.localPosition = locPos;


        }
        else
        {
            locPos.y = 0.3f;
            gameObject.transform.localPosition = locPos;


        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //meshRenderer.material.color = enterColor;
        enter = true;
        print("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //meshRenderer.material.color = normalColor;
        enter = false;
        print("Exit");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //meshRenderer.material.color = downColor;
        //print("Down");
        foreach (GameObject cani in kacheln)
        {
            if (cani.Equals(gameObject)) //schaut ob kachel aus array diese kachel ist
            {
                stay = true;
            }
            else
            {
                cani.GetComponent<PointerEventFarbkachel>().SetStay(false);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //meshRenderer.material.color = enterColor;
        print("Up");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //OnClick.Invoke();
        print("Click");
        //var canRenderer = can.GetComponent<Renderer>();
        //canRenderer.material.SetColor("_Color", normalColor);

         //holt sich alle kacheln

       

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
