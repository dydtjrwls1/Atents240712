using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : RecycleObject
{
    public AnimationCurve movement;
    public AnimationCurve fade;

    public float duration = 1.5f;
    public float verticalMovement = 2.0f;

    TextMeshPro damageTextGUI;

    Color color;

    float baseHeight;

    float elapsedTime = 0.0f;

    private void Awake()
    {
        damageTextGUI = GetComponent<TextMeshPro>();
        color = damageTextGUI.color;
    }

    protected override void OnReset()
    {
        elapsedTime = 0.0f;
        baseHeight = transform.position.y;

        DisableTimer(duration);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float progressRatio = elapsedTime / duration; // 진행 정도 변수 (비율 : 0 ~ 1)

        // alpha 값 변경 
        UpdateAlpha(progressRatio);

        // 위치 변경
        UpdateHeight(progressRatio);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    void UpdateAlpha(float ratio)
    {
        float alpha = fade.Evaluate(ratio);
        color.a = alpha;
        damageTextGUI.color = color;
    }

    void UpdateHeight(float ratio)
    {
        float height = baseHeight + movement.Evaluate(ratio) * verticalMovement;
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }

    public void SetDamageText(int damage)
    {
        damageTextGUI.text = damage.ToString();
    }
}
