using TMPro;
using UnityEngine;

public class CharacterInfoPanel : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterDescription;
    
    [Header("Mask")]
    [SerializeField] private TMP_Text maskName;
    [SerializeField] private TMP_Text maskDescription;

    public void SelectNewCharacter(CharacterSO characterData)
    {
        characterName.text = characterData.characterName;
        string levelUpData = "";
        
        foreach (EntityStatModifier statModifier in characterData.CharacterData.LevelupModifiers)
        {
            string modifictionType = "";

            if (statModifier.modificationType == StatModificationType.multiplier)
                modifictionType = "%";
            
            levelUpData += statModifier.stat + ": +" + statModifier.value + modifictionType + "\n";
        }

        characterDescription.text = levelUpData;

        maskName.text = characterData.maskData.maskName;
        maskDescription.text = characterData.maskData.maskDescription;
    }
}
