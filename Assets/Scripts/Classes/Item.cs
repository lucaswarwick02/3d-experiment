using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
     

    public static Color GetRarityColor (Rarity rarity) {
        switch (rarity) {
            case Rarity.Common:
                return Color.white;
            case Rarity.Uncommon:
                return Color.green;
            case Rarity.Rare:
                return Color.blue;
            case Rarity.Superior:
                return Color.magenta;
            case Rarity.Legendary:
                return new Color(1.0f, 0.647f, 0.0f);
            case Rarity.Mythical:
                return Color.red;
            default:
                return Color.white;
        }
    }
}

class CompareRarity : IComparer<Gun>
{
    public int Compare(Gun gun1, Gun gun2) {
        if (gun1 == null) return -1;
        return gun1.Rarity.CompareTo(gun2.Rarity);
    }
}

public enum Rarity {
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    Superior = 3,
    Legendary = 4,
    Mythical = 5
}