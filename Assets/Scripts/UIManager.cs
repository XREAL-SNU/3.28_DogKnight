using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance()
    {
        return _instance;
    }

    public Text roundText;
    public Slider playerHpBar;
    public Slider enemyHpBar;
    public GameObject GameOverImage;

    public Button InvenBtn;
    public GameObject Inventory;

    private Player player;
    private Enemy enemy;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; //instance할당해주기
            DontDestroyOnLoad(this.gameObject); // Scene 이 바뀌어도 유지
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject); //여러개인 경우 삭제
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerHpBar.minValue = 0;
        playerHpBar.maxValue = player._myHp;
        playerHpBar.value = player._myHp;
        enemyHpBar.minValue = 0;
        enemyHpBar.maxValue = enemy._myHp;
        enemyHpBar.value = enemy._myHp;

        //Inventory = GameObject.FindGameObjectWithTag("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SliderBarUpdate(Character character)
    {
        StartCoroutine(DelaySliderBar(character));
    }

    public IEnumerator DelaySliderBar(Character character)
    {
        yield return new WaitForSeconds(1.5f);

        if (character == enemy)
            enemyHpBar.value = enemy._myHp;
        else
            playerHpBar.value = player._myHp;
    }
    public void GameOverUpdate(string whoseTurn)
    {
        GameOverImage.GetComponentInChildren<Text>().text = $"{ whoseTurn} is Win!";
    }
    
    public void OpenInven()
    {
        Inventory.SetActive(true);
    }

    public void CloseInven()
    {
        Inventory.SetActive(false);
    }

    public void DamageSkill()
    {
        player.isSkilled = true;
        //방금 클릭한 게임오브젝트 가져오기
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        CloseInven();
        Destroy(clickedObject);
    }
    public void HealSkill()
    {
        enemy.isSkilled = true;
        //방금 클릭한 게임오브젝트 가져오기
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        CloseInven();
        Destroy(clickedObject);
    }

    public void InvenBtnActive(string whoseTurn)
    {
        StartCoroutine(ButtonDisableCoroutine(whoseTurn));
    }

    IEnumerator ButtonDisableCoroutine(string whoseTurn)
    {
        InvenBtn.interactable = false;

        if (whoseTurn=="player")
            InvenBtn.interactable = false;
        else
            InvenBtn.interactable = true;
        yield return new WaitForSeconds(2.5f);

    }
}
