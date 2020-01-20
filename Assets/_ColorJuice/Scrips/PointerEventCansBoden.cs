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
    //[SerializeField] private Color enterColor;  // Farbe die angezeigt wird, wenn man mit pointer über object geht
    //[SerializeField] private Color downColor; // Farbe die angezeigt wird, wenn man bestätigt (pointer down)
    [SerializeField] private UnityEvent OnClick = new UnityEvent();

    private GameObject can; //sprühdose/object das mit bei der Farbauswahl auswählt

    private MeshRenderer meshRenderer = null;

    private Vector3 locPos;

    private void Awake()
    {
        //can = GameObject.FindGameObjectWithTag("HandCan");
        meshRenderer = GetComponent<MeshRenderer>();
        Debug.Log(meshRenderer.name);

        var chooseRenderer = gameObject.GetComponent<Renderer>();
        chooseRenderer.material.SetColor("_Color", normalColor); // Beim Laden wird dem Object die Farbe normalColor zugewiesen
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //meshRenderer.material.color = enterColor;
        print("Enter");

        locPos.y = -1f;
        gameObject.transform.localPosition = locPos;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        meshRenderer.material.color = normalColor;
        print("Exit");
        locPos.y = -1.64f;
        gameObject.transform.localPosition = locPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //meshRenderer.material.color = downColor;
        print("Down");
        locPos.y = -1f;
        gameObject.transform.localPosition = locPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //  meshRenderer.material.color = enterColor;
        print("Up");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
        print("Click");
        //var canRenderer = can.GetComponent<Renderer>();
        //canRenderer.material.SetColor("_Color", normalColor);
        
    }

    public Color GetNormalColor() //public methode um farbe zu übergeben. Wichtig für die Farbauswahl
    {
        return normalColor;
    }
}
