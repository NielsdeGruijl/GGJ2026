using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Chest")]
public class ChestSO : ScriptableObject
{
    [Header("Visuals")]
    public Sprite chestSprite;
    
    [Header("stats")]
    public float basePrice;
    public float flatPriceIncrease;
    public float multPriceIncrease;

    [Space(5)]
    public float spawnChance;
    
    [Header("content")]
    public List<MaskSO> masks;
}
