using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private CharacterInfoPanel infoPanel;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private SessionDataSO sessionData;
    
    [SerializeField] private List<CharacterSO> characters;

    [SerializeField] private CharacterButton characterButtonPrefab;
    [SerializeField] private Transform characterButtonContainer;

    private List<CharacterButton> characterButtons = new();
    
    private CharacterButton currentSelectedCharacter;
    private CharacterSO currentCharacterData;
    
    private void Awake()
    {
        infoPanel.gameObject.SetActive(false);
        confirmButton.SetActive(false);
        
        int tempIndex = 0;
        foreach (CharacterSO character in characters)
        {
            CharacterButton button = Instantiate(characterButtonPrefab, characterButtonContainer);
            button.Initialize(character, tempIndex);
            button.OnSelected.AddListener(OnCharacterSelected);
            characterButtons.Add(button);
            tempIndex++;
        }
    }

    public void OnCharacterSelected(int index)
    {
        confirmButton.SetActive(true);
        infoPanel.gameObject.SetActive(true);
        
        if (currentSelectedCharacter == characterButtons[index])
            return;
        
        if (!currentSelectedCharacter)
        {
            currentSelectedCharacter = characterButtons[index];
            currentSelectedCharacter.Select();
            infoPanel.SelectNewCharacter(characters[index]);
            currentCharacterData = characters[index];
            return;
        }
        
        currentSelectedCharacter.Deselect();
        currentSelectedCharacter = characterButtons[index];
        currentSelectedCharacter.Select();
        infoPanel.SelectNewCharacter(characters[index]);
        currentCharacterData = characters[index];
    }

    public void OnConfirm()
    {
        sessionData.selectedCharacter = currentCharacterData;

        SceneManager.LoadScene(1);
    }
}
