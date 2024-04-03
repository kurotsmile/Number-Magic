using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_rank : MonoBehaviour
{
    public Image icon;
    public Image icon_rank_index;
    public Text txt_name;
    public Text txt_timer;
    public Image img_avatar;
    public string s_user_id;
    public string s_user_lang;

    public void click()
    {
        GameObject.Find("Game").GetComponent<Game>().show_user_by_id(s_user_id, s_user_lang);
    }
}
