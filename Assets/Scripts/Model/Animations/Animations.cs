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
    private SkeletonGraphic _skeletonGraphic;
    private Dictionary<ItemNames, BuffAnimation> _buffAnimations = new Dictionary<ItemNames, BuffAnimation>();
    private PunchAnimation _punchAnimation;

    #endregion

    public void PlayPunchAnimation ()
    {
        _punchAnimation.Play();
    }
    public BuffAnimation GetBuffAnimation(ItemNames name)
    {
        return _buffAnimations[name];
    }
    private void Awake()
    {
        _skeletonGraphic = this.GetComponent<SkeletonGraphic>();
        createBuffAnimation(ItemNames.Armor, _armorAnimationName);
        createBuffAnimation(ItemNames.Sword, _swordAnimationName);
        createBuffAnimation(ItemNames.Wings, _wingsAnimationName);
        createBuffAnimation(ItemNames.Nimbus, _nimbusAnimationName);
        createBuffAnimation(ItemNames.Wrath, _wrathAnimationName);
        _punchAnimation = new PunchAnimation(_skeletonGraphic, (int)ItemNames.Punch, _attackAnimationName, _idleAnimationName);
    }
    private void createBuffAnimation(ItemNames item, string aimationName)
    {
        BuffAnimation animation = new BuffAnimation(_skeletonGraphic, aimationName, (int)item);
        _buffAnimations.Add(item, animation);
    }
}
