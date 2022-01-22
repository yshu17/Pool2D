using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragShoot : MonoBehaviour
{
    #region Global
    public float power = 15f, stopSpeed = 0.8f, speed, ballRad;
    public Rigidbody2D rb;
    public DrawTrajectory trajectoryLine,reflectLine,powerLine,reflectMainBall;
    Vector2 minPower =new Vector2(-1,-1), maxPower = new Vector2(1,1), force, cursorPos; 
    Vector3 startPos, endPos;
    Camera cam;
    BallManager bm;
    #endregion

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        ballRad= GetComponent<CircleCollider2D>().radius;     
    }
    private void Update()
    {   
        GameObject[] objectss = GameObject.FindGameObjectsWithTag("Ball");
        if(objectss.Length==0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        speed = rb.velocity.magnitude;
        cursorPos = cam.ScreenToWorldPoint(Input.mousePosition);
        var dir = (cursorPos - (Vector2)transform.position).normalized * -10f;
        
        BallLogic(cursorPos,dir);
    }
    private void BallLogic(Vector2 cursorPos,Vector2 dir)
    {
        int BallsAndWallsMask = 1 << LayerMask.NameToLayer("Table") | 1 << LayerMask.NameToLayer("Ball");
        if (speed < stopSpeed) rb.velocity = Vector3.zero;
        if (speed == 0)
        {
            startPos = transform.position;
            if (Input.GetMouseButton(0))
            {
                RaycastHit2D hit = Physics2D.CircleCast(transform.position, ballRad, dir, 50f, BallsAndWallsMask);
                if (hit.collider != null)
                {
                    Debug.DrawRay(transform.position,dir,Color.magenta);
                    trajectoryLine.ShowTrajectory(hit.centroid, transform.position);
                    if (hit.collider.CompareTag("Ball"))
                    {                       
                        Vector2 targetDir = ((Vector2)hit.transform.position - new Vector2(hit.centroid.x, hit.centroid.y)).normalized * 100f;
                        bm = hit.transform.gameObject.GetComponent<BallManager>();
                        if(bm) bm.ShowRayc(hit);
                    }
                    else                                    //???
                    {
                        Vector2 refl = Vector2.Reflect((rb.velocity -(Vector2)transform.position),hit.normal);
                        reflectMainBall.ShowTrajectory(refl,hit.centroid);
                    }
                    
                }
                powerLine.ShowTrajectory(startPos, cursorPos);
            }

            if (Input.GetMouseButtonUp(0))
            {
                endPos = cam.ScreenToWorldPoint(Input.mousePosition);
                force = new Vector2(startPos.x - endPos.x, startPos.y - endPos.y);
                rb.AddForce(force * power, ForceMode2D.Impulse);
                powerLine.EndLine();
                trajectoryLine.EndLine();
                reflectLine.EndLine();
                bm.reflectTrajectory.EndLine();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)=> SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
}
