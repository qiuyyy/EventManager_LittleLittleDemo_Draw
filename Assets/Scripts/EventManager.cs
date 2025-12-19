using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public enum EventType
{
    None,
    BtnTap,
}
public class EventManager : MonoBehaviour
{
    private Dictionary<EventType, List<IEventListener>> listenerDic = new Dictionary<EventType, List<IEventListener>>();
    public static EventManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ClearEventListener(EventType eventType)
    {
        if (listenerDic.TryGetValue(eventType, out List<IEventListener> listeners))
        {
            listeners.Clear();
        }
    }

    public void RegisterEventListener<T>(EventType eventType, T listener, bool isRegister) where T: IEventListener {
        if (!listenerDic.ContainsKey(eventType))
        {
            listenerDic.Add(eventType, new List<IEventListener>());
        }
        var listeners = listenerDic[eventType];
        if (!isRegister)
        {
            listeners.Remove(listener);
        } else
        {
            listeners.Add(listener);
        }
    }

    public void TriggerEventListener(EventType eventType)
    {
        if (listenerDic.TryGetValue(eventType, out List<IEventListener> listeners))
        {
            foreach (var listener in listeners.ToList())
            {
                TriggerEvent(eventType, listener);
            }
        }
    }

    private void TriggerEvent(EventType eventType, IEventListener listener)
    {
        switch (eventType)
        {
            case EventType.BtnTap:
                if (listener is IEventListener_BtnTap btnTapListener)
                    btnTapListener.OnBtnTap();
                break;
            default:
                break;
        }
    }

}
public interface IEventListener {
}

public interface IEventListener_BtnTap: IEventListener {
    void OnBtnTap();
}
