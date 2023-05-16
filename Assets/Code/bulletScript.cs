using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public GameObject player;
    public bulletScript local;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != 7)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        local = GetComponent<bulletScript>();
        local.player = GameObject.Find("Stary");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * 20 * Time.deltaTime);
        if (transform.position.x > player.transform.position.x + 25 || transform.position.x < player.transform.position.x - 25 || transform.position.y > player.transform.position.y + 20 || transform.position.y < player.transform.position.y - 20)
        {
            Destroy(gameObject);
        }
    }
}
