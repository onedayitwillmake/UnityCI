using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class BuilderScript : EditorWindow
{
	private static string path = Path.Combine (Application.dataPath, "../_builds/");
	private static string appPrefix = PlayerSettings.productName;

	private static void BuildDevAndProduction () {
		Debug.Log("CAN YOU SEE ME!");
		BuildIOS();
		BuildETC();
	}

	private static void SetUp ()
	{
		if (!Directory.Exists (path))
			Directory.CreateDirectory (path);
	}

	private static void SetUpAndroidSettings ()
	{
		//set up android specific settings, such as keystore, password, etc
		//this method is called before building android


		/*PlayerSettings.Android.keystoreName = "";
		PlayerSettings.Android.keyaliasName = "";
		PlayerSettings.Android.keystorePass = "";
		PlayerSettings.Android.keyaliasPass = "";
		*/
	}

	private static void SetUpIOSSettings ()
	{
		//set up ios specific settings, such as compilation options
	}

	[MenuItem ("Custom/CI/Build Android apks for all Texture Compressions")]
	static void BuildApksForAllCompressions ()
	{
		IEnumerator texCompress = Enum.GetValues (typeof(AndroidBuildSubtarget)).GetEnumerator ();
		int apksCount = Enum.GetValues (typeof(AndroidBuildSubtarget)).Length;

		Debug.Log ("...building " + apksCount + " apks");
		while (texCompress.MoveNext ()) {
			var current = (AndroidBuildSubtarget)texCompress.Current;
			BuildAndroid (current);
		}

		Debug.Log ("Finished building");
	}

	[MenuItem ("Custom/CI/Build ATC")]
	private static void BuildATC ()
	{
		BuildAndroid (AndroidBuildSubtarget.ATC);
	}

	[MenuItem ("Custom/CI/Build ETC")]
	private static void BuildETC ()
	{
		BuildAndroid (AndroidBuildSubtarget.ETC);
	}

	[MenuItem ("Custom/CI/Build DXT")]
	private static void BuildDXT ()
	{
		BuildAndroid (AndroidBuildSubtarget.DXT);
	}

	[MenuItem ("Custom/CI/Build PVRTC")]
	private static void BuildPVRTC ()
	{
		BuildAndroid (AndroidBuildSubtarget.PVRTC);
	}

	[MenuItem ("Custom/CI/Build ETC2")]
	private static void BuildETC2 ()
	{
		BuildAndroid (AndroidBuildSubtarget.ETC2);
	}

	private static string[] GetScenesToBuild ()
	{
		List<string> sc = new List<string> ();
		foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (scene.enabled)
				sc.Add (scene.path);
		}
		return sc.ToArray ();
	}

	private static void BuildAndroid (AndroidBuildSubtarget target)
	{
		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
			EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.Android);

		EditorUserBuildSettings.androidBuildSubtarget = target;

		SetUp ();
		SetUpAndroidSettings ();

		try {
			string buildName = path + appPrefix + "_" + target.ToString ().ToLower () + ".apk";
			BuildPipeline.BuildPlayer (GetScenesToBuild (), buildName, BuildTarget.Android, BuildOptions.None);
			Debug.Log ("Finished: " + buildName);
		} catch (Exception e) {
			Debug.Log ("Error: " + e.Message);
		}
	}

	[MenuItem ("Custom/CI/Build iOS")]
	private static void BuildIOS ()
	{
		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iPhone)
			EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.iPhone);

		SetUp ();
		SetUpIOSSettings ();

		try {
			string buildName = path + appPrefix + "_xcode/";
			BuildPipeline.BuildPlayer (GetScenesToBuild (), buildName, BuildTarget.iPhone, BuildOptions.None);
			Debug.Log ("Finished: " + buildName);
		} catch (Exception e) {
			Debug.Log ("Error: " + e.Message);
		}
	}
}
