using UnityEngine;
using System.Collections;

public class InstItem : MonoBehaviour {
	
	GameObject currentItem;
	string currentItemType = "";
	public GameObject itemAND;
	public GameObject itemOR;
	public GameObject itemIMPL;
	public GameObject itemEQUIV;
	public GameObject item1;
	public GameObject item0;
	
	public void GenerateItem (string itemType) 
	{
		Destroy (currentItem);
		if (itemType == "AND") 
		{
			currentItem = Instantiate (itemAND, transform.position, transform.rotation) as GameObject;
		}
		if (itemType == "OR") 
		{
			currentItem = Instantiate (itemOR, transform.position, transform.rotation) as GameObject;
		}
		if (itemType == "IMPL") 
		{
			currentItem = Instantiate (itemIMPL, transform.position, transform.rotation) as GameObject;
		}
		if (itemType == "EQUIV") 
		{
			currentItem = Instantiate (itemEQUIV, transform.position, transform.rotation) as GameObject;
		}
		if (itemType == "1") 
		{
			currentItem = Instantiate (item1, transform.position, transform.rotation) as GameObject;
		}
		if (itemType == "0") 
		{
			currentItem = Instantiate (item0, transform.position, transform.rotation) as GameObject;
		}
		currentItem.transform.parent = transform;
		currentItemType = itemType;
	}
	
	public string CurrentItemType ()
	{
		return currentItemType;
	}
	
	public void RemoveCurrentItem ()
	{
		Destroy (currentItem);
		currentItemType = "";
	}
	
}