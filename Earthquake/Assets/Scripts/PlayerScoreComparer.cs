using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreComparer : IComparer<PlayerScore>
{
    public int Compare(PlayerScore x, PlayerScore y)
    {
        return x.Score.CompareTo(y.Score);
    }
}
