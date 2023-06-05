using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Animations : MonoBehaviour
{
    #region Inspector
    // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
    [Header("Actions")]
    [SpineAnimation] [SerializeField] private string _idleAnimationName;
    [SpineAnimation] [SerializeField] private string _attackAnimationName;
    [SpineAnimation] [SerializeField] private string _wrathAttackAnimationName;
    [SpineAnimation] [SerializeField] private string _critAttackAnimationName;
    [SpineEvent(dataField: "skeletonGraphic", fallbackToTextField: true)] public string _eventName;

    [Header("Buffs")]
    [SpineAnimation] [SerializeField] private string _armorAnimationName;
    [SpineAnimation] [SerializeField] private string _swordAnimationName;
    [SpineAnimation] [SerializeField] private string _wingsAnimationName;
    [SpineAnimation] [SerializeField] private string _nimbusAnimationName;

    [Header("Reactions")]
    [SpineAnimation] [SerializeField] private string _getHitFlesh;
    [SpineAnimation] [SerializeField] private string _getHitArmor;
    [SpineAnimation] [SerializeField] private string _death;
    #endregion

    public delegate void punchCallback();

    #region Private variables
    private SkeletonGraphic _skeletonGraphic;
    private SkeletonAnimation _skeletonAnimation;
    private Dictionary<ItemNames, BuffAnimation> _buffAnimations = new Dictionary<ItemNames, BuffAnimation>();
    private PunchAnimation _punchAnimation;
    private PunchAnimation _critAnimation;
    private PunchAnimation _wrathAnimation;
    private SingleAnimation _getFleshHitAnimation;
    private SingleAnimation _getArmorHitAnimation;
    private Spine.EventData _eventData;
    private punchCallback _punchCallback;
    private bool _punchEventSet;
    #endregion
    public void SetPunchCallback(punchCallback callback)
    {
        _punchCallback = callback;
    }
    public void PlayPunchAnimation(bool isCritical, bool isWrath)
    {
        if (isWrath)
            _wrathAnimation.Play();
        else if (isCritical)
            _critAnimation.Play();
        else
            _punchAnimation.Play();
    }
    public void PlayGetHitAnimation(bool armor)
    {
        if (armor)
            _getArmorHitAnimation.Play();
        else
            _getFleshHitAnimation.Play();
    }
    public BuffAnimation GetBuffAnimation(ItemNames name)
    {
        if (_buffAnimations.ContainsKey(name))
            return _buffAnimations[name];
        else
            return null;
    }
    private void Awake()
    {
        _skeletonGraphic = this.GetComponent<SkeletonGraphic>();
        RoundController.NewRoundIsStarted += resetAnamations;

        createBuffAnimation(ItemNames.Armor, _armorAnimationName);
        createBuffAnimation(ItemNames.Sword, _swordAnimationName);
        createBuffAnimation(ItemNames.Wings, _wingsAnimationName);
        createBuffAnimation(ItemNames.Nimbus, _nimbusAnimationName);
        createBuffAnimation(ItemNames.Wrath, null);
        _punchAnimation = new PunchAnimation(_skeletonGraphic, (int)ItemNames.Punch, _attackAnimationName, _idleAnimationName);
        _wrathAnimation = new PunchAnimation(_skeletonGraphic, (int)ItemNames.Punch, _wrathAttackAnimationName, _idleAnimationName);
        _critAnimation = new PunchAnimation(_skeletonGraphic, (int)ItemNames.Punch, _critAttackAnimationName, _idleAnimationName);

        int track = (int)ItemNames.Wrath + 1;
        _getFleshHitAnimation = new SingleAnimation(_skeletonGraphic, track, _getHitFlesh);
        _getArmorHitAnimation = new SingleAnimation(_skeletonGraphic, track, _getHitArmor);


        if (_eventName != "")
        {
            //_eventData = _skeletonGraphic.SkeletonData.FindEvent(_eventName); //no need while only 1 event exists
            _skeletonGraphic.AnimationState.Event += HandleAnimationStateEvent;
            _punchEventSet = true; // temp while attack and crit have one animation;
        }
        _skeletonGraphic.AnimationState.End += OnSpineAnimationEnd;
    }
    private void createBuffAnimation(ItemNames item, string aimationName)
    {
        BuffAnimation animation = new BuffAnimation(_skeletonGraphic, aimationName, (int)item);
        _buffAnimations.Add(item, animation);
    }
    private void resetAnamations(int stage)
    {
        _punchAnimation.Stop();
        foreach (var animation in _buffAnimations.Values)
            animation.ActivateBuff(false);
    }
    private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (trackEntry.Animation.Name.Equals(_attackAnimationName))
            _punchCallback?.Invoke();
    }
    public void OnSpineAnimationEnd(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Equals(_critAttackAnimationName) && (_punchEventSet == false) ||
            trackEntry.Animation.Name.Equals(_wrathAttackAnimationName))
            _punchCallback?.Invoke();
    }
}
