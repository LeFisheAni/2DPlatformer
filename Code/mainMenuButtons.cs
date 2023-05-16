using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class mainMenuButtons : MonoBehaviour
{
    public Image fade;
    public GameObject fadeObj;
    public float speed;
    public void StartGame()
    {
        fadeObj.SetActive(true);
        StartCoroutine(FadeIn());
        
    }

    IEnumerator FadeIn()
    {
        float alpha = fade.color.a;
        while (fade.color.a < 254);
        {
            alpha += Time.fixedDeltaTime * speed;
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, alpha);
            yield return null;
        } 
        SceneManager.LoadScene("Level1");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
