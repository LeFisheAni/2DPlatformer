using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float constspeed = 10f;
    public float jump = 5000f;
    public float secondsTimer = 0.1f;
    public float dashTime = 0.1f;
    public float dashImpulse = 10f;

    public bool ver = true;
    public bool ground = true;
    public bool dash = true;
    public bool dashing = false;
    private bool cancelDash = false;

    private Rigidbody2D rb;
    private Vector2 vx;
    private TrailRenderer tr;
    private float hor;
    private float verti;
    private float timeCooldown;
    private float gravSupremeCalamity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        gravSupremeCalamity = rb.gravityScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ground = true;
        cancelDash = true;
        if (dashing)
        {
            dashing = false;
            rb.gravityScale = gravSupremeCalamity;
            tr.emitting = false;
            Vector3 vx = rb.velocity;
            if (ver)
            {
                vx.y = 0f;
            }
            vx.x = 0f;
            rb.velocity = vx;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        dash = true;
    }
    private IEnumerator DashingSwag()
    {
        cancelDash = false;
        Vector2 vx = rb.velocity;
        if (ver)
        {
            vx.y = 0f;
        }
        vx.x = 0f;
        rb.velocity = vx;
        rb.gravityScale = 0;
        dash = false;
        dashing = true;
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        dashing = false;
        rb.gravityScale = gravSupremeCalamity;
        tr.emitting = false;
        if (!cancelDash)
        {
            dashing = false;
            rb.gravityScale = gravSupremeCalamity;
            tr.emitting = false;
            vx = rb.velocity;
            if (ver)
            {
                vx.y = 0f;
            }
            vx.x = 0f;
            rb.velocity = vx;
        }
    }
    private void FixedUpdate()
    {
        if (dashing)
        {
            return;
        }

        rb.velocity = new Vector2(hor * speed, rb.velocity.y);
    }
    void Update()
    {
        if (dashing)
        {
            return;
        }

        hor = Input.GetAxis("Horizontal");
        verti = Input.GetAxis("Vertical");
        ver = Input.GetKey(KeyCode.W);

        if (Input.GetKey(KeyCode.W) && ground == true)
        {
            Vector2 vx = rb.velocity;
            vx.y = 0f;
            rb.velocity = vx;
            rb.AddForce(Vector2.up * jump);
            ground = false;
            timeCooldown = Time.time + secondsTimer;
        }

        if (Input.GetKeyDown(KeyCode.Space) && dash == true && Time.time > timeCooldown)
        {
            if (hor < 0)
            {
                StartCoroutine(DashingSwag());
                rb.velocity = new Vector2(-1 * dashImpulse * Time.fixedDeltaTime, System.Convert.ToUInt32(ver) * dashImpulse * Time.fixedDeltaTime);
            }
            if (hor > 0)
            {
                StartCoroutine(DashingSwag());
                rb.velocity = new Vector2(dashImpulse * Time.fixedDeltaTime, System.Convert.ToUInt32(ver) * dashImpulse * Time.fixedDeltaTime);

            }
            if (hor == 0)
            {
                StartCoroutine(DashingSwag());
                rb.velocity = new Vector2(0, System.Convert.ToUInt32(ver) * dashImpulse * Time.fixedDeltaTime);
            }
        }
    }
}