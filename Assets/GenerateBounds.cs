using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Vector2[] points = coll.points;
            EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
            if (edge == null)
            {
                edge = gameObject.AddComponent<EdgeCollider2D>();
            }
            edge.points = points;
            Destroy(coll);
            Debug.Log(transform.localScale);
            return;
        }
        if (gameObject.name == "Circle")
        {
            Transform parent = GetComponentInParent<Transform>();
            gameObject.transform.localScale = parent.localScale * scale;
        }
    }

    private void Start()
    {
        
        //if (gameObject.name == "Circle")
        //{
        //    Transform parent = GetComponentInParent<Transform>();
        //    gameObject.transform.localScale = parent.localScale * scale;
        //}
    }
}
