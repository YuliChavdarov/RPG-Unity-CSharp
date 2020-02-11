using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class Stat {

    [SerializeField]
    private int baseValue;

    private int currentValue;

    private List<int> modifiers = new List<int>();
    public int GetValue()
    {
        currentValue = baseValue;
        foreach (int modifier in modifiers)
        {
            currentValue += modifier;
        }
        return currentValue;
    }

    public void AddModifier(int modifier)
    {
        if(modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
}
