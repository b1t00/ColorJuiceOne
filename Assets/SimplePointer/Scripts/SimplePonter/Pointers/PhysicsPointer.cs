
using UnityEngine;

public class PhysicsPointer : MonoBehaviour
{
    public float defaultLength = 25.0f;

    public GameObject sprayCan;

    private LineRenderer lineRenderer = null;

    private Color auswahl = Color.blue;

    private bool isPlaying = false;

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

    public Vector3 CalculateEnd()
    {
        RaycastHit hit = CreateForwarRaycast();
        Vector3 endPosition = DefauldEnd(defaultLength);

        if (hit.collider)
        {
            endPosition = hit.point;

            //if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) && (hit.collider.tag == "Can"))
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && (hit.collider.tag == "Can"))
            {
                var canRenderer = hit.collider.GetComponent<Renderer>();
                auswahl = canRenderer.material.GetColor("_Color");

                var spraycanRenderer = sprayCan.GetComponent<Renderer>();
                spraycanRenderer.material.SetColor("_Color", auswahl);

                FindObjectOfType<AudioManager>().Play("Shake");

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
