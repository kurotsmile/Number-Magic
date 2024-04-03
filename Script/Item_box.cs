using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_box : MonoBehaviour
{
    public Image icon;
    public Text txt_name;
    public int type = 0;
    public string url;
    public void click()
    {
        Application.OpenURL(this.url);
    }
}
