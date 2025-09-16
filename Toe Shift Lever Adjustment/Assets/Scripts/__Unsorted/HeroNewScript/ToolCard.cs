using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ToolCards/Card")]
public class ToolCard : ScriptableObject
{
    public string toolName;
    [SerializeField]Sprite toolSprite;
}
