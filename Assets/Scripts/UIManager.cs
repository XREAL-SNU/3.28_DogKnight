using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
