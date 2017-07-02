using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public enum Languages
    {
        English = 0,
        Spanish = 1,
        italian,
        german,
        french,
        korean,
        chinese,
        japanese,
        russian,
        portuguese
    }

    public enum GameColors
    {
        Cyan,
        Magenta,
        Yellow,
        Green,
        Blue,
        Red,
        Black,
        White
    }

    public enum Towers
    {
        Bullets,
        Lasers
    }

    public enum EnemyStates
    {
        Walking,
        Dying,
        Scaping
    }

    public enum PlayerStates
    {
        Idle,
        Walking,
        Dying,
        Attacking
    }
}
