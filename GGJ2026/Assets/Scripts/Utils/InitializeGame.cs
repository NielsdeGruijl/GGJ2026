using System;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    [SerializeField] private SessionDataSO data;
    
    [SerializeField] private Player player;
    [SerializeField] private Mask groundItem;

    private void Awake()
    {
        if (!data || !data.selectedCharacter)
            return;
        
        if(player)
            player.SetPlayerData(data.selectedCharacter.CharacterData);
        
        if(groundItem)
            groundItem.Initialize(data.selectedCharacter.maskData);
    }
}
