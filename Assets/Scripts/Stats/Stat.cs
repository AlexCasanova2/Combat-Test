﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    private List<int> modifiers = new List<int>();

    public int getValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    //Añadimos el modificador 
    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }

    public static explicit operator int(Stat v)
    {
        throw new NotImplementedException();
    }
}
