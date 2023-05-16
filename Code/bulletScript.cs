using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public bool hit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * 20 * Time.deltaTime);
        /* if (transform.position.x > 25 || transform.position.x < -25 || transform.position.y > 10 || transform.position.y < -10)
        {
            Destroy(gameObject);
        } */
    }
    
}
