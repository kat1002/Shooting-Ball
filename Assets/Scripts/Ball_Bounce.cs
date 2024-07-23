using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball_Bounce : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 lastVelocity;
    //[SerializeField] float speed;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collObject = collision.gameObject;

        if (collObject.CompareTag("Bottom")) {
            Destroy(gameObject);

            SceneScripts sc = GameObject.FindGameObjectWithTag("PlayGround").GetComponent<SceneScripts>();
            sc.Des += 1;
            return;
        }
        
        if (collObject.CompareTag("Obstacle"))
        {
            Obstacle obs = collObject.GetComponent<Obstacle>();
            obs.takeDamage();
        }

        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed, 0f);

        //rb.AddForce(direction * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collObject = collision.gameObject;
        if (collObject.CompareTag("Plus"))
        {
            PlusBall pb = collObject.GetComponent<PlusBall>();
            pb.AddBall();
        }
    }
}
