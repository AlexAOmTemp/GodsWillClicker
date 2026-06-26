using UnityEngine;

public class Sounds : MonoBehaviour
{
    [Header("Combat")] [SerializeField] private AudioSource _receiveFleshDamageSound;
    [SerializeField] private AudioSource _evasonSound;
    [SerializeField] private AudioSource _receiveArmorDamageSound;
    [Header("Buffs")] [SerializeField] private AudioSource _armorSound;
    [SerializeField] private AudioSource _swordSound;
    [SerializeField] private AudioSource _wingSound;
    [SerializeField] private AudioSource _nimbusSound;
    [SerializeField] private AudioSource _wrathSound;
    
    public AudioSource GetBuffSound(AbilityNames name)
    {
        AudioSource sound = name switch
        {
            AbilityNames.Armor => _armorSound,
            AbilityNames.Sword => _swordSound,
            AbilityNames.Wings => _wingSound,
            AbilityNames.Nimbus => _nimbusSound,
            AbilityNames.Wrath => _wrathSound,
            _ => null
        };
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