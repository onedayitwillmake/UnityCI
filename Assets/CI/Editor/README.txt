This is really easy-to-use script that helps you to integrate Unity with any continuous integration system which supports command line(Windows/Mac).
It helps you to build ios/android apps with different texture compression format, just in one click.

This builder script works on both Unity and Unity Pro.


If you have any question or concerns, please feel free to contact me at rganeyev+unityas@gmail.com

How-to use it(Unity):

Import BuilderScript, make sure it is located in Editor folder.
Make sure your project can be builded in editor.
Choose Unity menu "Custom -> CI -> Build " what you like.

Available build options:
--Android--
* Build for all texture compressions - builds android apks for all texture compressions, including ATC, ETC, ETC2, DXT, PVRTC.
To learn more, check docs here:
http://docs.unity3d.com/412/Documentation/ScriptReference/AndroidBuildSubtarget.html
* Build ATC 
* Build ETC 
* Build ETC2 
* Build DXT 
* Build PVRTC. 

--iOS--
* Build iOS 

Advanced:
You may have some project-specific and platform-specific options. There are methods prepared for that:
* static void SetUp() - called before any build happen. You can arrange project-specific options there.
* static void SetUpAndroidSettings() - Android specific options (keystore, password, etc)
* static void SetUpiOSSettings()  - iOS specific options.


Continuous integration tips:
Unity allows to run and execute methods from command line in batchmode. 

Under MacOS, you can launch Unity from the Terminal by typing:-
/Applications/Unity/Unity.app/Contents/MacOS/Unity

...while under Windows, you should type
"C:\Program Files (x86)\Unity\Editor\Unity.exe"
...at the command prompt.

You can call executing static method with parameter -executeMethod:

Windows:
C:\program files\Unity\Editor>Unity.exe -quit -batchmode -executeMethod BuilderScript.BuildApksForAllCompressions

Mac OS:
/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -executeMethod BuilderScript.BuildApksForAllCompressions -projectPath /Users/mgonzalez/SVN/UnityCI/UnityCI

Use -quit, -batchmode parameters to run Unity in batchmode (no windows are shown) and quit Unity after finishing executing script.

Learn more: 
http://docs.unity3d.com/Documentation/Manual/CommandLineArguments.html