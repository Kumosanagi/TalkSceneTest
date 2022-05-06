using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class SomeSheetsTestData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Excel/SomeSheetsTestData.xls";
	private static readonly string exportPath = "Assets/Excel/SomeSheetsTestData.asset";
	private static readonly string[] sheetNames = { "Sheet1","Sheet1-2","Sheet1-2 (2)", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_SomeSheetsTestData data = (Entity_SomeSheetsTestData)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_SomeSheetsTestData));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_SomeSheetsTestData> ();
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

					Entity_SomeSheetsTestData.Sheet s = new Entity_SomeSheetsTestData.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_SomeSheetsTestData.Param p = new Entity_SomeSheetsTestData.Param ();
						
					cell = row.GetCell(0); p.number = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.text = (cell == null ? "" : cell.StringCellValue);
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
