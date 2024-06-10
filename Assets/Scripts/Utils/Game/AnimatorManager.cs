using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator anim;
    public AnimatorSetup[] animatorSetups;
    public float animSpeed = 7;

    public enum AnimStyle
    {
        Idle,
        Run,
        Dead,
        Dead_02,
        JumpFly,
        Fly
    }

    public void Play(AnimStyle newStyle, float speed = 7, bool b = true)
    {
        foreach(var animator in animatorSetups)
        {
            if(animator.animStyle == newStyle)
            {
                anim.SetTrigger(animator.trigger);
                anim.SetBool(animator.trigger, b);
                
                break;
            }
        }
    }

    public void CheckAnimVelocity(float speed = 0)
    {
        anim.speed = speed / animSpeed;
    }

    [System.Serializable]
    public class AnimatorSetup
    {
        public AnimStyle animStyle;
        public string trigger;
    } 
}
