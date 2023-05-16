using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGun : MonoBehaviour
{
    public GameObject canvas;
    public GameObject bullet;
    public GameObject bulletOrigin;
    public GameObject player;
    private enemyGun local;
    private float cooldown = 0f;
    private float shootCooldown = 1f;
    private buttons but;

    private float initialScaleY;

    void Start()
    {
        local = GetComponent<enemyGun>();
        local.canvas = GameObject.Find("Main Canvas");
        local.player = GameObject.Find("Stary");
        but = canvas.GetComponent<buttons>();
        initialScaleY = transform.localScale.y;
    }
    void Update()
    {
        if (!but.pause)
        {
            if (Time.time > cooldown)
            {
                cooldown = Time.time + shootCooldown + Random.Range(0f, 0.1f);
                Instantiate(bullet, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
            }
            Vector2 direction = player.transform.position - transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            transform.eulerAngles = new Vector3(0, 0, angle);
            if (angle < -90 && angle >= -180 || angle <= 180 && angle > 90)
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
