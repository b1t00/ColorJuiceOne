
using UnityEngine;

//Klasse die für den pointer zuständig ist, der auf obekte zeigt
public class PhysicsPointer : MonoBehaviour
{
    public float defaultLength = 25.0f;

    public GameObject sprayCan;  //SprayCan beim controller 

    private LineRenderer lineRenderer = null;

    private Color auswahl = Color.blue; //farbe, die gesprayt wird

    private bool isPlaying = false;

   // public Texture2D imageMap;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLength();

       // if(gameObject.transform.rotation.x > 50 || gameObject.transform.rotation.z > 50)
        //{
         //   FindObjectOfType<AudioManager>().Play("Shake");
        //}
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

            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Can")) //RIndexTrigger schaut nach Input vom Controller und es wird geschaut welches tag das getroffene Object besitzt
            {
                //var canRenderer = hit.collider.GetComponent<Renderer>();
                auswahl = hit.collider.gameObject.GetComponent<PointerEvent>().GetNormalColor();

                var spraycanRenderer = sprayCan.GetComponent<Renderer>();
                spraycanRenderer.material.SetColor("_Color", auswahl);

                FindObjectOfType<AudioManager>().Play("Shake");

            }
            else if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Menucan"))
            {
                hit.collider.gameObject.GetComponent<LoadOnClick>().NewScene();
            }
            else if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Farbrad"))
            {
                var farbradRenderer =  hit.collider.gameObject.GetComponent<Renderer>();
                Texture2D tex = (Texture2D) farbradRenderer.material.mainTexture; // Get texture of object under mouse pointer
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
            else if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Ghetto Blaster"))
            {
                //FindObjectOfType<AudioManager>().Play("Every Day");
                hit.collider.gameObject.GetComponent<AudioSource>().Play();

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
                if (isPlaying) { 
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
