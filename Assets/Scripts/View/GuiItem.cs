using UnityEngine.UI;
using TMPro;

public class GuiItem
{
    public Button Button { get; private set; }
    public TMP_Text AwailabilityCounter { get; private set; }
    public TMP_Text ResourceCounter { get; private set; }
    public TMP_Text CurrentEffectDuration { get; private set; }
    public ButtonFiller Filler { get; private set; }
    public GuiItem(Button button, TMP_Text awailabilityCounter,
        TMP_Text resourceCounter, TMP_Text currentEffectDuration)
    {
        if (button != null)
            Button = button;
        if (awailabilityCounter != null)
            AwailabilityCounter = awailabilityCounter;
        if (resourceCounter != null)
            ResourceCounter = resourceCounter;
        if (currentEffectDuration != null)
            CurrentEffectDuration = currentEffectDuration;
        if (Button != null)
            Filler = button.GetComponent<ButtonFiller>();
    }
    public void UpdateGui(CountersSet counters)
    {
        if (ResourceCounter != null)
            ResourceCounter.SetText(counters.Resource.ToString());
        if (Filler != null)
            Filler.FillImage(counters.Clicks);
        if (CurrentEffectDuration != null)
            CurrentEffectDuration.SetText(counters.CurrentEffectDuration.ToString());
        if (Button != null && AwailabilityCounter != null)
            updateButtonAvailability(counters.ButtonAwailability, Button, AwailabilityCounter);
    }
    private void updateButtonAvailability(int value, Button button, TMP_Text counter)
    {
        counter.SetText(value.ToString());
        if (value > 0)
        {
            if (button.interactable == false)
                button.interactable = true;
        }
        else
            button.interactable = false;
    }
}
