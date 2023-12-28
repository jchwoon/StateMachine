using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _instatnce;
    public static Main Instatnce { get { Initialize(); return _instatnce; } }

    private PoolManager _poolManager = new PoolManager();
    public static PoolManager PoolManager { get { return _instatnce._poolManager; } }


    private void Start()
    {
        Initialize();
    }

    private static void Initialize()
    {
        if (_instatnce == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Main>();
            }

            DontDestroyOnLoad(go);
            _instatnce = go.GetComponent<Main>();
        }
    }
}
