using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionSuccess : MonoBehaviour
{
    public Transform successMessage;
    public Image panel, title;

    public Transform[] rewardPos;
    public Transform[] valuePos;
    public TextMeshProUGUI[] rewardText;
    public TextMeshProUGUI[] valueText;
    bool startShowingMessage;

    float rewardMessageSize = 2;
    float rewardMessageSpeed = 2f;

    public GameObject confetti;
    private void OnEnable()
    {
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
        title.color = new Color(title.color.r, title.color.g, title.color.b, 0);

        startShowingMessage = true;
    }
    private void FixedUpdate()
    {
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, panel.color.a + Mathf.Clamp(Time.fixedDeltaTime * 2.5f, 0, 1));
        title.color = new Color(title.color.r, title.color.g, title.color.b, title.color.a + Mathf.Clamp(Time.fixedDeltaTime * 2.5f, 0, 1));

        if (startShowingMessage)
        {
            rewardMessageSize -= (rewardMessageSpeed * Time.fixedDeltaTime);
            rewardMessageSpeed += rewardMessageSpeed * Time.fixedDeltaTime;
            successMessage.localScale = new Vector2(rewardMessageSize, rewardMessageSize);
            if(rewardMessageSize <= 1)
            {
                startShowingMessage = false;

                Instantiate(confetti, new Vector2(transform.position.x - 3, transform.position.y + 2), transform.rotation);
                Instantiate(confetti, new Vector2(transform.position.x + 3, transform.position.y + 2), transform.rotation);
            }
        }

    }
}
