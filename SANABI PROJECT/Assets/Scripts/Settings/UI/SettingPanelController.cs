using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingPanelController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<bool> OnHover;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHover?.Invoke(false);
    }
}

