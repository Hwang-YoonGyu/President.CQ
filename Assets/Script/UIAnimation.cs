using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public static IEnumerator Bigger(GameObject panel)
    {
        panel.SetActive(true);
        RectTransform rect = panel.GetComponent<RectTransform>();
        float t = 0.0f;

        while (t <= 0.1f)
        {
            rect.localScale = new Vector3(10 * t, 10 * t, 10 * t);
            t += Time.deltaTime;
            yield return null;
        }

    }
    public static IEnumerator Smaller(GameObject panel)
    {
        RectTransform rect = panel.GetComponent<RectTransform>();
        float t = 0.1f;

        while (t >= 0.0f)
        {
            rect.localScale = new Vector3(10 * t, 10 * t, 10 * t);
            t -= Time.deltaTime;
            yield return null;
        }
        panel.SetActive(false);


    }


    //밝아짐 
    public static IEnumerator fadeIn(GameObject panel)
    {

        for (float f = 0f; f < 1; f += 0.01f)
        {
            Color c = panel.GetComponent<Image>().color;
            //a == 투명도값 
            c.a = f;
            panel.GetComponent<Image>().color = c;
            yield return null;
        }


    }

    //어두워짐 
    public static IEnumerable fadeOut(GameObject panel)
    {

        for (float f = 1f; f > 0; f -= 0.01f)
        {
            Color c = panel.GetComponent<Image>().color;
            c.a = f;
            panel.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        panel.SetActive(false);


    }

    //카드이동 
    //public static IEnumerable moveCard(Vector3 StartPosition, Vector3 EndPosition)
    //{
    //    StartPosition = trans 
    //}
}
