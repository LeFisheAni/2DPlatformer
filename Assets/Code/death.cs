using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class death : MonoBehaviour
{
    public GameObject yippieDeath;
    public buttons but;
    public GameObject canvas;

    public void Continue()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }
    void Start()
    {
        but = canvas.GetComponent<buttons>();
    }
    void Update()
    {
        if (GameObject.Find("Stary") == null)
        {
            yippieDeath.SetActive(true);
            but.pause = true;
            Time.timeScale = 0;
        }
    }
}