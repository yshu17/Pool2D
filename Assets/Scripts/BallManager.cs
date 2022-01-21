using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    Rigidbody2D rb;
    public DrawTrajectory reflectTrajectory;

    public float stopSpeed = 0.8f, speed;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
     
        speed = rb.velocity.magnitude;
        if (speed < stopSpeed) rb.velocity = Vector3.zero;
    }
    public void ShowRayc(RaycastHit2D hit)
    {
        Vector3 targetDir = (hit.transform.position - new Vector3(hit.centroid.x, hit.centroid.y, 0)).normalized * 500f;
        
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, targetDir, 500f, 1 << LayerMask.NameToLayer("Ball")|1 << LayerMask.NameToLayer("Ball"));
        if (hit1)
        {
            Debug.DrawRay(transform.position,targetDir,Color.cyan);
            reflectTrajectory.ShowTrajectory(hit1.point,targetDir);
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D col) => Destroy(gameObject);
}
