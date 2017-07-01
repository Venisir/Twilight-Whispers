using UnityEngine;
using System.Collections;
using System;

public static class Registry
{
    /**********************************************************************************************/
    /* Public types and data                                                                      */
    /**********************************************************************************************/

    #region Public types and data

    /// <summary>
    /// The Guid of the user. This Guid uniquely identifies each user, and is given to us when by
    /// the server when we first log on it.
    /// </summary>
    public static Guid UserGuid
    {
        get
        {
            if (PlayerPrefs.HasKey(k_Key_UserGuid))
            {
                return new Guid(PlayerPrefs.GetString(k_Key_UserGuid));
            }

            return Guid.Empty;
        }
        set
        {
            if (value != Guid.Empty)
            {
                PlayerPrefs.SetString(k_Key_UserGuid, value.ToString());
            }
            else
            {
                PlayerPrefs.DeleteKey(k_Key_UserGuid);
            }

            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// The user languages.
    /// </summary>
    public static string UserLanguage
    {
        get
        {
            if (PlayerPrefs.HasKey(k_Key_UserLanguages))
            {
                return PlayerPrefs.GetString(k_Key_UserLanguages);
            }
            return string.Empty;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                PlayerPrefs.DeleteKey(k_Key_UserLanguages);
            }
            else
            {
                PlayerPrefs.SetString(k_Key_UserLanguages, value);
            }

            PlayerPrefs.Save();
        }
    }

    #endregion

    /**********************************************************************************************/
    /* Public methods                                                                             */
    /**********************************************************************************************/

    #region Public methods

    /// <summary>
    /// Checks if a given key exists in the registry.
    /// </summary>
    /// <param name="strKey">The key to search for</param>
    /// <returns>true if it exists, false if not</returns>
    public static bool HasKey(string strKey)
    {
        return PlayerPrefs.HasKey(strKey);
    }

    /// <summary>
    /// Deletes a given key in the registry.
    /// </summary>
    /// <param name="strKey">The key to delete</param>
    public static void DeleteKey(string strKey)
    {
        PlayerPrefs.DeleteKey(strKey);
    }

    #endregion

    /**********************************************************************************************/
    /* Private data                                                                               */
    /**********************************************************************************************/

    private const string k_Key_UserGuid = "User Guid";
    private const string k_Key_UserLanguages = "User Languages";
}