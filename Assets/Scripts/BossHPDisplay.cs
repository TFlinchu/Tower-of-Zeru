using UnityEngine;
using TMPro;

public class BossHPDisplay : MonoBehaviour
{
    public Transform bossTransform;
    public TextMeshProUGUI hpText;

    // Update is called once per frame
    void Update()
    {
        if (bossTransform != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(bossTransform.position);
            screenPos.y -= 75;
            screenPos.x += 50;
            hpText.transform.position = screenPos;

            Skeleton bossSkeleton = bossTransform.GetComponent<Skeleton>();
            float bossHP = bossSkeleton.hp;

            hpText.text = bossHP.ToString();
        }
    }
}
