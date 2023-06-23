using UnityEngine;
using System.Collections.Generic;
public class CombatSystem : MonoBehaviour
{
    [SerializeField] private Animations _animations;
    [SerializeField] private CombatSystem _enemy;
    [SerializeField] private GuiController _guiController;
    [SerializeField] bool _isPlayer;
    public AbilityPack Abilities { get; private set; }

    #region Audio
    [SerializeField] private Sounds _sounds;
    private AudioSource _receiveFleshDamageSound;
    private AudioSource _evasonSound;
    private AudioSource _receiveArmorDamageSound;

    #endregion
    #region Public Events
    public delegate void Death();
    public event Death IsDead;
    public delegate void ResourceProc(AbilityNames name);
    public ResourceProc ResorceIsGenerated; // for Collect Item Spawner class

    #endregion
    #region Private Fields
    private Stats _stats = new Stats();
    private bool _isCrit;
    private int _damage;
    private int _swordDamage;
    #endregion
    #region Public Methods
    public void Init(Stats stats, CountersPack pack)
    {
        Abilities.SetCountersData(pack);
        _stats = stats;
        _guiController.SetMaxLife(_isPlayer, _stats.Life);
        _guiController.UpdateLife(_isPlayer, _stats.Life);
        _animations.SetPunchCallback(punchAnimationFinished);
    }
    public void OnButtonClick(AbilityNames ability)
    {
        Abilities.GetAbility(ability).OnButtonClick();
    }

    public void GetDamage(int damage, int swordDamage, bool isCrit)
    {
        bool wingHit = false;
        bool armorHit = false;
        bool sword = false;
        if (swordDamage > 0)
            sword = true;

        if (Abilities.IsActive(AbilityNames.Wings) == true)
        {
            wingHit = true;
            //if (checkProc(_stats.EvasonChance) == true)
            //{
            Abilities.BuffChargeUsed(AbilityNames.Wings);

            if (isCrit == true) //crit makes half of damage if evason is sucsessed
                damage /= 2;
            else    //non crit makes no damage
            {
                _guiController.GenerateDamageText(_isPlayer, "Evade", false, false);
                Sounds.IfSetPlaySound(_evasonSound);
                return;
            }
            //}
        }
        if (Abilities.IsActive(AbilityNames.Armor) == true)
        {
            armorHit = true;
            Abilities.BuffChargeUsed(AbilityNames.Armor);

            //sword makes double damage to armor
            int armorAfterSword = _stats.ArmorValue - swordDamage * 2;
            if (armorAfterSword >= 0)
                damage -= armorAfterSword;
            else
                damage += (int)(armorAfterSword / (-2));

            _animations.PlayGetHitAnimation(true);
            Sounds.IfSetPlaySound(_receiveArmorDamageSound);
            if (damage <= 0)//all damage is blocked
            {
                _guiController.GenerateDamageText(_isPlayer, "Block", false, false);
                return;
            }
        }
        else
            damage += swordDamage; //no armor. Full sword damage applied

        _guiController.GenerateDamageText(_isPlayer, damage.ToString(), sword, isCrit);
        _stats.Life -= damage;

        if (armorHit == false)
        {
            Sounds.IfSetPlaySound(_receiveFleshDamageSound);
            _animations.PlayGetHitAnimation(false);
        }

        if (checkProc(_enemy.GetDropChance()))
        {
            AbilityNames[] names = generatePart(wingHit, armorHit);
            foreach (var name in names)
            {
                if (_isPlayer == true)
                    _enemy.AddPart(name);
                else
                    ResorceIsGenerated?.Invoke(name);
            }
        }
        _guiController.UpdateLife((_isPlayer), _stats.Life);
        if (_stats.Life <= 0)
            IsDead?.Invoke();

    }
    public void AddPart(AbilityNames ability)
    {
        Abilities.GetAbility(ability).AddResource();
    }
    public float GetDropChance()
    {
        // return _stats.DropChance; //temp
        return 100f;
    }
    public void MakeFewPunches(int count)
    {
        for (int i = 0; i < count; i++)
            makePunch();
    }
    #endregion
    #region Private Methods
    private void Awake()
    {
        Abilities = this.GetComponent<AbilityPack>();
        Abilities.Init(this);
        if (_sounds.GetFleshHitSound() != null)
            _receiveFleshDamageSound = _sounds.GetFleshHitSound();
        if (_sounds.GetEvasonSound() != null)
            _evasonSound = _sounds.GetEvasonSound();
        if (_sounds.GetArmorHitSound() != null)
            _receiveArmorDamageSound = _sounds.GetArmorHitSound();
    }
    private void punchAnimationFinished()
    {
        _enemy.GetDamage(_damage, _swordDamage, _isCrit);
    }
    private void makePunch()
    {
        _isCrit = false;
        bool isWrath = false;
        _damage = _stats.Damage;
        _swordDamage = 0;
        if (Abilities.IsActive(AbilityNames.Sword) == true)
        {
            Abilities.BuffChargeUsed(AbilityNames.Sword);
            _swordDamage = _stats.WeaponDamage;
        }
        if (Abilities.IsActive(AbilityNames.Nimbus) == true)
        {
            Abilities.BuffChargeUsed(AbilityNames.Nimbus);
            _damage = (int)(_damage * _stats.CriticalDamage / 100);
            _swordDamage = (int)(_swordDamage * _stats.CriticalDamage / 100);
            _isCrit = true;
        }
        if (Abilities.IsActive(AbilityNames.Wrath) == true)
        {
            Abilities.BuffChargeUsed(AbilityNames.Wrath);
            _damage *= 5;
            _swordDamage *= 5;
            isWrath = true;
        }
        _animations.PlayPunchAnimation(_isCrit, isWrath);
    }
    private AbilityNames[] generatePart(bool wing, bool armor)
    {
        List<AbilityNames> part = new List<AbilityNames>();

        if (Abilities.IsActive(AbilityNames.Nimbus) == true)
            if (checkProc(5))
                part.Add(AbilityNames.Nimbus);
        if (wing == true) //wings and armor can be spent before call
            if (checkProc(10))
                part.Add(AbilityNames.Wings);
        if (Abilities.IsActive(AbilityNames.Sword) == true)
            if (checkProc(15))
                part.Add(AbilityNames.Sword);
        if (armor == true)
            if (checkProc(20))
                part.Add(AbilityNames.Armor);
        if (checkProc(25))
            part.Add(AbilityNames.Punch);
        return (part.ToArray());
    }
    private bool checkProc(float chance)
    {
        float rand = Random.Range(0f, 99f);
        if (rand > chance)
            return false;
        else
            return true;
    }
    #endregion


    /*private AbilityNames generatePart(bool wing, bool armor)
    {
        List<AbilityNames> random = new List<AbilityNames>();

        if (wing == true) //wings and armor can be spent before call
            random.Add(AbilityNames.Wings);
        if (Abilities.IsActive(AbilityNames.Nimbus) == true)
            random.Add(AbilityNames.Nimbus);
        if (Abilities.IsActive(AbilityNames.Sword) == true)
            random.Add(AbilityNames.Sword);
        if (armor == true)
            random.Add(AbilityNames.Armor);
        random.Add(AbilityNames.Punch);
        int RndGen = Random.Range(0, random.Count - 1);
        return (random[RndGen]);
    }*/
}
