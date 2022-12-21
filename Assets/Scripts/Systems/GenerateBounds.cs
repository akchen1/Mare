using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateBounds : MonoBehaviour
{
    public float scale;
    private void Awake()
    {

        if (gameObject.tag == "Bounds")
        {
            Physics2D.IgnoreLayerCollision(10, 11);
            PolygonCollider2D coll = GetComponent<PolygonCollider2D>();
            if (coll == null)
            {
                coll = gameObject.AddComponent<PolygonCollider2D>();
            }
            List<Vector2> points = coll.points.ToList();
            EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
            if (edge == null)
            {
                edge = gameObject.AddComponent<EdgeCollider2D>();
            }
            points.Add(points[0]);
            edge.points = points.ToArray();
            Destroy(coll);
            return;
        }
        if (gameObject.name == "Circle")
        {
            Transform parent = GetComponentInParent<Transform>();
            gameObject.transform.localScale = parent.localScale * scale;
        }
    }
}
