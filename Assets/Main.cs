using System;
using UnityEngine;
using UnityEngine.Diagnostics;
using BugSplatUnity;
using BugSplatUnity.Runtime.Client;

#if UNITY_STANDALONE_WIN
using System.Runtime.InteropServices;
#endif

public class Main : MonoBehaviour
{
    BugSplat bugsplat;

#if UNITY_STANDALONE_WIN
    [DllImport("BugSplatUnity")]
    static extern void BsCrashImmediately();
#endif

    void Start()
   {
        // TODO BG should we leave this, we used it to diagnose memory leak
        Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.Full);

        bugsplat = new BugSplat("octomore", Application.productName, Application.version);
        bugsplat.Description = "the default description";
        bugsplat.Email = "fred@bugsplat.com";
        bugsplat.Key = "the key!";
        bugsplat.User = "Fred";
        bugsplat.CaptureEditorLog = true;
        bugsplat.CapturePlayerLog = true;
        bugsplat.CaptureScreenshots = true;
        Application.logMessageReceived += bugsplat.LogMessageReceived;
#if UNITY_STANDALONE_WIN
        StartCoroutine(bugsplat.PostMostRecentCrash());
#endif
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
                var options = new ReportPostOptions()
                {
                    Description = "a new description"
                };

                static void callback()
                {
                    Debug.Log($"Exception post callback!");
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
