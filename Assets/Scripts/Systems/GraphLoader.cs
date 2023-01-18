using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class GraphLoader : MonoBehaviour
{
    public string FilePath;
    private void Awake()
    {
        LoadGraph();
    }


    private void LoadGraph()
    {
        string path = Application.streamingAssetsPath + FilePath;
        path = path[0] == '/' ? path.Substring(1) : path;
        #if (UNITY_STANDALONE)
        byte[] file = File.ReadAllBytes(path);
        AstarPath.active.data.DeserializeGraphs(file);
        AstarPath.active.Scan();
#endif

#if (UNITY_ANDROID || UNITY_IPHONE || UNITY_WEBGL)
        StartCoroutine(FetchFile(path));
        #endif
    }
    private IEnumerator FetchFile(string filePath)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                // Get the file contents as a byte array
                byte[] fileContent = www.downloadHandler.data;
                AstarPath.active.data.DeserializeGraphs(fileContent);
                AstarPath.active.Scan();
            }
        }
    }
}
