using System;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class UnityAsyncSceneLoading
{
    private static CancellationTokenSource cts;

    //Methods
    //Try Catch async task
    public static async Task LoadNewScene(string sceneName)
    {
        if (cts == null)
        {
            cts = new CancellationTokenSource();
            try
            {
                await PerformSceneLoading(cts.Token,sceneName);
            }
            catch (OperationCanceledException ex)
            {
                if (ex.CancellationToken == cts.Token)
                {
                    //Perform operation after cancelling
                    Debug.Log("Task cancelled");                        
                }
            }
            finally
            {
                cts.Cancel();
                cts = null;
            }
        }
        else
        {
            //Cancel Previous token
            cts.Cancel();
            cts = null;
        }
    }
    //Actual Scene loading
    private static Task PerformSceneLoading(CancellationToken token,string sceneName)
    {
        token.ThrowIfCancellationRequested();
        if (token.IsCancellationRequested)
            return Task.CompletedTask;

        var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (true)
        {
            token.ThrowIfCancellationRequested();
            if (token.IsCancellationRequested)
                return Task.CompletedTask;               
            if (asyncOperation.progress>=0.9f)
                break;               
        }
        asyncOperation.allowSceneActivation = true;            
        cts.Cancel();
        token.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }
}        