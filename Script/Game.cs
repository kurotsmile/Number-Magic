using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Carrot.Carrot carrot;

    [Header("Panel Game")]
    public GameObject panel_home;
    public GameObject panel_play;
    public GameObject panel_pause;
    public GameObject panel_mission_complete;
    public GameObject panel_help;
    public GameObject btn_buy_ads;
    public GameObject btn_buy_ads2;

    [Header("Object Source")]
    public Sprite[] pic_Number;
    public Sprite[] pic_true;
    public AudioSource[] sound;

    [Header("Object Game play")]
    public Level_game[] game_level;
    private Level_game level_curent;
    public Text txt_timer_count;
    private float timer;
    private bool timeStarted = true;
    public Skybox skybox;
    public Text txt_play_title;
    private float timer_complete = 0f;
    private bool timer_complete_started = false;

    [Header("Effect game")]
    public GameObject effect_win;
    public GameObject effect_switch;

    void Start()
    {
        this.carrot.Load_Carrot(check_exit_app);
        this.carrot.game.load_bk_music(this.sound[2]);

        this.game_level[0].gameObject.SetActive(false);
        this.game_level[1].gameObject.SetActive(false);
        this.game_level[2].gameObject.SetActive(false);
        this.panel_home.SetActive(true);
        this.panel_play.SetActive(false);
        this.panel_pause.SetActive(false);
        this.panel_mission_complete.SetActive(false);
        this.panel_help.SetActive(false);
    }

    private void check_exit_app()
    {
        if (this.panel_help.activeInHierarchy)
        {
            this.btn_back_home();
            this.carrot.set_no_check_exit_app();
        }
        else if (this.panel_mission_complete.activeInHierarchy)
        {
            this.btn_back_home();
            this.carrot.set_no_check_exit_app();
        }
        else if (this.panel_pause.activeInHierarchy)
        {
            this.game_pause(false);
            this.carrot.set_no_check_exit_app();
        }
        else if (this.panel_play.activeInHierarchy)
        {
            this.btn_back_home();
            this.carrot.set_no_check_exit_app();
        }
    }

    void Update()
    {
        if (this.timeStarted)
        {
            timer += Time.deltaTime;
            this.txt_timer_count.text =this.timer_to_time(this.timer);
        }

        if (this.timer_complete_started)
        {
            this.timer_complete += 0.5f * Time.deltaTime;
            if (this.timer_complete > 1.5f)
            {
                this.timer_complete = 0f;
                this.timer_complete_started = false;
                this.panel_mission_complete.SetActive(true);
            }
        }
    }

    public void play_game_level(int index_level)
    {
        this.timer = 0f;
        this.panel_play.SetActive(true);
        this.panel_home.SetActive(false);
        this.level_curent =this.game_level[index_level];
        this.level_curent.load_game();
        this.level_curent.gameObject.SetActive(true);
        this.txt_play_title.text = this.level_curent.Name_level;
        this.set_skybox_Texture(this.level_curent.bk_level);
        this.play_sound(1);
        this.carrot.ads.show_ads_Interstitial();
    }

    private void set_skybox_Texture(Texture textT)
    {
        Material result = new Material(Shader.Find("RenderFX/Skybox"));
        result.SetTexture("_FrontTex", textT);
        result.SetTexture("_BackTex", textT);
        result.SetTexture("_LeftTex", textT);
        result.SetTexture("_RightTex", textT);
        result.SetTexture("_UpTex", textT);
        result.SetTexture("_DownTex", textT);
        this.skybox.material = result;
    }

    public void btn_back_home()
    {
        this.carrot.ads.show_ads_Interstitial();
        this.panel_home.SetActive(true);
        this.panel_play.SetActive(false);
        this.panel_help.SetActive(false);
        this.panel_mission_complete.SetActive(false);
        this.game_level[0].gameObject.SetActive(false);
        this.game_level[1].gameObject.SetActive(false);
        this.game_level[2].gameObject.SetActive(false);
        this.effect_win.SetActive(false);
        this.play_sound(1);
    }

    public void play_sound(int index)
    {
        if(this.carrot.get_status_sound())this.sound[index].Play();
    }

    public void game_pause(bool is_pause)
    {
        if (is_pause)
        {
            this.panel_pause.SetActive(true);
            this.timeStarted = false;
            this.sound[2].Pause();
        }
        else
        {
            this.panel_pause.SetActive(false);
            this.timeStarted = true;
            this.sound[2].UnPause();
        }
        this.play_sound(1);
    }

    public void game_rate()
    {
        this.play_sound(1);
        this.carrot.show_rate();
    }

    public void effect_switch_click(Vector3 pos)
    {
        this.effect_switch.transform.localPosition = pos;
        this.effect_switch.SetActive(false);
        this.effect_switch.SetActive(true);
    }

    [ContextMenu("Game Victory")]
    public void game_victory()
    {
        this.timer_complete_started = true;
        this.play_sound(3);
        carrot.game.update_scores_player(int.Parse(this.timer.ToString()), this.level_curent.id_level);
        this.effect_win.SetActive(true);
    }


    [ContextMenu("Test add rank")]
    public void test_add_rank()
    {
        carrot.game.update_scores_player(int.Parse(this.timer.ToString()), this.level_curent.id_level);
        Debug.Log("Add rank " + this.level_curent + " Timer :" + this.timer);
    }

    public void show_help()
    {
        this.play_sound(1);
        this.panel_help.SetActive(true);
    }

    public string timer_to_time(float timer)
    {
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");
        return minutes + ":" + seconds;
    }

    public void btn_show_login()
    {
        this.play_sound(1);
        this.carrot.show_login();
    }

    public void buy_product(int index)
    {
        this.play_sound(1);
        this.carrot.shop.buy_product(index);
    }

    public void btn_show_list_app_other()
    {
        this.carrot.stop_all_act();
        this.play_sound(1);
        this.carrot.show_list_carrot_app();
    }

    public void btn_share()
    {
        this.play_sound(1);
        this.carrot.show_share();
    }

    public void show_user_by_id(string s_user_id,string s_user_lang)
    {
        this.carrot.stop_all_act();
        this.play_sound(1);
        this.carrot.user.show_user_by_id(s_user_id, s_user_lang);
    }

    public void show_setting()
    {
        this.play_sound(1);
        this.carrot.Create_Setting();
    }

    public void Btn_show_rank()
    {
        this.play_sound(1);
        this.carrot.game.Show_List_Top_player();
    }
}
