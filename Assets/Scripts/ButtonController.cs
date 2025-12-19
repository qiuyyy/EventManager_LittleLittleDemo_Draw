using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void OnBtn()
    {
        EventManager.Instance.TriggerEventListener(EventType.BtnTap);
    }

}
