using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    Rigidbody2D rb;
    float ballRad;
    public DrawTrajectory reflectTrajectory;


    public float stopSpeed = 0.8f, speed;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        ballRad= GetComponent<CircleCollider2D>().radius;
    }
    private void Update() {
     
        speed = rb.velocity.magnitude;
        if (speed < stopSpeed) rb.velocity = Vector3.zero;
    }
    public void ShowRayc(RaycastHit2D hit)
    {  
        Vector2 targetDir = ((Vector2)hit.transform.position - new Vector2(hit.centroid.x, hit.centroid.y)).normalized * 100f;     
        RaycastHit2D hit1 = Physics2D.CircleCast(hit.transform.position,ballRad, targetDir, 50f, 1 << LayerMask.NameToLayer("Table"));
        if (hit1.collider != null)
        {
            Debug.DrawRay(transform.position,targetDir,Color.cyan);
            reflectTrajectory.ShowTrajectory(hit1.centroid,transform.position);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D col) => Destroy(gameObject);
}
