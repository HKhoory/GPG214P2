using UnityEngine;
using System.Text.RegularExpressions;

public class GoogleDriverLoader
{
    
    public static string GetDownloadLink(string link)
    {
        Match match = Regex.Match(link, @"(?:drive\.google\.com\/.*?\/d\/|id=)([a-zA-Z0-9_-]+)");

        string result = string.Empty;


        if (match.Success)
        {
            string id = match.Groups[1].Value;
            result = "https://drive.google.com/uc?export=download&id=" + id;
            Debug.Log(result);
        }
        else
        {
            Debug.Log("Invalid Google Drive Link");
        }

        return result;

    }


}
