using System;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Components
{
    public sealed partial class AnimatorStateComponent : IHaveActor, IInitAfterView, IDisposable
    {
        public Actor Actor { get; set; }
        [HideInInspectorCrossPlatform]
        public Animator Animator;
        [HideInInspectorCrossPlatform]
        public bool Activated;

        public override void Init()
        {
            if (Owner.ContainsMask<ViewReferenceGameObjectComponent>() || Owner.ContainsMask<ViewDependencyTagComponent>())
                return;

            SetupAnimatorState();
        }

        public void SetupAnimatorState()
        {
            if (Owner.TryGetComponent(out ViewReadyTagComponent viewReadyTagComponent))
            {
                Animator =  viewReadyTagComponent.View.GetComponentInChildren<Animator>();
            }
            else
                Actor.TryGetComponent(out Animator, true);

            if (Animator == null)
            {
                Debug.LogError($"we have problems with animator on {Actor}  with container = {Actor.ContainerID}");
                return;
            }

            State.SetAnimator(Animator);
            Activated = true;
            ResetAnimator();
        }

        public void SetTrigger(int id)
        {
            Animator.SetTrigger(id);
        }

        public void ResetAnimator()
        {
            Animator.enabled = true;
            Animator.Rebind();
            Animator.Update(0);
        }

        public void InitAfterView()
        {
            SetupAnimatorState();
        }

        public async UniTask WaitStateComplete(int layer = 0)
        {
            var currentState = Animator.GetCurrentAnimatorStateInfo(layer).shortNameHash;
            var owner = Owner.GetAliveEntity();

            while (owner.IsAlive)
            {
                var state = Animator.GetCurrentAnimatorStateInfo(layer);

                if (state.normalizedTime >= 1)
                    break;

                if (state.shortNameHash != currentState)
                    break;

                await UniTask.DelayFrame(1);
            }
        }

        public void Reset()
        {
            Activated = false;
            Animator = null;
        }

        public void Dispose()
        {
            Reset();
        }
    }
}