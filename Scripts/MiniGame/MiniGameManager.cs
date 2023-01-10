using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    /*
     *  <미니게임 실행 순서>
     *  MiniGameManager Awake Method (최초 1회)
     *  MiniGame Awake Method (최초 1회)
     *      -> MiniGame의 자식들에서도 Awake (최초 1회, GetComponent, new등 할당 작업)
     *  MiniGame SettingBeforeStartGame Method
     *      -> MiniGame의 자식들에서 SetDefaults (게임 실행전 수행할 기본 세팅들, 초기화)
     *  MiniGameManager CountDown Coroutine
     *  MiniGame StartMiniGame Method
     *  
     *  ~~GamePlay~~
     *  
     *  MiniGame EndMiniGame Method
     *  MiniGameManager ShowButtons Method -> 테스트용 버튼 생성기
     */

    #region PublicMethod
    void Awake()
    {
        instance = this;

        SetDefaults();
    }
    public void MiniGameChooseEvent() // 버튼 클릭, 미니게임 선택 이벤트, 메인게임 화면에서 클릭해서 킬 예정
    {
        SetDefaults();

        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;

        int miniGameIndex = int.Parse(clickedButton.gameObject.name);

        StartCoroutine(StartCountDown(miniGameIndex));
    }

    //for test
    public void ShowButtons()
    {
        testCanvas.gameObject.SetActive(true);
    }

    #endregion

    #region PublicVariable
    public static MiniGameManager instance = null;
    #endregion

    #region PrivateVariable
    [SerializeField] MiniGame[] m_miniGames;
    [SerializeField] Image m_countDownImage;
    [SerializeField] Sprite[] m_countDownSprite;

    [Header("For Test")]
    [SerializeField] Canvas testCanvas;
    #endregion

    #region PrivateMethod
    void SetDefaults()
    {
        m_countDownImage.gameObject.SetActive(false);
    }
    IEnumerator StartCountDown(int _miniGameIdx)
    {
        //for test
        testCanvas.gameObject.SetActive(false);

        MiniGame selectedMiniGame = m_miniGames[_miniGameIdx].GetComponent<MiniGame>();

        // 로딩 필요?
        selectedMiniGame.gameObject.SetActive(true);

        selectedMiniGame.SettingBeforeStartGame();

        yield return new WaitForSeconds(1.5f);

        m_countDownImage.gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            m_countDownImage.sprite = m_countDownSprite[i];

            yield return new WaitForSeconds(1);
        }

        m_countDownImage.gameObject.SetActive(false);
        selectedMiniGame.StartMiniGame();
    }
    #endregion
}
