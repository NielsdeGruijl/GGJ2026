using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class IntValueEvent : UnityEvent<int>
{
}

public class CharacterButton : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private Image characterImage;
    [SerializeField] private Image maskImage;
    
    public IntValueEvent OnSelected = new();
    
    private CharacterSO characterData;

    private int index;

    public void Initialize(CharacterSO character, int index)
    {
        characterData = character;
        this.index = index;
        
        characterImage.sprite = characterData.characterSprite;
        maskImage.sprite = characterData.maskData.maskSprite;
    }
    
    public void OnSelect()
    {
        OnSelected.Invoke(index);
    }

    public void Select()
    {
        
    }

    public void Deselect()
    {
        
    }
}
