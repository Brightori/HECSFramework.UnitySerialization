using System.Collections.Generic;
using UnityEngine;

namespace HECSFramework.Serialize
{
    public partial class AnimatorState
    {
        private Animator animator;

        public void SetAnimator(Animator animator)
        {
            this.animator = animator;
        }

        partial void SetBoolUnityPart(int id, bool value)
        {
            animator.SetBool(id, value);
        }

        partial void SetFloatUnityPart(int id, float value, float deltaTime, float damp)
        {
            animator.SetFloat(id, value, damp, deltaTime);
        }
    }
}
