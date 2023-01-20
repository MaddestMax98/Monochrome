using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoredBehaviour : StateMachineBehaviour
{
    [SerializeField]
    [Tooltip("Time it takes for the character to play a bored animation")]
    private float _patience;

    [SerializeField]
    [Tooltip("Total animations added to the blend tree")]
    private int _totalAnimations;

    private bool _isBored;
    private float _idleTime;
    private float _animationToPlay;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetAnimation();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isBored == false)
        {
            _idleTime += Time.deltaTime;

            if (_patience < _idleTime && stateInfo.normalizedTime % 1 < 0.02f)
            {
                _animationToPlay = Random.Range(1, _totalAnimations + 1);
                _animationToPlay = _animationToPlay * 2 - 1;
                _isBored = true;

                animator.SetFloat("CurrentIdleAnimation", _animationToPlay - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetAnimation();
        }

        animator.SetFloat("CurrentIdleAnimation", _animationToPlay, 0.4f, Time.deltaTime);
    }

    private void ResetAnimation()
    {
        if (_isBored)
        {
            _animationToPlay--;
        }
        _isBored = false;
        _idleTime = 0;
    }
}
