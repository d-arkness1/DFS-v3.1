using UnityEngine;

[CreateAssetMenu(fileName = "ModuleFieldGroup", menuName = "Scriptable Objects/Module Field Group", order =-1)]
public class ModuleFieldGroup : ScriptableObject
{
	public Sprite icon;
	[TextArea(5, 10)]
	public string description;
	public Sprite[] dataSprites;
	public ArrayModuleField identifiers;
	public ModuleField[] fields;
}
