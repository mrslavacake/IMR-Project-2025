using UnityEngine;

[CreateAssetMenu(fileName = "New Organ Info", menuName = "Dissection/Organ Info")]
public class OrganInfo : ScriptableObject
{
    public string organName;
    [TextArea(3, 10)]
    public string detailedDescription;
    public Sprite organIllustration; 
}