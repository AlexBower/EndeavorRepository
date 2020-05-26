using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public enum ButtonOption
    {
        DOWN, LEFT, UP, RIGHT, A, B, X, Y, RANDOM
    }

    public new string name;
    public string description;
    public string flavorText;

    public Sprite artwork;
    
    public List<ButtonOption> buttonOptions;

    private bool isSelected;
}
