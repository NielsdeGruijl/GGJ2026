using System;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    [SerializeField] private SessionDataSO data;
    
    [SerializeField] private Player player;
    [SerializeField] private Mask groundItem;

    private void Awake()
    {
        player.SetPlayerData(data.selectedCharacter.CharacterData);
        groundItem.maskSO = data.selectedCharacter.maskData;
    }
}
