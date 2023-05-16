using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTransition : MonoBehaviour
{
    public GameObject cam;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if(player.CompareTag("Player") && !player.isTrigger)
        {
            cam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player") && !player.isTrigger)
        {
            cam.SetActive(false);
        }
    }
}
