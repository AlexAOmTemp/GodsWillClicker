using UnityEngine.UI;

public struct RewardData
{
    public delegate void CallBack (int value);
    public delegate void CallBackWithName (ItemNames name, int value);
  
    public CallBack StatIncrement { get; private set; }
    public CallBackWithName CounterIncrement { get; private set; }
   
    public Image RewardImage { get; private set; }
    public string Description { get; private set; }
    public ItemNames Name { get; private set; }
    public int Value { get; set; }

    public RewardData (Image image, string description, CallBack statIncrement) => (RewardImage, Description, StatIncrement, CounterIncrement, Name, Value) = (image, description, statIncrement, null, ItemNames.Armor,1);
    public RewardData(Image image, string description, CallBackWithName counterIncrement, ItemNames name) => (RewardImage, Description, StatIncrement, CounterIncrement, Name, Value) = (image, description, null, counterIncrement, name,1);

    public void InvokeEffect()
    {
        StatIncrement?.Invoke(Value);
        CounterIncrement?.Invoke(Name, Value);
    }
}
