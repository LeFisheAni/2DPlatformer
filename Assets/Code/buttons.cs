using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{
    public bool pause = false;
    public GameObject blur;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pause = !pause;
            blur.SetActive(true);
            if (!pause)
            {
                Time.timeScale = 1;
                blur.SetActive(false);
            }
        }
    }
}
