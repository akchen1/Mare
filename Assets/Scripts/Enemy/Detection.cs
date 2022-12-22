using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    EnemyController parent;

    // The number of rays to cast in the cone
    public int rayCount = 3;
    // The angle of the cone, in degrees
    public float coneAngle = 30f;
    // The distance of the cone
    public float coneDistance = 5f;
    // The layer mask for the raycast
    public LayerMask layerMask;

    public float scanDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<EnemyController>();
        StartCoroutine(DetectionRaycast());
    }

    private IEnumerator DetectionRaycast()
    {
        while (StateManager.PLAYERSTATE == Constants.PLAYERSTATE.ALIVE)
        {
            // Calculate the step angle of the rays in the cone
            float stepAngle = coneAngle / rayCount;

            // Create an array to store the hit results
            RaycastHit2D[] hits = new RaycastHit2D[rayCount];

            // Cast the rays in the cone
            bool detected = false;
            for (int i = 0; i < rayCount; i++)
            {
                // Calculate the angle of the ray
                float angle = transform.eulerAngles.z - coneAngle / 2 + stepAngle * i;
                // Calculate the direction of the ray
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                // Cast the ray and store the hit result
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, coneDistance, layerMask);
                //Debug.DrawLine(transform.position, transform.position + (Vector3)direction * coneDistance, Color.red);

                if (hit.collider != null)
                {
                    detected = true;
                    break;
                }
            }
            if (detected)
            {
                parent.setSpeed(0);
                parent.inRange = true;
            } else
            {
                parent.setSpeed(1);
                parent.inRange = false;
            }
            yield return new WaitForSeconds(scanDelay);
            
        }
        
    }
}
