using UnityEngine;
using System;
using System.Collections;

static public class Utils
{
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }

    public static string TimeFormattedStringFromSeconds(float seconds)
    {
        TimeSpan span = new TimeSpan(0, 0, (int)seconds);

        string str = span.Minutes.ToString("00") + ":" + span.Seconds.ToString("00");

        if (span.Hours > 0)
        {
            str = ((int)span.TotalHours).ToString("00") + "h " + str;
        }

        return str;
    }

    public static void SendEmail(string email = "admin@albertosalieto.com", string subject = "TFG", string body = "Hi!")
    {
        string _subject = Utils.EscapeURL(subject);
        string _body = Utils.EscapeURL(body);
#if UNITY_ANDROID || UNITY_IOS
        Application.OpenURL("mailto:" + email + "?subject=" + _subject + "&body=" + _body);
#endif
    }

    public static string EscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
}