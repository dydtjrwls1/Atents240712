using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour
{
    // 비활성화 되었을 때의 색상
    public Color disableColor;

    Image[] lifeImages;

    private void Awake()
    {
        lifeImages = new Image[transform.childCount];
        for(int i = 0; i < lifeImages.Length; i++)
        {
            Transform child = transform.GetChild(i);
            lifeImages[i] = child.GetComponent<Image>();
        }
    }

    /// <summary>
    /// 초기화 될 때 실행될 함수
    /// </summary>
    public void OnInitialize()
    {
        Player player = GameManager.Instance.Player;
        player.onLifeChange += OnLifeChange;
    }

    /// <summary>
    /// Life 가 변경되었을 때 실행되는 함수
    /// </summary>
    /// <param name="life">현재 life</param>
    private void OnLifeChange(int life)
    {
        for(int i = 0; i < life; i++)
        {
            lifeImages[i].color = Color.white;  // 남아있는 생명은 정상적으로 보이기
        }
        for(int i = life; i < lifeImages.Length; i++)
        {
            lifeImages[i].color = disableColor; // 날아간 생명은 비활성화된 색으로 보이게 하기
        }
    }
}
