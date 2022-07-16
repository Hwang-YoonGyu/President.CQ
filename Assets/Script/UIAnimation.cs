using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public static IEnumerator Bigger(GameObject panel)
    {
        panel.SetActive(true);
        RectTransform rect = panel.GetComponent<RectTransform>();
        float t = 0.0f;

        while (t <= 0.2f)
        {
            rect.localScale = new Vector3(5 * t, 5 * t, 5 * t);
            t += Time.deltaTime;
            yield return null;
        }

    }
    public static IEnumerator Smaller(GameObject panel)
    {
        RectTransform rect = panel.GetComponent<RectTransform>();
        float t = 0.2f;

        while (t >= 0.0f)
        {
            rect.localScale = new Vector3(5 * t, 5 * t, 5 * t);
            t -= Time.deltaTime;
            yield return null;
        }
        panel.SetActive(false);


    }
}
