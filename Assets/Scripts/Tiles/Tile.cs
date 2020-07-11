using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int MarkedValue { get; private set; } = 0;
    public bool IsMarked { get; private set; } = false;

    public void MarkValue(int value)
    {
        MarkedValue = value;
        IsMarked = true;
    }

    public void Reset()
    {
        MarkedValue = 0;
        IsMarked = false;
    }
}
