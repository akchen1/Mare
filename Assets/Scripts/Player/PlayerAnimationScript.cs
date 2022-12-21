using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerAnimationScript : MonoBehaviour
{

    Rigidbody2D rbody;
    Animator animator;

    public InGameUIScript iScript;
    public EnemyManager eScript;
    public PolygonCollider2D poly;
    public Camera cam;
    public PlayerLightsScript lScript;

    private bool zooming;
    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        poly = GetComponent<PolygonCollider2D>();
        lScript = GetComponent<PlayerLightsScript>();

        zooming = false;
        x = 0f;
        y = 0f;
        animator.SetBool("IsDead", false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (zooming)
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, 2f, 2f * Time.deltaTime);

            x = Mathf.MoveTowards(x, transform.position.x + 0.02f, 0.01f * Time.deltaTime);
            y = Mathf.MoveTowards(y, transform.position.y - 0.47f, 0.235f / 1.5f * 2 * Time.deltaTime);
            cam.transform.position = new Vector3(x, y, -10);
        }

        if (StateManager.PLAYERSTATE == Constants.PLAYERSTATE.DEAD)
        {
            return;
        }

        if (rbody.velocity.magnitude > 0)
        {
            animator.SetFloat("Horizontal", rbody.velocity.x);
            animator.SetFloat("Vertical", rbody.velocity.y);
        }
        animator.SetBool("IsMoving", rbody.velocity.magnitude > 0);
        animator.SetBool("DarkState", StateManager.WORLDSTATE == Constants.WORLDSTATE.BLACK);
    }

    public void Dead()
    {
        animator.SetBool("IsDead", true);
        rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        poly.enabled = false;
        StateManager.PLAYERSTATE = Constants.PLAYERSTATE.DEAD;
        iScript.Hide();
        lScript.NoLight();
        x = transform.position.x;
        y = transform.position.y;
        zooming = true;

        if (StateManager.WORLDSTATE == Constants.WORLDSTATE.BLACK)
        {
            StateManager.WORLDSTATE = Constants.WORLDSTATE.WHITE;
            iScript.Swap();
            gameObject.GetComponentInChildren<Camera>().backgroundColor = Color.white;
        }

        iScript.Show();
        
    }
}
