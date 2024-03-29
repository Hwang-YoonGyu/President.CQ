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

    /*
     오늘의 팁!
    - while 로 돌려서 Time.deltaTime으로 해준다 
    for로 반복문 돌리면 실행환경에따라 결과가 달라질슈이씀 ㅋㅋ 
     */

    //밝아짐 
    public static IEnumerator fadeIn(GameObject panel)
    {
        float t = 0.0f;
        Color c = panel.GetComponent<Image>().color;
        panel.SetActive(true);

        while (t <= 0.5f)
        {
            panel.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 2*t);
            t += Time.deltaTime;
            yield return null;

        }


    }

    //어두워짐 
    public static IEnumerator fadeOut(GameObject panel)
    {
        float t = 0.5f;
        Color c = panel.GetComponent<Image>().color;
        while (t >= 0.0f)
        {
            panel.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 2 * t);
            t -= Time.deltaTime;
            yield return null;
        }
        panel.SetActive(false);


    }

    //카드이동 start end 고정
    public static IEnumerator moveCard(GameObject gameObject, Vector3 start ,Vector3 end)
    {
        float t = 0.0f;

        while (t <= 0.5f)
        {
            gameObject.transform.position = Vector3.Lerp(start, end, t*2);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
