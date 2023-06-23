using Spine.Unity;

public class PunchAnimation
{
    private string _idleAnimationName;
    private string _attackAnimationName;
    private SkeletonGraphic _skeletonGraphic;
    private Spine.AnimationState _spineAnimationState;
    private int _track;

    public PunchAnimation(SkeletonGraphic skeletonGraphic, int track, string attackAnimationName, string idleAnimationName)
    {
        _track = track;
        _skeletonGraphic = skeletonGraphic;
        _spineAnimationState = _skeletonGraphic.AnimationState;
        _attackAnimationName = attackAnimationName;
        _idleAnimationName = idleAnimationName;
        _spineAnimationState.AddAnimation(_track, _idleAnimationName, true, 0);
        
    }
    public void Play()
    {
        _spineAnimationState.SetAnimation(_track, _attackAnimationName, false);
        _spineAnimationState.AddAnimation(_track, _idleAnimationName, true, 0);
    }
    public void Stop()
    {
        _spineAnimationState.ClearTrack(_track);
        _spineAnimationState.SetAnimation(_track, _idleAnimationName, true);
    }
}
