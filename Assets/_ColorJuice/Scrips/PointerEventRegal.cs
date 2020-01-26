using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Scriptkomponente, die auf den Objecten für die Farbauswahl liegt

public class PointerEventRegal: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    //Farben werden im Inspector festgelegt
    [SerializeField] private Color normalColor; // Die Farbe die zum Sprühen verwendet wird
    //[SerializeField] private Color enterColor;  // Farbe die angezeigt wird, wenn man mit pointer über object geht
    //[SerializeField] private Color downColor; // Farbe die angezeigt wird, wenn man bestätigt (pointer down)
    [SerializeField] private UnityEvent OnClick = new UnityEvent();

    public GameObject can; //sprühdose/object das mit bei der Farbauswahl auswählt

    private MeshRenderer meshRenderer = null;

    private Vector3 locPos;
    bool enter;
    bool stay;

    private GameObject[] alleCans;
    


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Debug.Log(meshRenderer.name);

        var chooseRenderer = gameObject.GetComponent<Renderer>();
        chooseRenderer.material.SetColor("_Color", normalColor); // Beim Laden wird dem Object die Farbe normalColor zugewiesen

        enter = false;
        stay = false;
        
    }
    private void Start()
    {
        alleCans = GameObject.FindGameObjectsWithTag("Can"); //holt sich alle kacheln
        
    }
    private void Update()
    {
        locPos = gameObject.transform.localPosition;

        if (enter || stay)
        {
            locPos.x = -6.32f;
            gameObject.transform.localPosition = locPos;
        }
        else
        {
            locPos.x = -6.635f;
            gameObject.transform.localPosition = locPos;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //eshRenderer.material.color = enterColor;
        print("Enter");
        enter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //meshRenderer.material.color = normalColor;
        print("Exit");
        enter = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //meshRenderer.material.color = downColor;
        //print("Down");
        foreach (GameObject yo in alleCans)
        {
            if (yo.Equals(gameObject)) //schaut ob kachel aus array diese kachel ist
            {
                this.stay = true;
            }
            else
            {
                yo.GetComponent<PointerEventRegal>().SetStay(false);
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
        OnClick.Invoke();
        //print("Click");
        


       



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
