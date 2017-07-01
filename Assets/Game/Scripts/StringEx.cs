using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using UnityEngine;

/// <summary>
/// Extends String class to add some methods
/// </summary>
public static class StringEx
{
    /// <summary>
    /// Check if an string has a valid email format
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsEmailValid(this string email)
    {
        try
        {
            MailAddress m = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coins"></param>
    /// <param name="millionsChr"></param>
    /// <returns></returns>
    public static string GetCurrencyFormat(int coins, char millionsChr)
    {
        if (coins < 10000)
            return coins.ToString();

        if (coins < 1000000)
        {
            int thousands = coins / 1000;
            int remain = coins % 1000;
            return thousands.ToString() + ' ' + remain.ToString("000");
        }
        else
        {
            float millions = coins / 1000000.0f;
            return millions.ToString("0.##") + ' ' + millionsChr;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="secondsLeft"></param>
    /// <returns></returns>
    public static string GetTimeFormat(float secondsLeft, string dayChr, string hourChar, string minChar, string secondsChar)
    {
        int days = (int)(secondsLeft / SecondsPerDay);
        int hours = (int)(secondsLeft % SecondsPerDay) / SecondsPerHour;
        if (days > 0)
        {
            return ((int)days).ToString() + dayChr + ' ' + ((int)hours).ToString() + hourChar;
        }
        int minutes = (int)((secondsLeft % SecondsPerHour) / SecondsPerMinute);
        if (hours > 0)
        {
            return ((int)hours).ToString() + hourChar + ' ' + ((int)minutes).ToString() + minChar;
        }
        int seconds = Mathf.CeilToInt(secondsLeft % SecondsPerMinute);
        if (minutes > 0)
        {
            return ((int)minutes).ToString() + minChar + ' ' + ((int)seconds).ToString() + secondsChar;
        }
        return seconds.ToString() + secondsChar;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="secondsLeft"></param>
    /// <returns></returns>
    public static string GetTimeFormatDayHourMinutes(float secondsLeft, string dayChr, string hourChar, string minChar)
    {
        int days = (int)(secondsLeft / SecondsPerDay);
        int hours = (int)(secondsLeft % SecondsPerDay) / SecondsPerHour;
        if (days > 0)
        {
            return ((int)days).ToString() + dayChr + ' ' + ((int)hours).ToString() + hourChar;
        }
        int minutes = (int)((secondsLeft % SecondsPerHour) / SecondsPerMinute);

        return ((int)hours).ToString() + hourChar + ' ' + ((int)minutes).ToString() + minChar;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string Capitalize(this string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }

    /// <summary>
    /// Elimina un caracter duplicado dentro de una cadena
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string RemoveDuplicateChar(string s, char chr)
    {
        string myChar = chr.ToString();
        string myChar2 = chr.ToString() + chr.ToString();
        string finalString = s;
        do
        {
            string test = finalString.Replace(myChar2, myChar);
            if (test.Length == finalString.Length)
            {
                break;
            }
            else
            {
                finalString = test;
            }
        } while (true);
        return finalString;
    }

    /**********************************************************************************************/
    /* Private fields                                                                             */
    /**********************************************************************************************/

    #region Private fields

    private const int SecondsPerMinute = 60;
    private const int SecondsPerHour = SecondsPerMinute * 60;
    private const int SecondsPerDay = SecondsPerHour * 24;

    #endregion // Private fields
}
