﻿public class GameData
{
    public enum Languages
    {
        English = 0,
        Spanish = 1,
        Japanese = 2,
        italian,
        german,
        french,
        korean,
        chinese,
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
        Magic,
        Laser,
        Fire
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
