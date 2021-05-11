using BugSplatDotNetStandard;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Diagnostics;
using BugSplat = BugSplatUnity.BugSplat;

public class Main : MonoBehaviour
{
    BugSplat bugsplat;

#if UNITY_STANDALONE_WIN
    [DllImport("BugSplatUnity")]
    static extern void BsCrashImmediately();
#endif

    void Start()
   {
        bugsplat = new BugSplat("fred", Application.productName, Application.version);
        bugsplat.Description = "the default description";
        bugsplat.Email = "fred@bugsplat.com";
        bugsplat.Key = "the key!";
        bugsplat.User = "Fred";
        bugsplat.CaptureEditorLog = true;
        bugsplat.CapturePlayerLog = true;
        bugsplat.CaptureScreenshots = true;
        Application.logMessageReceived += bugsplat.LogMessageReceived;
        StartCoroutine(bugsplat.PostAllCrashes());
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -0.2f));

#if UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.A))
        {
            BsCrashImmediately();
        }
#endif
        if (Input.GetKeyDown(KeyCode.M))
        {
            Utils.ForceCrash(ForcedCrashCategory.Abort);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Utils.ForceCrash(ForcedCrashCategory.AccessViolation);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Utils.ForceCrash(ForcedCrashCategory.FatalError);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Utils.ForceCrash(ForcedCrashCategory.PureVirtualFunction);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            try
            {
                GenerateSampleStackFramesAndThrow();
            }
            catch (Exception ex)
            {
                var options = new ExceptionPostOptions()
                {
                    Description = "a new description"
                };

                static async void callback(HttpResponseMessage response)
                {
                    var status = response.StatusCode;
                    var contents = await response.Content.ReadAsStringAsync();
                    Debug.Log($"Response {status}: {contents}");
                };

                StartCoroutine(bugsplat.Post(ex, options, callback));
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            GenerateSampleStackFramesAndThrow();
        }
    }

    private void ThrowException()
    {
        throw new Exception("BugSplat rocks!");
    }

    private void GenerateSampleStackFramesAndThrow()
    {
        SampleStackFrame0();
    }

    private void SampleStackFrame0()
    {
        SampleStackFrame1();
    }

    private void SampleStackFrame1()
    {
        SampleStackFrame2();
    }

    private void SampleStackFrame2()
    {
        ThrowException();
    }
}
