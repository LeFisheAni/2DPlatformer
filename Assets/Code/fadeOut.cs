using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeOut : MonoBehaviour
{
    public Image fade;
    public GameObject fadeObj;
    public float speed;
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float alpha = fade.color.a;
        while (fade.color.a > 0)
        {
            alpha -= Time.fixedDeltaTime * speed;
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, alpha);
            yield return null;
        }
        fadeObj.SetActive(false);
    }
}
