using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Character")]
public class CharacterSO : ScriptableObject
{
    [Header("Data")]
    public BaseEntityDataSO CharacterData;
    public MaskSO maskData;

    [Header("Other")] 
    public Sprite characterSprite;
    public string characterName;
}
