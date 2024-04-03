using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group_number : MonoBehaviour
{
    public GameObject[] Obj_Number;
    public GameObject btn_change_number;
    private GameObject[] Obj_Number_change;
    private float speed_change = 1.2f;
    private float timer_change = 0f;
    private bool is_run_change = false;
    public GameObject img_action_switch;
    public void OnMouseDown()
    {
        if (this.is_run_change == false)
        {
            this.Obj_Number_change = new GameObject[4];
            this.Obj_Number_change[0] = Instantiate(Obj_Number[0]);
            this.Obj_Number_change[1] = Instantiate(Obj_Number[1]);
            this.Obj_Number_change[2] = Instantiate(Obj_Number[2]);
            this.Obj_Number_change[3] = Instantiate(Obj_Number[3]);

            this.Obj_Number_change[0].name = Obj_Number[0].name;
            this.Obj_Number_change[1].name = Obj_Number[1].name;
            this.Obj_Number_change[2].name = Obj_Number[2].name;
            this.Obj_Number_change[3].name = Obj_Number[3].name;

            this.Obj_Number_change[0].transform.SetParent(this.transform.parent);
            this.Obj_Number_change[1].transform.SetParent(this.transform.parent);
            this.Obj_Number_change[2].transform.SetParent(this.transform.parent);
            this.Obj_Number_change[3].transform.SetParent(this.transform.parent);

            this.Obj_Number_change[0].transform.localScale = Obj_Number[0].transform.localScale;
            this.Obj_Number_change[1].transform.localScale = Obj_Number[1].transform.localScale;
            this.Obj_Number_change[2].transform.localScale = Obj_Number[2].transform.localScale;
            this.Obj_Number_change[3].transform.localScale = Obj_Number[3].transform.localScale;

            this.Obj_Number_change[0].transform.localPosition = Obj_Number[0].transform.localPosition;
            this.Obj_Number_change[1].transform.localPosition = Obj_Number[1].transform.localPosition;
            this.Obj_Number_change[2].transform.localPosition = Obj_Number[2].transform.localPosition;
            this.Obj_Number_change[3].transform.localPosition = Obj_Number[3].transform.localPosition;

            this.Obj_Number[1].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[0].GetComponent<SpriteRenderer>().sprite;
            this.Obj_Number[2].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[1].GetComponent<SpriteRenderer>().sprite;
            this.Obj_Number[3].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[2].GetComponent<SpriteRenderer>().sprite;
            this.Obj_Number[0].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[3].GetComponent<SpriteRenderer>().sprite;

            this.Obj_Number[1].transform.localScale = this.Obj_Number_change[0].transform.localScale;
            this.Obj_Number[2].transform.localScale = this.Obj_Number_change[1].transform.localScale;
            this.Obj_Number[3].transform.localScale = this.Obj_Number_change[2].transform.localScale;
            this.Obj_Number[0].transform.localScale = this.Obj_Number_change[3].transform.localScale;

            this.Obj_Number[1].name = this.Obj_Number_change[0].name;
            this.Obj_Number[2].name = this.Obj_Number_change[1].name;
            this.Obj_Number[3].name = this.Obj_Number_change[2].name;
            this.Obj_Number[0].name = this.Obj_Number_change[3].name;

            this.Obj_Number[0].gameObject.SetActive(false);
            this.Obj_Number[1].gameObject.SetActive(false);
            this.Obj_Number[2].gameObject.SetActive(false);
            this.Obj_Number[3].gameObject.SetActive(false);
            this.transform.parent.GetComponent<Level_game>().lock_all_group_number(false,this.gameObject.name);

            GameObject.Find("Game").GetComponent<Game>().effect_switch_click(this.transform.localPosition);

            this.is_run_change = true;
        }
    }

    private void Update()
    {
        if (this.is_run_change)
        {
            this.Obj_Number_change[0].transform.Translate(Vector3.right * this.speed_change * Time.deltaTime);
            this.Obj_Number_change[1].transform.Translate(Vector3.down * this.speed_change * Time.deltaTime);
            this.Obj_Number_change[2].transform.Translate(Vector3.left * this.speed_change * Time.deltaTime);
            this.Obj_Number_change[3].transform.Translate(Vector3.up * this.speed_change * Time.deltaTime);
            this.timer_change = this.timer_change + 0.8f * Time.deltaTime;
            if (this.timer_change >= 0.6f)
            {
                this.timer_change = 0f;
                this.is_run_change = false;
                this.change_done();
            }
        }
    }

    private void change_done()
    {
        this.Obj_Number[0].gameObject.SetActive(true);
        this.Obj_Number[1].gameObject.SetActive(true);
        this.Obj_Number[2].gameObject.SetActive(true);
        this.Obj_Number[3].gameObject.SetActive(true);

        Destroy(this.Obj_Number_change[0]);
        Destroy(this.Obj_Number_change[1]);
        Destroy(this.Obj_Number_change[2]);
        Destroy(this.Obj_Number_change[3]);
        this.transform.parent.GetComponent<Level_game>().lock_all_group_number(true,this.name);
    }

    public void lock_action(bool is_lock,string name_group)
    {
        this.GetComponent<CircleCollider2D>().enabled = is_lock;
        this.img_action_switch.SetActive(false);
        if (this.name == name_group&&is_lock==false) this.img_action_switch.SetActive(true);
        this.btn_change_number.SetActive(is_lock);
        this.btn_change_number.GetComponent<Animator>().enabled = is_lock;
    }
}
