using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaPosition : MonoBehaviour
{
    public enum Position
    {
        Solo,
        PairHigh,
        PairLow,
        TrioHigh,
        TrioMiddle,
        TrioLow,
        Disappear//多分テスト用？
    }

    public Dictionary<Position, Vector3> positionDict = new Dictionary<Position, Vector3>()
    {
        { Position.Solo,        new Vector3(0.5f,9.5f) },
        { Position.PairHigh,    new Vector3(0.4f,11.5f) },
        { Position.PairLow,     new Vector3(0.6f,7) },
        { Position.TrioHigh,    new Vector3(0.2f,11) },
        { Position.TrioMiddle,  new Vector3(0.5f,8) },
        { Position.TrioLow,     new Vector3(0.8f,5) },
        { Position.Disappear,   new Vector3(20,0) },
    };

    public enum Scale
    {
        Solo,
        Pair,
        Trio
    }

    public Dictionary<Scale, Vector3> scaleDict = new Dictionary<Scale, Vector3>()
    {
        { Scale.Solo, Vector3.one},
        { Scale.Pair, Vector3.one * 0.85f},
        { Scale.Trio, Vector3.one * 0.7f}
    };

    public enum SetPosition
    {
        GroupSolo,
        GroupLeft,
        GroupRight,
        SlashLeft,
        SlashCenter,
        SlashRight
    }

    //public Dictionary<SetPosition,Vector3>
}
