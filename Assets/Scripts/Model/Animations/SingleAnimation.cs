using Spine.Unity;

public class SingleAnimation
{
    private string _idleAnimationName;
    private string _attackAnimationName;
    private SkeletonGraphic _skeletonGraphic;
    private Spine.AnimationState _spineAnimationState;
    private int _track;

    public SingleAnimation(SkeletonGraphic skeletonGraphic, int track, string attackAnimationName)
    {
        _track = track;
        _skeletonGraphic = skeletonGraphic;
        _spineAnimationState = _skeletonGraphic.AnimationState;
        _attackAnimationName = attackAnimationName;
    }

    public void Play()
    {
        _spineAnimationState.SetAnimation(_track, _attackAnimationName, false);
    }
    public void Stop()
    {
        _spineAnimationState.ClearTrack(_track);
    }
}
