using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    bool _fadeout = false;

    private void Start()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private void OnEnable()
    {
        if (_fadeout)
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }

    IEnumerator FadeInCoroutine()
    {
        Debug.Log("FadeIn");
        float alpha = 1.0f;
        while (alpha != 0)
        {
            alpha -= Time.deltaTime;
            if (alpha <= 0)
            {
                alpha = 0;
            }
            GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        _fadeout = true;
        this.gameObject.SetActive(false);
        yield break;
    }

    IEnumerator FadeOutCoroutine()
    {
        Debug.Log("FadeOut");
        float alpha = 0.0f;
        while (alpha != 1)
        {
            alpha += Time.deltaTime;
            if (alpha >= 1)
            {
                alpha = 1;
            }
            GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        yield break;
    }
}
