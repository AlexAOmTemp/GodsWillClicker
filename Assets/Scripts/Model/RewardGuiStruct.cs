using UnityEngine.UI;

public struct RewardGui
{
    public Image RewardImage { get; private set; }
    public string Description { get; private set; }
    public RewardGui (Image image, string description) => (RewardImage, Description) = (image, description);
    
}
