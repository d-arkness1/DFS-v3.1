using UnityEngine;

[CreateAssetMenu(fileName = "ModuleFieldOption", menuName = "Scriptable Objects/Module Field Option")]
public class ModuleFieldOption : ModuleField
{
    public Sprite itemImage;
    [TextArea(5, 10)]
    public string description;
}
