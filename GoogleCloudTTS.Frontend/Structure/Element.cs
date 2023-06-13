using GoogleCloudTTS.Shared.Enums;

namespace GoogleCloudTTS.Shared.Structure;

public class Element
{
    public int ItemID { get; set; }
    public EnumElementType Type { get; set; }

    public Element() {}
    
    public Element(int itemId, EnumElementType type)
    {
        ItemID = itemId;
        Type = type;
    }
}