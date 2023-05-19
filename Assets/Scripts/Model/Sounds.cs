using UnityEngine;

public class Sounds : MonoBehaviour
{
    #region Editor Variables
    [Header("Combat")]
    [SerializeField] private AudioSource _receiveFleshDamageSound;
    [SerializeField] private AudioSource _evasonSound;
    [SerializeField] private AudioSource _receiveArmorDamageSound;
    [Header("Buffs")]
    [SerializeField] private AudioSource _armorSound;
    [SerializeField] private AudioSource _swordSound;
    [SerializeField] private AudioSource _wingSound;
    [SerializeField] private AudioSource _nimbusSound;
    [SerializeField] private AudioSource _wrathSound;
    #endregion

    public AudioSource GetBuffSound(ItemNames name)
    {
        AudioSource sound = null;
        switch (name)
        {
            case ItemNames.Armor:
            sound = _armorSound;
                break;
            case ItemNames.Sword:
            sound = _swordSound;
                break;
            case ItemNames.Wings:
            sound = _wingSound;
                break;
            case ItemNames.Nimbus:
            sound = _nimbusSound;
                break;
            case ItemNames.Wrath:
            sound = _wrathSound;
                break;
        }
        return sound;
    }
    public AudioSource GetFleshHitSound()
    { 
        return _receiveFleshDamageSound;
    }
    public AudioSource GetEvasonSound()
    { 
        return _evasonSound;
    }
    public AudioSource GetArmorHitSound()
    { 
        return _receiveArmorDamageSound;
    }

    static public void IfSetPlaySound(AudioSource sound)
    {
        if (sound != null)
            sound.Play();
    }
}
