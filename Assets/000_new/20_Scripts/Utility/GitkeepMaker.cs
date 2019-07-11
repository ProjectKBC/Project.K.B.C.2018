/* 空ディレクトリに対する.gitkeepでの対策
// https://gist.github.com/shimiu/5864179
// Unity Editorで新規フォルダが作られたときに自動で.gitkeepを作成する。
// Editorフォルダに入れて使う。
// プロジェクトルートに.gitフォルダが無かったらスルー。
*/

using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// 新規フォルダ作成時に.gitkeepを自動作成
/// </summary>
public class GitkeepMaker : AssetPostprocessor
{
	private readonly static string folderKeeperName = ".gitkeep";

	public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetsPath)
	{
		if (!Directory.Exists(".git")) return;

		foreach (string path in importedAssets)
		{
			if (Directory.Exists(path))
			{
				string folderKeeperPath = path + "/" + folderKeeperName;
				if (!File.Exists(folderKeeperPath))
				{
					File.Create(folderKeeperPath).Close();
					Debug.Log(folderKeeperPath + " created");
				}
			}
		}
	}
}