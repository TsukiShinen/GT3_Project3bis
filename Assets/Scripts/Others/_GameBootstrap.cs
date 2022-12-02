using System;
using UnityEngine;

public class _GameBootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        var app = Instantiate(Resources.Load("App"));
        if (!app)
            throw new ApplicationException();
        DontDestroyOnLoad(app);
    }
}