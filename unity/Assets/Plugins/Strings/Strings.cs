using UnityEngine;
using System.Collections;
using System.Linq;
using JsonFx.Json;
using System.Collections.Generic;

public class Strings : object 
{	
#region Static	
	static Strings singleton;
	static SystemLanguage language;
	public static Strings Singleton
	{
		get
		{
			if (singleton == null)
			{
				language = SystemLanguage.Russian;
				singleton = new Strings();
			}
			return singleton;
		}
	}
	
	public static string Get(string key)
	{
		return Singleton.GetStringForKey(key);
	}
#endregion
	
	
	string GetStringForKey(string key)
	{
        string word = "";
        try
        {
            key = key.ToLower();
            word = dict[key];
        }
        catch 
        {
            Debug.LogError(key + " is not found in dictionary");
        }
        return word;
	}
	
	Strings()
	{
		var filepath = "Global";
		var text = Resources.Load(filepath) as TextAsset;
		
		var reader = new JsonReader(text.ToString());
		var words = reader.Deserialize() as IEnumerable;
		
		dict = new Dictionary<string, string> ();
		var lKey = language == SystemLanguage.Russian ? "ru" : "en";
		foreach (Dictionary<string, object> word in words) 
		{
			dict[(word["key"] as string).ToLower()] = word[lKey] as string;
		}
	}
	
	Dictionary<string, string> dict;
}
