[![BugSplat](https://s3.amazonaws.com/bugsplat-public/npm/header.png)](https://www.bugsplat.com)

# my-unity-crasher
The `my-unity-crasher` sample demonstates how to use BugSplat's [com.bugsplat.unity](https://github.com/BugSplat-Git/bugsplat-unity) package for crash and exception reporting for Unity projects.

Follow these steps to get started:

1. Clone this repository or download the zip from the releases tab
2. Initialize the submodules by running the following in a command window
```sh
git submodule init
git submodule update
```
3. Open project in Unity
4. Click the play button to start the project in the Editor
5. Press one of the key combinations found in [Main.cs](https://github.com/BugSplat-Git/my-unity-crasher/blob/efd4a340dd7c4ef3f1a376ca74d9ea5c20e64d9e/Assets/Main.cs#L37) to generate a crash or an exception
6. Find your crash on the [Crashes](https://app.bugsplat.com/v2/crashes?c0=appName&f0=CONTAINS&v0=my-unity-crasher&database=Fred) page and click the link in the ID column to see details about the crash

For additional information on how to use BugSplat, check out the [documentation](https://www.bugsplat.com/docs/sdk/unity/) on our website or send us a message via the in-app chat.

Enjoy!
© BugSplat Software
[Web](https://www.bugsplat.com) | [Twitter](https://twitter.com/BugSplatCo) | [Facebook](https://www.facebook.com/bugsplatsoftware/)
