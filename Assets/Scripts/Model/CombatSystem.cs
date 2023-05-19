using UnityEngine;
using System.Collections.Generic;
public class CombatSystem : MonoBehaviour
{
    [SerializeField] private GameObject _resourceTimerPrefab;
    [SerializeField] private Animations _animations;
    [SerializeField] private CombatSystem _enemy;
    [SerializeField] private GuiController _guiController;
    [SerializeField] bool _isPlayer;
    #region Audio
    [SerializeField] private Sounds _sounds;
    private AudioSource _receiveFleshDamageSound;
    private AudioSource _evasonSound;
    private AudioSource _receiveArmorDamageSound;

    #endregion
    #region Public Events
    public delegate void Death();
    public event Death IsDead;
    public ActiveItemsPack ActiveItems { get; private set; }

    #endregion
    #region Private Fields
    private Stats _stats = new Stats();
    private ResourceTimer[] _resourceTimers = new ResourceTimer[5];
    private bool _timersCreated = false;
    #endregion
    #region Public Methods
    public void Init(Stats stats, CountersPack pack)
    {
        createTimers();
        ActiveItems = new ActiveItemsPack(_isPlayer, this, _animations, _sounds, _guiController,_resourceTimers);
        ActiveItems.SetCountersData(pack);
        _stats = stats;
        _stats.calculateStats();
        _guiController.SetMaxLife(_isPlayer, _stats.Life);
    }
    public void OnButtonClick(ItemNames item)
    {
        ActiveItems.GetActiveItem(item).OnButtonClick();
    }
    public void GetDamage(int damage, bool isCrit, int swordDamage)
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

            if (damage <= 0)//all damage is blocked
            {
                _guiController.GenerateDamageText(_isPlayer, "Block", false, false);
                Sounds.IfSetPlaySound(_receiveArmorDamageSound);
                return;
            }
        }
        _guiController.GenerateDamageText(_isPlayer, damage.ToString(), sword, isCrit);
        _stats.Life -= damage;
        Sounds.IfSetPlaySound(_receiveFleshDamageSound);
        if (checkProc(_enemy.GetDropChance()))
            _enemy.AddPart(generatePart(wingHit, armorHit));
        if (_stats.Life <= 0)
            IsDead?.Invoke();
        _guiController.UpdateLife((_isPlayer), _stats.Life);
    }
    public void AddPart(ItemNames item)
    {
        ActiveItems.GetActiveItem(item).AddResource();
    }
    public float GetDropChance()
    {
        return _stats.DropChance;
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
        if (_sounds.GetFleshHitSound() != null)
            _receiveFleshDamageSound = _sounds.GetFleshHitSound();
        if (_sounds.GetEvasonSound() != null)
            _evasonSound = _sounds.GetEvasonSound();
        if (_sounds.GetArmorHitSound() != null)
            _receiveArmorDamageSound = _sounds.GetArmorHitSound();
    }
    void createTimers()
    {
        if (_timersCreated == false)
        {
            for (int i = 0; i < _resourceTimers.Length; i++)
            {
                var timer = Instantiate(_resourceTimerPrefab, Vector3.zero, Quaternion.identity, this.transform);
                _resourceTimers[i] = timer.GetComponent<ResourceTimer>();
            }
            _timersCreated=true;
        }
    }
    private void makePunch()
    {
        _animations.PlayPunchAnimation(); ;
        bool isCrit = false;
        int damage = _stats.Damage;
        int swordDamage = 0;

        if (ActiveItems.IsActive(ItemNames.Sword) == true)
        {
            ActiveItems.BuffChargeUsed(ItemNames.Sword);
            swordDamage = _stats.WeaponDamage;
        }
        if (ActiveItems.IsActive(ItemNames.Nimbus) == true)
        {
            if (checkProc(_stats.CriticalChance) == true)
            {
                ActiveItems.BuffChargeUsed(ItemNames.Nimbus);
                damage = (int)(damage * _stats.CriticalDamage / 100);
                swordDamage = (int)(swordDamage * _stats.CriticalDamage / 100);
                isCrit = true;
            }
        }
        _enemy.GetDamage(damage, isCrit, swordDamage);
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
