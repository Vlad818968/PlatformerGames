using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHashes : MonoBehaviour
{
    public int IsJump = Animator.StringToHash("IsJump");
    public int IsWalk = Animator.StringToHash("IsWalk");
    public int IsDead = Animator.StringToHash("IsDead");
    public int MushroomDead = Animator.StringToHash("IsDead");
}
