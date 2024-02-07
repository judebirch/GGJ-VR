using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    public TMPro.TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Time: " + Mathf.RoundToInt(GameManager.Instance.GameTimer));
        sb.AppendLine("Served: " + GameManager.Instance.Served);
        sb.AppendLine("Waiting: " + GameManager.Instance.WaitingCustomers);

        text.SetText(sb.ToString());
    }
}
