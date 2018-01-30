using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    [SerializeField] private int terrainRange = 20;
    [SerializeField] private int pointsPerMeter = 4;

	// Use this for initialization
	void Start () {
        CreatePoints();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreatePoints()
    {
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();        
        List<Vector2> points = new List<Vector2>();

        for (int i = -terrainRange; i <= terrainRange; i++)
        {
            if(i >= terrainRange)
            {
                points.Add(new Vector2(i, 0));
                Debug.Log(i);
                continue;
            }

            for (int j = 0; j < pointsPerMeter; j++)
            {
                points.Add(new Vector2(i + Mathf.Lerp(0.0f, 1.0f, (float)j / (float)pointsPerMeter), 0));
            }
        }

        Vector2[] newPoints = new Vector2[points.Count];

        for (int i = 0; i < points.Count; i++)
        {
            newPoints[i] = points[i];
        }

        collider.points = newPoints;
    }

    private void OnDrawGizmos()
    {
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
        Vector2[] points = collider.points;

        for (int i = 0; i < points.Length; i++)
        {
            if (i < points.Length - 1)
                Gizmos.DrawLine(new Vector3(points[i].x, points[i].y, 0), new Vector3(points[i + 1].x, points[i + 1].y, 0));

            Gizmos.DrawSphere(new Vector3(points[i].x, points[i].y, 0), 0.2f);
        }
    }

    public void DestroyAt(Vector2 point, float range, float damage)
    {
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[collider.pointCount];
        int index = 0;

        for (int i = 0; i < collider.points.Length; i++)
        {
            Vector2 curr = collider.points[i];
            Vector2 next = new Vector2(0, 0);

            if(i < collider.points.Length - 1)
            {
                next = collider.points[i + 1];

                if(point.x >= curr.x && point.x <= next.x)
                {
                    index = i;
                }
            }
            else
            {
                if(point.x >= curr.x )
                {
                    index = i;
                }
            }
            
        }

        float maxDepth = damage;
        float falloff = .99f;
        float newRange = range * pointsPerMeter;   // in meters, times pointsPerMeter
        int iterator = 0;

        points = collider.points;

        points[index].y -= maxDepth;
        iterator++;

        Debug.Log(newRange + " - range");
        for (int i = 0; i < newRange; i++)
        {
            maxDepth *= falloff;

            if (index + iterator < points.Length)
                points[index + iterator].y -= maxDepth;

            if (index - iterator > 0)
                points[index - iterator].y -= maxDepth;

            Debug.Log(iterator + " - iterator");
            iterator++;
        }

        collider.points = points;
    }
}
