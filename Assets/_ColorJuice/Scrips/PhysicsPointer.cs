
using UnityEngine;

//Klasse die für den pointer zuständig ist, der auf obekte zeigt
public class PhysicsPointer : MonoBehaviour
{
    public float defaultLength = 25.0f;

    private Renderer sprayCan;  //SprayCan beim controller 

    private LineRenderer lineRenderer = null;

    private Color auswahl = Color.yellow; //farbe, die gesprayt wird

    private bool isPlaying = false;
    private bool musicOn = false;

    private bool outlinesBool = true;

    // public Texture2D imageMap;
    private GameObject farbrasterTemp;
    bool farbrasterToggle;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        sprayCan = GameObject.FindGameObjectWithTag("HandCan").GetComponent<Renderer>();
        farbrasterTemp = GameObject.FindGameObjectWithTag("FarbrasterTempo");
        farbrasterToggle = true;


    }

    private void Update()
    {
        UpdateLength();
        sprayCan.material.SetColor("_Color", auswahl);

        if (OVRInput.Get(OVRInput.Button.Back)) {
            if (!farbrasterToggle)
            {
                farbrasterToggle = true;
                farbrasterTemp.active = true;
            }
            else
            {
                farbrasterToggle = false;
                farbrasterTemp.SetActive(false);
            }

        } // Input möglichkeit für touch button

       
        
        //lineRenderer.SetColors(auswahl, buswahl);
        //lineRenderer.startColor = auswahl;
        lineRenderer.material.SetColor("_Color", auswahl);
        //lineRenderer.material.("_Color", auswahl);
        //lineRenderer.endColor = buswahl;
    }

    private void UpdateLength()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, CalculateEnd());
        
    }

    //Methode gibt den Punkt zurück an dem der Pointerstrahl auf ein physics object trifft
    //Außerdem wird hier abgefragt, auf welches object gezeigt wird 
    // hit ist das object welches getroffen wird. Über hit.collider.getKomponent<KompeneteDeinerWahl>() können Methoden auf den Objectkomponenten aufgerufen werden die angepeilt werden
    public Vector3 CalculateEnd()
    {
        RaycastHit hit = CreateForwarRaycast();
        Vector3 endPosition = DefauldEnd(defaultLength);

        if (hit.collider)
        {
            endPosition = hit.point;

            //if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) && (hit.collider.tag == "Can")) // Input möglichkeit für touch button



            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "BodenCan")) //RIndexTrigger schaut nach Input vom Controller und es wird geschaut welches tag das getroffene Object besitzt
            {
               
                auswahl = hit.collider.gameObject.GetComponent<PointerEventCansBoden>().GetNormalColor();
                FindObjectOfType<AudioManager>().Play("Shake");
            }

            if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.name == "Vorlage")){
                var outlines = GameObject.FindGameObjectWithTag("Outlines");
               
                if (outlinesBool)
                {
                    outlines.SetActive(false);
                    outlinesBool = false;
                } else
                {
                    outlines.active = true;
                    outlinesBool = true;
                }   
                
            }

            else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Farbkachel")) //RIndexTrigger schaut nach Input vom Controller und es wird geschaut welches tag das getroffene Object besitzt
            {

                auswahl = hit.collider.gameObject.GetComponent<PointerEventFarbkachel>().GetNormalColor();
                FindObjectOfType<AudioManager>().Play("Shake");
            }

            else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Can")) //RIndexTrigger schaut nach Input vom Controller und es wird geschaut welches tag das getroffene Object besitzt
            {
                //var canRenderer = hit.collider.GetComponent<Renderer>();
                auswahl = hit.collider.gameObject.GetComponent<PointerEventRegal>().GetNormalColor();

                //var spraycanRenderer = sprayCan.GetComponent<Renderer>();
                //spraycanRenderer.material.SetColor("_Color", auswahl);

                FindObjectOfType<AudioManager>().Play("Shake");

            }
            else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Menucan"))
            {
                hit.collider.gameObject.GetComponent<LoadOnClick>().NewScene();
            }
            else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Farbrad"))
            {
                var farbradRenderer = hit.collider.gameObject.GetComponent<Renderer>();
                Texture2D tex = (Texture2D)farbradRenderer.material.mainTexture; // Get texture of object under mouse pointer
                auswahl = tex.GetPixelBilinear(hit.textureCoord2.x, hit.textureCoord2.y); // Get color from texture
                auswahl.a = 255;

                var spraycanRenderer = sprayCan.GetComponent<Renderer>();
                spraycanRenderer.material.SetColor("_Color", auswahl);

                FindObjectOfType<AudioManager>().Play("Shake");

                /*Renderer farbradRenderer = hit.collider.gameObject.GetComponent<Renderer>();
                Texture2D texture = farbradRenderer.material.mainTexture as Texture2D;
                Vector2 pixelUV = hit.textureCoord;
                pixelUV.x *= texture.width;
                pixelUV.y *= texture.height;
                Vector2 tiling = farbradRenderer.material.mainTextureScale;
                auswahl = imageMap.GetPixel(Mathf.FloorToInt(pixelUV.x * tiling.x), Mathf.FloorToInt(pixelUV.y * tiling.y));*/
            }
            else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Ghetto Blaster") && OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
            {
                // spielt color juice hidden track
                FindObjectOfType<AudioManager>().Play("ColorJuiceTrack");
                MembranPulls[] mb = hit.collider.gameObject.GetComponentsInChildren<MembranPulls>();
                if (!musicOn)
                {
                    hit.collider.gameObject.GetComponent<AudioSource>().Play();
                    musicOn = true;
                    foreach (MembranPulls membran in mb)
                    {
                        membran.SetMusicOn(true);
                    }
                }
                else
                {
                    hit.collider.gameObject.GetComponent<AudioSource>().Stop();
                    musicOn = false;
                    foreach (MembranPulls membran in mb)
                    {
                        membran.SetMusicOn(false);
                    }
                }

            }
            else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Ghetto Blaster"))
            {
                //FindObjectOfType<AudioManager>().Play("Every Day");
                MembranPulls[] mb = hit.collider.gameObject.GetComponentsInChildren<MembranPulls>();
                if (!musicOn)
                {
                hit.collider.gameObject.GetComponent<AudioSource>().Play();
                    musicOn = true;
                    foreach(MembranPulls membran in mb)
                    {
                        membran.SetMusicOn(true);
                    }
                } else
                {
                    hit.collider.gameObject.GetComponent<AudioSource>().Stop();
                    musicOn = false;
                    foreach (MembranPulls membran in mb)
                    {
                        membran.SetMusicOn(false);
                    }
                }

            }
            
            else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
            {
                if (!isPlaying)
                {
                    FindObjectOfType<AudioManager>().Play("Burst");
                    isPlaying = true;
                }
            }
            else
            {
                if (isPlaying)
                {
                    FindObjectOfType<AudioManager>().Stop("Burst");
                    isPlaying = false;
                }
            }
        }
        else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            if (!isPlaying)
            {
                FindObjectOfType<AudioManager>().Play("Burst");
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying)
            {
                FindObjectOfType<AudioManager>().Stop("Burst");
                isPlaying = false;
            }
        }

        return endPosition;
    }

    public Color GetColorAuswahl()
    {
        return auswahl;
    }


    private RaycastHit CreateForwarRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray, out hit, defaultLength);
        return hit;
    }

    private Vector3 DefauldEnd(float length)
    {
        return transform.position + (transform.forward * length);
    }
}
