using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SuriGrill : MonoBehaviour
{
    public GameObject suriLoc;
    private GameObject suriObj;
    public GameObject wrapOn;
    public GameObject grillOn;
    public GameObject grillOff;
    public HandleBar handleBar;

    public float grillTime = 5f;
    private AudioSource audioSource;
    [SerializeField] private ParticleSystem grillParticles;

    private void Start()
    {
        wrapOn.SetActive(false);
        grillOn.SetActive(false);
        grillOff.SetActive(true);
        handleBar.maxTime = grillTime;
        handleBar.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void Grill(GameObject suri)
    {
        if (suri == null)
            return;

        if (suriObj != null) return;

        if (!suri.GetComponent<Suri>())
            return;

        if (!suri.GetComponent<Suri>().wrapped)
            return;

        grillOn.SetActive(true);
        grillOff.SetActive(false);
        wrapOn.SetActive(true);
        audioSource.Play();
        grillParticles.Play();

        suriObj = suri;
        suriObj.transform.SetParent(suriLoc.transform);
        suriObj.transform.position = suriLoc.transform.position;
        suriObj.transform.rotation = suriLoc.transform.rotation;
        handleBar.Reset();
        handleBar.StartTimer();
        suriObj.SetActive(false);
        StartCoroutine(GrillCoroutine());
    }

    IEnumerator GrillCoroutine()
    {
        yield return new WaitForSeconds(grillTime);
        if (suriObj != null)
        {
            //Debug.Log("Suri has been grilled!");
            suriObj.GetComponent<Suri>().AddGrillMarks();
            grillOn.SetActive(false);
            grillOff.SetActive(true);
            wrapOn.SetActive(false);
            suriObj.SetActive(true);
            audioSource.Stop();
            grillParticles.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (suriObj != null)
        {
            if (suriObj.transform.parent != suriLoc.transform)
            {
                // The suri has been removed from the grill, stop grilling
                StopAllCoroutines();
                suriObj = null;
            }
        }
    }
}
