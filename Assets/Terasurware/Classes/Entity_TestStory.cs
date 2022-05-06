using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_TestStory : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public string speaker;
		public string text;
		public string eventName;
		public int eventValue;
		public string who;
		public string emotion;
		public string reaction;
	}
}

