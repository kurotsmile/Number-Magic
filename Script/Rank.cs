using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public Sprite icon_list_history;
    public Sprite icon_list_rank;
    public Sprite[] icon_level;
    public Sprite[] icon_rank_index;
    private int length = 0;
    public GameObject prefab_item_history;
    public GameObject prefab_item_rank;
    public Color32 color_me;

    private Carrot.Carrot_Box box_history_and_rank;
    void Start()
    {
        this.length = PlayerPrefs.GetInt("rank_length", 0);
    }

    public void show_rank()
    {
        if (length == 0)
        {
            this.GetComponent<Game>().carrot.Show_msg("Achievement history", "You do not have play rankings",Carrot.Msg_Icon.Alert);
        }
        else
        {
            this.box_history_and_rank=this.GetComponent<Game>().carrot.Create_Box("Achievement history", this.icon_list_history);
            for(int i = this.length-1; i>=0; i--)
            {
                if (PlayerPrefs.GetString("rank_name_" + i) != "")
                {
                    GameObject item_rank_obj = Instantiate(this.prefab_item_history);
                    item_rank_obj.transform.SetParent(this.box_history_and_rank.area_all_item);
                    item_rank_obj.transform.localPosition = new Vector3(item_rank_obj.transform.localPosition.x, item_rank_obj.transform.localPosition.y, 0f);
                    item_rank_obj.transform.localScale = new Vector3(1f, 1f, 1f);
                    int id_rank = PlayerPrefs.GetInt("rank_id_" + i, 0);
                    item_rank_obj.GetComponent<Item_rank>().icon.sprite = this.get_icon_leve_by_id(id_rank);
                    item_rank_obj.GetComponent<Item_rank>().txt_name.text = PlayerPrefs.GetString("rank_name_" + i);
                    item_rank_obj.GetComponent<Item_rank>().txt_timer.text = this.GetComponent<Game>().timer_to_time(PlayerPrefs.GetFloat("rank_value_" + i));
                }
            }
        }
    }

    public void add_rank(int id_rank,string name_rank,float rank_value)
    {
        PlayerPrefs.SetInt("rank_id_" + this.length, id_rank);
        PlayerPrefs.SetString("rank_name_" + this.length, name_rank);
        PlayerPrefs.SetFloat("rank_value_" + this.length, rank_value);
        this.length = this.length + 1;
        PlayerPrefs.SetInt("rank_length", this.length);
        this.upload_history(id_rank, rank_value);
    }

    private void upload_history(int id_rank,float time_rank)
    {
        string id_user_login = this.GetComponent<Game>().carrot.user.get_id_user_login();
        if (id_user_login != "")
        {
            WWWForm frm = this.GetComponent<Game>().carrot.frm_act("upload_rank");
            frm.AddField("id_rank", id_rank);
            frm.AddField("timer_rank", time_rank.ToString());
            frm.AddField("user_id", id_user_login);
            frm.AddField("user_lang", this.GetComponent<Game>().carrot.user.get_lang_user_login());
            this.GetComponent<Game>().carrot.send_hide(frm, act_upload_rank_done);
        }
    }

    private void act_upload_rank_done(string s_data){}

    public void show_list_rank()
    {
        this.GetComponent<Game>().carrot.stop_all_act();
        WWWForm frm= this.GetComponent<Game>().carrot.frm_act("list_rank");
        this.GetComponent<Game>().carrot.send(frm, act_show_list_rank);
    }

    private void act_show_list_rank(string s_data)
    {
        string id_user_login = this.GetComponent<Game>().carrot.user.get_id_user_login();
        IList list_rank = (IList)Carrot.Json.Deserialize(s_data);
        this.box_history_and_rank=this.GetComponent<Game>().carrot.Create_Box("The game score rankings", this.icon_list_rank);
        for (int i = 0; i < list_rank.Count; i++)
        {
            IDictionary data_rank = (IDictionary)list_rank[i];
            GameObject item_rank_obj = Instantiate(this.prefab_item_rank);
            item_rank_obj.transform.SetParent(this.box_history_and_rank.area_all_item);
            item_rank_obj.transform.localPosition = new Vector3(item_rank_obj.transform.localPosition.x, item_rank_obj.transform.localPosition.y, 0f);
            item_rank_obj.transform.localScale = new Vector3(1f, 1f, 1f);
            item_rank_obj.GetComponent<Item_rank>().s_user_id = data_rank["id_device"].ToString();
            item_rank_obj.GetComponent<Item_rank>().s_user_lang = data_rank["lang"].ToString();
            item_rank_obj.GetComponent<Item_rank>().txt_name.text = data_rank["name"].ToString();
            item_rank_obj.GetComponent<Item_rank>().txt_timer.text = this.GetComponent<Game>().timer_to_time(float.Parse(data_rank["rank_time"].ToString()));
            int id_rank = int.Parse(data_rank["rank_id"].ToString());
            item_rank_obj.GetComponent<Item_rank>().icon.sprite = this.get_icon_leve_by_id(id_rank);
            if (i < this.icon_rank_index.Length)
                item_rank_obj.GetComponent<Item_rank>().icon_rank_index.sprite = this.icon_rank_index[i];
            else
                item_rank_obj.GetComponent<Item_rank>().icon_rank_index.sprite = this.icon_rank_index[4];
            if (data_rank["avatar"].ToString()!="")this.GetComponent<Game>().carrot.get_img(data_rank["avatar"].ToString(), item_rank_obj.GetComponent<Item_rank>().img_avatar);
            if (data_rank["id_device"].ToString() == id_user_login) item_rank_obj.GetComponent<Image>().color = color_me;
        }
    }

    private Sprite get_icon_leve_by_id(int id_level)
    {
        if (id_level == 0)
            return this.icon_level[0];
        else
            return this.icon_level[id_level - 1];
    }
}
