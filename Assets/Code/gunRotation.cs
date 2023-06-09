using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunRotation : MonoBehaviour
{
    public GameObject followPlayer;
    public GameObject canvas;
    public GameObject bullet;
    public GameObject bulletOrigin;
    private float shoot;
    private float shootCooldown=0.4f;
    private buttons but;

    private float initialScaleY;

    void Start()
    {
        but = canvas.GetComponent<buttons>();
        initialScaleY = transform.localScale.y;
    }
    void Update()
    {
        if (!but.pause)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > shoot)
            {
                shoot = Time.time + shootCooldown;
                Instantiate(bullet, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
            }
            transform.position = new Vector3 (followPlayer.transform.position.x, followPlayer.transform.position.y, transform.position.z);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            transform.eulerAngles = new Vector3(0, 0, angle);
            if(angle<-90 && angle>=-180 || angle<=180 && angle > 90)
            {
                Vector3 newScale = transform.localScale;
                newScale.y = -initialScaleY;
                transform.localScale = newScale;
            }
            else
            {
                Vector3 newScale = transform.localScale;
                newScale.y = initialScaleY;
                transform.localScale = newScale;
            }
        }
    }
}
