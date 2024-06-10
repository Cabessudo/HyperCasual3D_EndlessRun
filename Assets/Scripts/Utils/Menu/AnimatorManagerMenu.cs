using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManagerMenu : MonoBehaviour
{
    public Animator anim;
    public List<AnimatorSetup> animatorSetups;
    public float animSpeed = 7;

    public enum AnimStyle
    {
        Idle01,
        Idle02,
        Idle03,
        Watch
    }

    public void Play(AnimStyle newStyle, float speed = 7, bool b = true)
    {
        foreach(var animator in animatorSetups)
        {
            if(animator.animStyle == newStyle)
            {
                anim.SetTrigger(animator.trigger);
                anim.SetBool(animator.trigger, b);
                anim.speed = speed / animSpeed;
                break;
            }
        }
    }

    [System.Serializable]
    public class AnimatorSetup
    {
        public AnimStyle animStyle;
        public string trigger;
    } 
}
