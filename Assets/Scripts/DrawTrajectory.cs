using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    private LineRenderer lrc;
    private void Awake() => lrc = GetComponent<LineRenderer>();   
    public void ShowTrajectory(Vector2 to,Vector2 from)
    {
        Vector3[]points =new Vector3[2];
        lrc.positionCount= points.Length;
        points[0] = to;
        points[1] = from;
        lrc.SetPositions(points);
    }
    public void EndLine() => lrc.positionCount=0;
}
