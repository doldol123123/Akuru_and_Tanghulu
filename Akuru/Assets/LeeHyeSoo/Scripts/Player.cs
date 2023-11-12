using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public List<Fruit> fruits; //과일을 정보 담을 리스트 

    public float akuruMakingTime; //아쿠루 손질 시간
    //public Text timeText;
    
    public Slider akuruSlider; //아쿠루 과일 손질 슬라이더

    Animator anim;
    

    public bool isMaking;

    [Header("냄비")]
    public PotInventory potInventory; // 냄비

    [Header("랜덤과일 인덱스 번호")]
    public int i;

    UnlockFreezer unlockFreezer;
    public bool isAkuruWorking;


    private void Start()
    {
        //timeText = GameObject.Find(name: "SliderText").GetComponent<Text>();
        akuruSlider = GameObject.Find(name: "AkuruSlider").GetComponent<Slider>();
        potInventory = FindAnyObjectByType<PotInventory>();
        unlockFreezer = FindAnyObjectByType<UnlockFreezer>();
        anim = GetComponent<Animator>();
        

        akuruMakingTime = 0;
        isMaking = false;
        isAkuruWorking = false;
        i = 0;

    }

    void Update()
    {
        
        if (!isMaking && !potInventory.isPotFull )
        {

            RandomSelectTangfuru();
            anim.SetBool("isMakingTfr", false);
            isMaking = true;

        }
        else if(isMaking && !potInventory.isPotFull)
        {

            if (GameManager.instance.fruitNumList[i] > 0) // 선택된 과일의 수량이 있으면
            {
                AkuruMakingTangfuru();
            }
            


        }
        
        
    }

    //랜덤 과일 인덱스 선택
    void RandomSelectTangfuru() 
    {
        //냉장고 해금도로 탕후루 랜덤 범위 조절
        int fruitNum = 0;
        for (int i = 0; i < fruits.Count; i++)
        {
            if (unlockFreezer.lockFreezer[i] == false)
            {
                fruitNum = i + 1;
                break;
            }
            else if (unlockFreezer.lockFreezer[3] == true)
            {
                fruitNum = 3 + 1;
                break;
            }
        }


        //랜덤한 과일선택
        if (fruitNum == 0)
        {
            i = 0;
        }
        else
        {
            i = Random.Range(0, fruitNum);
        }


    }
    void AkuruMakingTangfuru() //아쿠루 과일 손질
    {
        isAkuruWorking = true;
        akuruMakingTime += Time.deltaTime;
        //timeText.text = Mathf.FloorToInt(akuruMakingTime).ToString() + " S";
        akuruSlider.maxValue = fruits[i].making_time;
        akuruSlider.value = akuruMakingTime;
        anim.SetBool("isMakingTfr", true);

        if (akuruMakingTime >= fruits[i].making_time)
        {
            GoToPot();
            GameManager.instance.fruitNumList[i] -= 1;
            akuruMakingTime = 0;
            isMaking = false;
            isAkuruWorking = false;
        }
    }

    //작성중
    void GoToPot()
    {
        if (!potInventory.isPotFull) //냄비인벤토리가 차지 않았다면
        {
            potInventory.AddFruit(fruits[i]);
            //만들어 둔 냄비인벤토리에 과일을 넣어 준다 
        }
        

    }

    
}
