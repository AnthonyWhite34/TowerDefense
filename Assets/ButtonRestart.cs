using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonRestart : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
    }
    public void IWasClicked()
    {
        Debug.Log("Restart Button was clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
