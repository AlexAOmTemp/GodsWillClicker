using UnityEngine;
using System.Collections.Generic;
public class CombatSystem : MonoBehaviour
{
    [SerializeField] private Animations _animations;
    [SerializeField] private CombatSystem _enemy;
    [SerializeField] private GuiController _guiController;
    [SerializeField] bool _isPlayer;
    public ActiveItemsPack ActiveItems { get; private set; }

    #region Audio
    [SerializeField] private Sounds _sounds;
    private AudioSource _receiveFleshDamageSound;
    private AudioSource _evasonSound;
    private AudioSource _receiveArmorDamageSound;

    #endregion
    #region Public Events
    public delegate void Death();
    public event Death IsDead;
    public delegate void ResourceProc(ItemNames name);
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
        ActiveItems.SetCountersData(pack);
        _stats = stats;
        _stats.calculateStats();
        _guiController.SetMaxLife(_isPlayer, _stats.Life);
        _guiController.UpdateLife(_isPlayer, _stats.Life);
        _animations.SetPunchCallback(punchAnimationFinished);
    }
    public void OnButtonClick(ItemNames item)
    {
        ActiveItems.GetActiveItem(item).OnButtonClick();
    }

    public void GetDamage(int damage, int swordDamage, bool isCrit)
    {
        bool wingHit = false;
        bool armorHit = false;
        bool sword = false;
        if (swordDamage > 0)
            sword = true;

        if (ActiveItems.IsActive(ItemNames.Wings) == true)
        {
            wingHit = true;
            if (checkProc(_stats.EvasonChance) == true)
            {
                ActiveItems.BuffChargeUsed(ItemNames.Wings);

                if (isCrit == true) //crit makes half of damage if evason is sucsessed
                    damage /= 2;
                else    //non crit makes no damage
                {
                    _guiController.GenerateDamageText(_isPlayer, "Evade", false, false);
                    Sounds.IfSetPlaySound(_evasonSound);
                    return;
                }
            }
        }
        if (ActiveItems.IsActive(ItemNames.Armor) == true)
        {
            armorHit = true;
            ActiveItems.BuffChargeUsed(ItemNames.Armor);

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
            ItemNames name = generatePart(wingHit, armorHit);
            if (_isPlayer == true)
                _enemy.AddPart(name);
            else
                ResorceIsGenerated?.Invoke(name);
        }
        _guiController.UpdateLife((_isPlayer), _stats.Life);
        if (_stats.Life <= 0)
            IsDead?.Invoke();

    }
    public void AddPart(ItemNames item)
    {
        ActiveItems.GetActiveItem(item).AddResource();
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
        ActiveItems = this.GetComponent<ActiveItemsPack>();
        ActiveItems.Init(this);
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
        if (ActiveItems.IsActive(ItemNames.Sword) == true)
        {
            ActiveItems.BuffChargeUsed(ItemNames.Sword);
            _swordDamage = _stats.WeaponDamage;
        }
        if (ActiveItems.IsActive(ItemNames.Nimbus) == true)
        {
            if (checkProc(_stats.CriticalChance) == true)
            {
                ActiveItems.BuffChargeUsed(ItemNames.Nimbus);
                _damage = (int)(_damage * _stats.CriticalDamage / 100);
                _swordDamage = (int)(_swordDamage * _stats.CriticalDamage / 100);
                _isCrit = true;
            }
        }
        if (ActiveItems.IsActive(ItemNames.Wrath) == true)
        {
            ActiveItems.BuffChargeUsed(ItemNames.Wrath);
            _damage *= 5;
            _swordDamage *= 5;
            isWrath = true;
        }
        _animations.PlayPunchAnimation(_isCrit, isWrath);
        //_enemy.GetDamage(_damage, _isCrit, _swordDamage);
    }
    private ItemNames generatePart(bool wing, bool armor)
    {
        List<ItemNames> random = new List<ItemNames>();

        if (wing == true) //wings and armor can be spent before call
            random.Add(ItemNames.Wings);
        if (ActiveItems.IsActive(ItemNames.Nimbus) == true)
            random.Add(ItemNames.Nimbus);
        if (ActiveItems.IsActive(ItemNames.Sword) == true)
            random.Add(ItemNames.Sword);
        if (armor == true)
            random.Add(ItemNames.Armor);
        random.Add(ItemNames.Punch);
        int RndGen = Random.Range(0, random.Count - 1);
        return (random[RndGen]);
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
}
