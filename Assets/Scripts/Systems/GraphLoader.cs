using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GraphLoader : MonoBehaviour
{
    public string FilePath;
    private void Awake()
    {
        LoadGraph();
    }


    private void LoadGraph()
    {
        
        byte[] file = File.ReadAllBytes(Application.streamingAssetsPath + FilePath);
        AstarPath.active.data.DeserializeGraphs(file);
        AstarPath.active.Scan();
    }
}
