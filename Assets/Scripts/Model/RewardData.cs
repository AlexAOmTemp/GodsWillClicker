using UnityEngine;
public struct RewardData
{
    public delegate void CallBack (int value);
    public delegate void CallBackWithName (AbilityNames name, int value);
  
    public CallBack StatIncrement { get; private set; }
    public CallBackWithName CounterIncrement { get; private set; }
   
    public Sprite RewardImage { get; private set; }
    public string Description { get; private set; }
    public AbilityNames Name { get; private set; }
    public int Value { get; set; }

    public RewardData (Sprite image, string description, CallBack statIncrement) => (RewardImage, Description, StatIncrement, CounterIncrement, Name, Value) = (image, description, statIncrement, null, AbilityNames.Armor,1);
    public RewardData(Sprite image, string description, CallBackWithName counterIncrement, AbilityNames name) => (RewardImage, Description, StatIncrement, CounterIncrement, Name, Value) = (image, description, null, counterIncrement, name,1);

    public void InvokeEffect()
    {
        StatIncrement?.Invoke(Value);
        CounterIncrement?.Invoke(Name, Value);
    }
}
