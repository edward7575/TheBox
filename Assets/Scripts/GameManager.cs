using System.Collections;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //定数定義
    public const int WALL_FRONT = 1;  //前
    public const int WALL_RIGHT = 2;  //右
    public const int WALL_BACK = 3;  //後
    public const int WALL_LEFT = 4;  //左

    //定数定義
    public const int COLOR_GREEN = 0; //緑
    public const int COLOR_RED = 1; //赤
    public const int COLOR_BLUE = 2; //青
    public const int COLOR_WHITE = 3; //白

    public GameObject panelWalls;   //壁全体

    public GameObject buttonHammer;   //ボタン：トンカチ
    public GameObject buttonKey;  //ボタン：鍵

    public GameObject buttonPig; //ボタン：豚の貯金箱

    public GameObject imageHammerIcon; //アイコン：トンカチ
    public GameObject imageKeyIcon; //アイコン：鍵

    public GameObject buttonMessage;  //ボタン：メッセージ
    public GameObject buttonMessageText;  //メッセージテキスト

    public GameObject[] buttonLamp = new GameObject[3]; //ボタン：金庫

    public Sprite[] buttonPicture = new Sprite[4]; //ボタンの絵

    public Sprite hammerPicture; //トンカチの絵
    public Sprite keyPicture; //鍵の絵


    private int wallNo;  //現在向いている方向
    private bool doesHaveHammer;      //トンカチを持っているか
    private bool doesHaveKey;    //鍵を持っているか
    private int[] buttonColor = new int[3]; //金庫のボタン


	// Use this for initialization
	void Start () {
        wallNo = WALL_FRONT; //スタート時点で前を向く
        doesHaveHammer = false; //トンカチは持っていない
        doesHaveKey = false; //鍵を持っていない

        buttonColor[0] = COLOR_GREEN;  //ボタン１の色は「緑」
        buttonColor[1] = COLOR_RED;  //ボタン２の色は「赤」
        buttonColor[2] = COLOR_BLUE;  //ボタン３の色は「青」
	}

    // Update is called once per frame
    void Update() {
    
    }

    //ボックスをタップ
    public void PushButtonBox () {
        if (doesHaveKey == false) {
            //鍵を持っていない
            DisplayMessage("鎖撚左阿屌！");
        }else{
            //鍵を持っている
            SceneManager.LoadScene("ClearScene");
        }
    }

    //メモをタップ 
    public void PushButtonMemo () {
        DisplayMessage ("寫住艾菲爾鐵塔阿睇字啦on9！ ");
    }

    //貯金箱をタップ
    public void PushButtonPig () {
        //トンカチを持っているか
        if (doesHaveHammer == false) {
            //トンカチを持っていない
            DisplayMessage("你唔係諗住用手下話傻閪！");
        }else{
            //トンカチを持っている
            DisplayMessage("哇屌，條匙點撚樣放入去架？");
            buttonPig.SetActive(false);  //貯金箱を消す
            buttonKey.SetActive(true);  //鍵の絵を表示
            imageKeyIcon.GetComponent<Image>().sprite = keyPicture;

            doesHaveKey = true;
        }
    }

    //トンカチの絵をタップ
    public void PushButtonHammer () {
        buttonHammer.SetActive(false);
    }

    //鍵の絵をタップ
    public void PushButtonKey () {
        buttonKey.SetActive(false);
    }

    //メッセージをタップ
    public void PushButtonMessage () {
        buttonMessage.SetActive (false);
    }

        //右(>)ボタンを押した
        public void PushButtonRight () {
            wallNo++;  //方向を１つ右に
            //「左」の１つ右は「前」
            if (wallNo > WALL_LEFT) {
            wallNo = WALL_FRONT;
            }
    DisplayWall (); //壁表示更新a
        ClearButtons(); //いらないものを消す
        }

    //金庫のボタンを１をタップ
    public void PushButtonLamp1 () {
        ChangeButtonColor(0);
    }

    //金庫のボタンを２をタップ
    public void PushBottonLamp2 (){
        ChangeButtonColor(1);
    }

    //金庫のボタンを3をタップ
    public void PushBottonLamp3 (){
        ChangeButtonColor(2);
    }

    //金庫のボタンの色を変更
    void ChangeButtonColor (int buttonNo) {
        buttonColor [buttonNo]++;
        //「白」の時にボタンを押したら緑に
        if (buttonColor [buttonNo] > COLOR_WHITE) {
            buttonColor[buttonNo] = COLOR_GREEN;
        }
        //ボタンの画像の変更
        buttonLamp[buttonNo].GetComponent<Image>().sprite =
            buttonPicture[buttonColor[buttonNo]];

        //ボタンの色順をチェック
        if ((buttonColor [0] == COLOR_BLUE) &&
            (buttonColor [1] == COLOR_WHITE) &&
            (buttonColor [2] == COLOR_RED)) {
            //まだトンカチをもっていない？
            if (doesHaveHammer == false) {
                DisplayMessage("痴撚線！放個鎚入金庫！");
                buttonHammer.SetActive(true); //トンカチの絵を表示
                imageHammerIcon.GetComponent<Image>().sprite = hammerPicture;

                doesHaveHammer = true;
            }
        }
           
    }


    //左(<)ボタンを押した
    public void PushButtonLeft () {
        wallNo--; //方向を1つ左に
        //「前」の１つ左は「左」
        if (wallNo < WALL_FRONT) {
            wallNo = WALL_LEFT;
        }
        DisplayWall (); //壁表示更新
        ClearButtons();


    }

    //各種表示をクリア
    void ClearButtons () {
        buttonHammer.SetActive(false);
        buttonKey.SetActive(false);
        buttonMessage.SetActive(false);
    }

    //メッセージを表示
    void DisplayMessage(string mes){
        buttonMessage.SetActive(true);
        buttonMessageText.GetComponent<Text> ().text = mes;
    }

    //向いてる方法の壁表示
    void DisplayWall() {
        switch (wallNo) {
            case WALL_FRONT: //前
                panelWalls.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case WALL_RIGHT: //右
                panelWalls.transform.localPosition = new Vector3(-1000.0f, 0.0f, 0.0f);
                break;
            case WALL_BACK: //後
                panelWalls.transform.localPosition = new Vector3(-2000.0f, 0.0f, 0.0f);
                break;
            case WALL_LEFT:  //左
                panelWalls.transform.localPosition = new Vector3(-3000.0f, 0.0f, 0.0f);
                break;

        }
    }

	}

