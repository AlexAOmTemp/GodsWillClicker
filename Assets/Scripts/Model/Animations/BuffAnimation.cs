using UnityEngine;
using Spine.Unity;

public class BuffAnimation
{
    private string _animationName;
    private SkeletonGraphic _skeletonGraphic;
    private Spine.AnimationState _spineAnimationState;
    private int _track;

    public BuffAnimation(SkeletonGraphic skeletonGraphic, string animationName, int track)
    {
        _animationName = animationName;
        _skeletonGraphic = skeletonGraphic;
        _spineAnimationState = _skeletonGraphic.AnimationState;
        _track = track;
    }
    public void ActivateBuff(bool active)
    {
        if (active == true && _animationName != null)
            _spineAnimationState.SetAnimation(_track, _animationName, true);
        else
            _spineAnimationState.SetEmptyAnimation(_track, 0);
    }
}
