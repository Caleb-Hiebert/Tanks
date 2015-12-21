using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

class SceneLauncher : EditorWindow {
	[MenuItem ("Scenes/SceneOpener")]
	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(SceneLauncher), false,"Scenes");
	}

	Vector2 ScrollPos;
	List<string> Scenes = new List<string>();

	void Update()
	{
	//	Scenes.Add(""+Scenes.Count);
	}

	void OnGUI ()
	{
		if(!Scenes.Contains(EditorApplication.currentScene))
		{
			if(GUILayout.Button("Add Current"))
			{
				Scenes.Add(EditorApplication.currentScene);
			}
		}
		if(GUILayout.Button("New Scene"))
		{
			EditorApplication.NewEmptyScene();
		}
		if(GUILayout.Button("Clear All"))
		{
			Scenes.RemoveRange(0,Scenes.Count);
		}
		ScrollPos = GUILayout.BeginScrollView(ScrollPos);
		for(int x = 0; x < Scenes.Count; x++)
		{
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("X" , GUILayout.Width(20), GUILayout.Height(17)))
			{
				Remove(x);
			}
			if(GUILayout.Button(Scenes[x]))
			{
				EditorApplication.SaveCurrentSceneIfUserWantsTo();
				EditorApplication.OpenScene(Scenes[x]);
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndScrollView();

	}

	void Remove(int NO)
	{
		Scenes.RemoveAt(NO);
	}

}