using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swicth_number : MonoBehaviour
{

    public GameObject[] number_changer;
    private GameObject[] Obj_Number_change;
    private float speed_change = 1.2f;
    private float timer_change = 0f;
    private bool is_run_change = false;
    public void OnMouseDown()
    {
        if (this.is_run_change == false)
        {
            this.Obj_Number_change = new GameObject[number_changer.Length];

            for (int i = 0; i < this.number_changer.Length; i++)
            {
                this.Obj_Number_change[i] = Instantiate(number_changer[i]);
                if (this.Obj_Number_change[i].GetComponent<swicth_number>())
                {
                    Destroy(this.Obj_Number_change[0].GetComponent<swicth_number>());
                    Destroy(this.Obj_Number_change[0].GetComponent<CircleCollider2D>());
                }

                this.Obj_Number_change[i].name = number_changer[i].name;
                this.Obj_Number_change[i].transform.SetParent(this.transform.parent);
                this.Obj_Number_change[i].transform.localScale = number_changer[i].transform.localScale;
                this.Obj_Number_change[i].transform.localPosition = number_changer[i].transform.localPosition;
                this.number_changer[i].gameObject.SetActive(false);
            }

            this.number_changer[1].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[0].GetComponent<SpriteRenderer>().sprite;
            this.number_changer[2].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[1].GetComponent<SpriteRenderer>().sprite;
            this.number_changer[3].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[2].GetComponent<SpriteRenderer>().sprite;
            this.number_changer[4].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[3].GetComponent<SpriteRenderer>().sprite;
            this.number_changer[5].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[4].GetComponent<SpriteRenderer>().sprite;
            this.number_changer[6].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[5].GetComponent<SpriteRenderer>().sprite;
            this.number_changer[7].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[6].GetComponent<SpriteRenderer>().sprite;
            this.number_changer[0].GetComponent<SpriteRenderer>().sprite = this.Obj_Number_change[7].GetComponent<SpriteRenderer>().sprite;


            this.number_changer[1].name = this.Obj_Number_change[0].name;
            this.number_changer[2].name = this.Obj_Number_change[1].name;
            this.number_changer[3].name = this.Obj_Number_change[2].name;
            this.number_changer[4].name = this.Obj_Number_change[3].name;
            this.number_changer[5].name = this.Obj_Number_change[4].name;
            this.number_changer[6].name = this.Obj_Number_change[5].name;
            this.number_changer[7].name = this.Obj_Number_change[6].name;
            this.number_changer[0].name = this.Obj_Number_change[7].name;

            //this.transform.parent.GetComponent<Level_game>().lock_all_group_number(false, this.gameObject.name);

            this.is_run_change = true;
        }
    }

    private void Update()
    {
        if (this.is_run_change)
        {
            this.Obj_Number_change[0].transform.Translate(Vector3.right * this.speed_change * Time.deltaTime);
            this.Obj_Number_change[1].transform.Translate(Vector3.right * this.speed_change * Time.deltaTime);
            this.Obj_Number_change[2].transform.Translate(Vector3.down * this.speed_change * Time.deltaTime);
            this.Obj_Number_change[3].transform.Translate(Vector3.down * this.speed_change * Time.deltaTime);
            this.Obj_Number_change[4].transform.Translate(Vector3.left * this.speed_change * Time.deltaTime);
            this.Obj_Number_change[5].transform.Translate(Vector3.left * this.speed_change * Time.deltaTime);
            this.Obj_Number_change[6].transform.Translate(Vector3.up * this.speed_change * Time.deltaTime);
            this.Obj_Number_change[7].transform.Translate(Vector3.up * this.speed_change * Time.deltaTime);
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
        this.number_changer[0].gameObject.SetActive(true);
        this.number_changer[1].gameObject.SetActive(true);
        this.number_changer[2].gameObject.SetActive(true);
        this.number_changer[3].gameObject.SetActive(true);
        this.number_changer[4].gameObject.SetActive(true);
        this.number_changer[5].gameObject.SetActive(true);
        this.number_changer[6].gameObject.SetActive(true);
        this.number_changer[7].gameObject.SetActive(true);

        Destroy(this.Obj_Number_change[0]);
        Destroy(this.Obj_Number_change[1]);
        Destroy(this.Obj_Number_change[2]);
        Destroy(this.Obj_Number_change[3]);
        Destroy(this.Obj_Number_change[4]);
        Destroy(this.Obj_Number_change[5]);
        Destroy(this.Obj_Number_change[6]);
        Destroy(this.Obj_Number_change[7]);
       // this.transform.parent.GetComponent<Level_game>().lock_all_group_number(true, this.name);
    }
}
