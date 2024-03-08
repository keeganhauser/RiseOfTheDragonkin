using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HTMLUtil
{
    /// <summary>
    /// Returns a string wrapped in an html color tag of the specified color.
    /// List of colors: https://htmlcolorcodes.com/color-names/
    /// </summary>
    public static string GetColoredString(string text, string color)
    {
        return $"<color={color}>{text}</color>";
    }
}
