using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlowerType", menuName = "Game/FlowerType")]
public class FlowerType : ScriptableObject
{
    public string flowerName;
    public int stars;

    public Sprite withLeaves;
    public Sprite withoutLeaves;
}
