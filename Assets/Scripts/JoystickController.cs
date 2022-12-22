using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private Sprite LightJoystick;
    [SerializeField] private Sprite DarkJoystick;
    [SerializeField] private Sprite LightJoystickOuter;
    [SerializeField] private Sprite DarkJoystickOuter;

    [SerializeField] private Image Joystick;
    [SerializeField] private Image JoystickOuter;

    private Constants.WORLDSTATE currentWorldState;

    private void Start()
    {
        currentWorldState = StateManager.WORLDSTATE;
        StartCoroutine(SwitchJoysticks());
    }



    private IEnumerator SwitchJoysticks()
    {

        while (StateManager.PLAYERSTATE == Constants.PLAYERSTATE.ALIVE)
        {
            yield return new WaitUntil(() => currentWorldState != StateManager.WORLDSTATE);
            currentWorldState = StateManager.WORLDSTATE;
            if (currentWorldState == Constants.WORLDSTATE.BLACK)
            {
                Joystick.sprite = LightJoystick;
                JoystickOuter.sprite = LightJoystickOuter;
            } else
            {
                Joystick.sprite = DarkJoystick;
                JoystickOuter.sprite = DarkJoystickOuter;
            }
        }
    }
}
