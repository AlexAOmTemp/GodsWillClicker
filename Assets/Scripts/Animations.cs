using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Animations : MonoBehaviour
{
    #region Inspector
    // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
    [Header("Actions")]
    [SpineAnimation][SerializeField] private string _idleAnimationName;
    [SpineAnimation][SerializeField] private string _attackAnimationName;

    [Header("Buffs")]
    [SpineAnimation][SerializeField] private string _armorAnimationName;
    [SpineAnimation][SerializeField] private string _swordAnimationName;
    [SpineAnimation][SerializeField] private string _wingsAnimationName;
    [SpineAnimation][SerializeField] private string _nimbusAnimationName;
    [SpineAnimation][SerializeField] private string _wrathAnimationName;
    #endregion

    #region Private variables
    private List<string> _animationNames = new List<string>();
    private SkeletonAnimation _skeletonAnimation;
    private Spine.AnimationState _spineAnimationState;
    private Spine.Skeleton _skeleton;
    #endregion

    void Awake()
    {
        _skeletonAnimation = this.GetComponent<SkeletonAnimation>();
        _spineAnimationState = _skeletonAnimation.AnimationState;
        _skeleton = _skeletonAnimation.Skeleton;
        collectAnimationNames();
        _spineAnimationState.AddAnimation(0, _idleAnimationName, true, 0);
    }
    public void Attack()
    {
        _spineAnimationState.SetAnimation(0, _attackAnimationName, false);
        _spineAnimationState.AddAnimation(0, _idleAnimationName, true, 0);
    }
    public void ActivateBuff(Buffs buff, bool active)
    {
        int n = (int)buff;
        Debug.Log($"{n} {_animationNames.Count}");
        
        if (_animationNames[n - 1] != null)
        {
            if (active == true)
                _spineAnimationState.SetAnimation(n, _animationNames[n - 1], true);
            else
                _spineAnimationState.SetEmptyAnimation(n, 0);
        }
    }
    private void collectAnimationNames()
    {
        _animationNames.Clear();
        _animationNames.Add(_armorAnimationName);
        _animationNames.Add(_swordAnimationName);
        _animationNames.Add(_wingsAnimationName);
        _animationNames.Add(_nimbusAnimationName);
        _animationNames.Add(_wrathAnimationName);
    }
}
