using UnityEngine;

public class RandomizeBehaviour : StateMachineBehaviour
{
    [SerializeField]
    [Tooltip("Time it takes for the character to play an alternative animation")]
    private float _timeTillSwitch;

    [SerializeField]
    [Tooltip("Total animations added to the blend tree")]
    private int _totalAnimations;

    [SerializeField]
    [Tooltip("Name of the variable that we trying to change the value of during runtime")]
    private string triggerVariable;

    private bool _needSwitching;
    private float _idleTime;
    private float _animationToPlay;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        ResetAnimation();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_needSwitching == false)
        {
            _idleTime += Time.deltaTime;

            if (_timeTillSwitch < _idleTime && stateInfo.normalizedTime % 1 < 0.02f)
            {
                _animationToPlay = Random.Range(1, _totalAnimations + 1);
                _animationToPlay = _animationToPlay * 2 - 1;
                _needSwitching = true;

                animator.SetFloat(triggerVariable, _animationToPlay - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetAnimation();
        }

        animator.SetFloat(triggerVariable, _animationToPlay, 0.4f, Time.deltaTime);
    }

    private void ResetAnimation()
    {
        if (_needSwitching)
        {
            _animationToPlay--;
        }
        _needSwitching = false;
        _idleTime = 0;
    }
}
