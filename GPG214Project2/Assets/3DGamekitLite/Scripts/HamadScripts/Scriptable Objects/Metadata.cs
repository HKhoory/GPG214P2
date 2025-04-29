using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "FileMetadata", menuName = "Metadata/File Metadata")]
public class Metadata : ScriptableObject
{

    public string fullFileName = "file.json";
    public string fileLink;
    public string metaFileLink;

    public string version;

    public string localFilePath => Path.Combine(Application.streamingAssetsPath, fullFileName);

    public string downloadLink => GoogleDriverLoader.GetDownloadLink(fileLink);



    public void SetupLocalData()
    {
        version = string.Empty;

        if (File.Exists(localFilePath))
        {
            string localContent = File.ReadAllText(localFilePath).ToString();
            
        }
        else
        {
            version = "-1";
        }
    }


    public bool FileUpdater()
    {
        return true;
    }

    public void DeleteLocalFile()
    {
        if (File.Exists(localFilePath)) { 
        
            File.Delete(localFilePath);
        
        }
    }


}
