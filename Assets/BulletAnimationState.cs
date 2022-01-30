using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimationState : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Bullet bullet = animator.gameObject.GetComponent<Bullet>();
        bullet.initate_finish = true;
    }
}
