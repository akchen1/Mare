using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    public EnemyManager eScript;

    // 0 = dark, 1 = light
    public static Constants.WORLDSTATE WORLDSTATE;
    //public static int worldState;
    public float cooldown;
    private float timer;

    public Camera cam;
    public GameObject lights;

    public AudioScript aScript;
    public InGameUIScript uiScript;

    private PlayerInputActions inputActions;
    public static Constants.PLAYERSTATE PLAYERSTATE;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        WORLDSTATE = Constants.WORLDSTATE.WHITE;
        cam.backgroundColor = Color.white;
        lights.SetActive(false);

        timer = cooldown;
        PLAYERSTATE = Constants.PLAYERSTATE.ALIVE;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.World.Switch.performed += OnSwitchWorlds;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.World.Switch.performed -= OnSwitchWorlds;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
    }

    public void OnSwitchWorlds(InputAction.CallbackContext context)
    {
        if (timer > 0 || PLAYERSTATE == Constants.PLAYERSTATE.DEAD) return;
        SwitchWorlds();
        uiScript.Cast();
        timer = cooldown;
    }

    public void SwitchWorlds()
    {
        aScript.SwitchTracks();
        aScript.PlayPhaseSwitch();

        if (WORLDSTATE == Constants.WORLDSTATE.BLACK)
        {
            // Switch to light
            WORLDSTATE = Constants.WORLDSTATE.WHITE;
            cam.backgroundColor = Color.white;
            lights.SetActive(false);
        }

        else if (WORLDSTATE == Constants.WORLDSTATE.WHITE)
        {
            // Switch to dark
            WORLDSTATE = Constants.WORLDSTATE.BLACK;
            cam.backgroundColor = Color.black;
            lights.SetActive(true);
        }
        eScript.ActivateEnemies();
        eScript.DeactivateEnemies();

        AstarPath.active.Scan();
    }
}
