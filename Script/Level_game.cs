using UnityEngine;

public class Level_game : MonoBehaviour
{
    public int id_level;
    public string Name_level;
    public GameObject[] Number_obj;
    public Group_number[] group_number;
    public Texture2D bk_level;
    public void load_game()
    {
        int[] arr_number = new int[this.Number_obj.Length];
        for (int i = 0; i < this.Number_obj.Length; i++)
        {
            arr_number[i] = i;
        }

        for (int i = 0; i < arr_number.Length; i++)
        {
            int rnd = Random.Range(0, arr_number.Length);
            int tempGO = arr_number[rnd];
            arr_number[rnd] = arr_number[i];
            arr_number[i] = tempGO;
        }

        Sprite[] arr_pic_number = GameObject.Find("Game").GetComponent<Game>().pic_Number;
        for (int i = 0; i < Number_obj.Length; i++)
        {
            this.Number_obj[i].GetComponent<SpriteRenderer>().sprite = arr_pic_number[arr_number[i]];
            this.Number_obj[i].name = arr_number[i].ToString();
        }
    }

    public void lock_all_group_number(bool is_lock,string name_group)
    {
        if (is_lock)
        {
            GameObject.Find("Game").GetComponent<Game>().play_sound(0);
        }
        else
        {
            this.check_done();
        }
        for(int i = 0; i < this.group_number.Length; i++)
        {
            this.group_number[i].lock_action(is_lock, name_group);
        }
    }

    public void check_done()
    {
        Sprite[] arr_pic_number = GameObject.Find("Game").GetComponent<Game>().pic_Number;
        Sprite[] arr_pic_number_true = GameObject.Find("Game").GetComponent<Game>().pic_true;
        int count_true = 0;
        for (int i = 0; i < Number_obj.Length; i++)
        {
            int index_pic =int.Parse(this.Number_obj[i].name);
            if (i==index_pic)
            {
                this.Number_obj[i].GetComponent<SpriteRenderer>().sprite = arr_pic_number_true[index_pic];
                count_true = count_true + 1;
            }
            else
            {
                this.Number_obj[i].GetComponent<SpriteRenderer>().sprite = arr_pic_number[index_pic];
            }

        }

        if (count_true >= this.Number_obj.Length)
        {
            GameObject.Find("Game").GetComponent<Game>().game_victory();
        }
    }
}
