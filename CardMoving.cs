using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CardMoving : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera MainCam;
    Vector3 offset;
    public Transform DefautlParent, DefaultTempCardParent;
    GameObject TempCardGo;
    public bool isDraggable;
    GameManager gamemanager;

    void Awake()
    {
        MainCam = Camera.allCameras[0];
        TempCardGo = GameObject.Find("TempCardGo");
        gamemanager = FindObjectOfType<GameManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCam.ScreenToWorldPoint(eventData.position);
        DefautlParent = DefaultTempCardParent = transform.parent;
        isDraggable = (DefautlParent.GetComponent<DropPlace>().Type == FieldType.SELF_HAND || DefautlParent.GetComponent<DropPlace>().Type ==  FieldType.SELF_FIELD) && gamemanager.IsPlayerTurn&&eventData.pointerDrag.gameObject.name!="0";
        if (!isDraggable)
            return;
        TempCardGo.transform.SetParent(DefautlParent);
        TempCardGo.transform.SetSiblingIndex(transform.GetSiblingIndex());
        transform.SetParent(DefautlParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;
        Vector3 newPos = MainCam.ScreenToWorldPoint(eventData.position);
       
        transform.position = newPos + offset;
        if (TempCardGo.transform.parent != DefaultTempCardParent)
            TempCardGo.transform.SetParent(DefaultTempCardParent);
        CheckPos();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;
        transform.SetParent(DefautlParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetSiblingIndex(TempCardGo.transform.GetSiblingIndex());
        TempCardGo.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGo.transform.localPosition = new Vector3(2340, 0);

    }

    void CheckPos()
    {
        int newIndex = DefaultTempCardParent.childCount;
        for (int i = 0; i < DefaultTempCardParent.childCount;i++)

        {
            if (transform.position.x < DefaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;
                if (TempCardGo.transform.GetSiblingIndex() < newIndex)
                    newIndex--;
                break;

            }
        }
        TempCardGo.transform.SetSiblingIndex(newIndex);
    }
}

