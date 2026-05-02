using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandleBar : MonoBehaviour
{
    private float barWidth;

    public enum BarType { Accending, Decending }
    public BarType bar;
    public GameObject greenBar;
    private RectTransform greenBarRectTransform;
    private Color warningColor = Color.red;

    public float maxTime = 5f;
    public float tickTime = 0.1f;


    void Start()
    {
        greenBarRectTransform = greenBar.GetComponent<RectTransform>();
        barWidth = 200;
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        if (bar == BarType.Accending)
        {
            greenBarRectTransform.sizeDelta = new Vector2(0, greenBarRectTransform.sizeDelta.y);
        }
        else if (bar == BarType.Decending)
        {
            greenBarRectTransform.sizeDelta = new Vector2(barWidth, greenBarRectTransform.sizeDelta.y);
        }
        greenBar.GetComponent<Image>().color = Color.green;
    }

    public void StartTimer()
    {
        if (bar == BarType.Accending)
        {
            StartCoroutine(AccendTimer());
        }
        else if (bar == BarType.Decending)
        {
            StartCoroutine(DecendTimer());
        }
    }

    IEnumerator AccendTimer()
    {
        for (float i = 0; i < maxTime; i += tickTime)
        {
            float newWidth = (i / maxTime) * barWidth;
            greenBarRectTransform.sizeDelta = new Vector2(newWidth, greenBarRectTransform.sizeDelta.y);
            yield return new WaitForSeconds(tickTime);
        }
        gameObject.SetActive(false);
    }

    IEnumerator DecendTimer()
    {
        bool warningTriggered = false;
        for (float i = maxTime; i >= 0; i -= tickTime)
        {
            float newWidth = (i / maxTime) * barWidth;
            greenBarRectTransform.sizeDelta = new Vector2(newWidth, greenBarRectTransform.sizeDelta.y);
            if (i <= maxTime/5 && !warningTriggered)
            {
                warningTriggered = true;
                StartCoroutine(LerpColor(greenBar.GetComponent<Image>(), Color.green, warningColor, 0.5f));
            }
            yield return new WaitForSeconds(tickTime);
        }
        gameObject.SetActive(false);
    }

    IEnumerator LerpColor(Image image,Color startColor, Color endColor, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            image.color = Color.Lerp(startColor, endColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        greenBar.GetComponent<Image>().color = endColor;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
