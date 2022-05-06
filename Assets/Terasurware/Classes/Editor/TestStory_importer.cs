using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class TestStory_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Excel/TestStory.xls";
	private static readonly string exportPath = "Assets/Excel/TestStory.asset";
	private static readonly string[] sheetNames = { "initialize","Test1","Test2","Test3", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_TestStory data = (Entity_TestStory)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_TestStory));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_TestStory> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					Entity_TestStory.Sheet s = new Entity_TestStory.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_TestStory.Param p = new Entity_TestStory.Param ();
						
					cell = row.GetCell(0); p.speaker = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.text = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.eventName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.eventValue = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.who = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(5); p.emotion = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(6); p.reaction = (cell == null ? "" : cell.StringCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
