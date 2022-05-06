using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tech
{
    public string name;
    public Sprite techSpriteOne;
    public string description;

    public Tech(string newName, Sprite newSprite, string newDesc)
    {
        name = newName;
        techSpriteOne = newSprite;
        description = newDesc;
    }
}
