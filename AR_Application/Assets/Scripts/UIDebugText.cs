using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Singleton utility class to enable writing debug messages to the canvas text component when working on mobile.
/// </summary>
public class UIDebugText : MonoBehaviour
{
    public static UIDebugText uiDebugText;

    private static TextMeshProUGUI tmproText;

    void Awake()
    {
        if (uiDebugText != null)
            GameObject.Destroy(uiDebugText);
        else
            uiDebugText = this;

        DontDestroyOnLoad(this);

        tmproText = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Overrides the old logged texts with the given string.
    /// </summary>
    /// <param name="debugText"></param>
    public static void WriteLog(string debugText)
    {
        tmproText.text = debugText;
    }

    /// <summary>
    /// Adds the new log string to the existing log.
    /// </summary>
    /// <param name="debugText"></param>
    public static void AddLog(string debugText)
    {
        tmproText.text += debugText + "\n";
    }

    /// <summary>
    /// Resets the log text.
    /// </summary>
    public static void ResetLog()
    {
        tmproText.text = "";
    }
}
