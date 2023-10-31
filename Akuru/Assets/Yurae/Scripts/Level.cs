using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Slider xpBar;
    public Text LV_in_Main;
    public Text LV_in_AdmLV;
    public Text XP;

    /*
     Lv 1 = 3xp
    이후 5LV씩 5부터 1씩 증가분을 가짐
     */
    private int PlayerLevel = 1; //현재 플레이어의 레벨


    private float maxXP = 3; //필요 경험치
    private float curXP = 0; //현재 누적 경험치
    private float increment_XP = 5; //경험치 증가분

    public float test_XP = 3; //테스트용 증가 경험치


    // Start is called before the first frame update
    void Start()
    {
        xpBar.value = (float)curXP / (float)maxXP;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            curXP += test_XP;
            if (curXP >= maxXP)
            {
                curXP = curXP - maxXP; //초과 경험치는 현재 경험치 량에서 필요 겅험치 량을 뺀 양
                LevelUP(); //플레이어 레벨업
                if (PlayerLevel != 1 && PlayerLevel % 5 == 1) //플레이어 레벨이 1이 아니고 5로 나눈 나머지가 1일때 증가분을 1 증가 시킴
                {
                    increment_XP++;
                }
                maxXP += increment_XP;
            }
        }
        HandleXP();
        ShowCurXP();
    }

    //슬라이더 관리 함수
    private void HandleXP()
    {
        xpBar.value = (float)curXP / (float)maxXP;
    }


    //레벨업 관리 함수 
    private void LevelUP()
    {
        PlayerLevel++;
        LV_in_AdmLV.text = PlayerLevel.ToString();
        LV_in_Main.text = PlayerLevel.ToString();
    }

    private void ShowCurXP()
    {
        XP.text = curXP.ToString() + " / " + maxXP.ToString();
    }
}
