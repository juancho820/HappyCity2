using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour {

    public string[] items;

    public Text codigo;

    IEnumerator Start()
    {
        WWW itemsData = new WWW("https://corretin.000webhostapp.com/Codigos.php");
        yield return itemsData;
        string itemsDataString = itemsData.text;
        print(itemsDataString);
        items = itemsDataString.Split(';');
        print(GetDataValue(items[0], "Codigo:"));
        GetComponent<DataLoader>().enabled = false;
        string lastItem = items[items.Length - 2];
        codigo.text = lastItem;
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }
}
