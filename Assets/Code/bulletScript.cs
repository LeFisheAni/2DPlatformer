using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public bool hit = false;

    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        faceMouse();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            hit = true;
            Destroy(col.gameObject);
            hit = true;
            Destroy(gameObject);
            hit = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * 20 * Time.deltaTime);
        /* if (transform.position.x > 25 || transform.position.x < -25 || transform.position.y > 10 || transform.position.y < -10)
        {
            Destroy(gameObject);
        } */
    }
    
}
