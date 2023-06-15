using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableBool
{
    public int currentStack { get; private set; } = 0;
    public bool value => currentStack > 0;
    public void AddStack() => ++currentStack;
    public void SubtractStack() => --currentStack;
    public void ResetStack() => currentStack = 0;
    public static implicit operator bool(StackableBool stackableBool) => stackableBool.value;
}
