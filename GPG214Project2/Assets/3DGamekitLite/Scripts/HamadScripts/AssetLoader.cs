using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class AssetLoader : MonoBehaviour
{

    public string downloadPath;

    [SerializeField] private List<Metadata> dlc = new List<Metadata>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GoogleDriverLoader.GetDownloadLink(downloadPath));
        StartCoroutine(CheckAndDownloadFiles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CheckAndDownloadFiles()
    {

        //here you do the thing
        foreach(Metadata meta in dlc)
        {
            meta.SetupLocalData();

            yield return StartCoroutine(DownloadFile(meta.fileLink, meta.localFilePath));
            yield return new WaitForEndOfFrame();

            if (meta.FileUpdater())
            {

                meta.DeleteLocalFile();

                if (!string.IsNullOrEmpty(meta.localFilePath) && !string.IsNullOrEmpty(meta.metaFileLink))
                {
                    yield return StartCoroutine(DownloadFile(meta.fileLink, meta.localFilePath));
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    Debug.Log("Download link not valid");
                }



            }

            yield return null;

        }
        yield return null;
    }

    private IEnumerator DownloadFile(string link, string path)
    {
        if (string.IsNullOrEmpty(link))
        {
            Debug.Log("Error: Link not valid");
            yield break;
        }

        UnityWebRequest request = UnityWebRequest.Get(link);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Failed to download file");
        }
        else
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllBytes(path, request.downloadHandler.data);
            Debug.Log("File Downloaded successfully");
        }

    }


}
