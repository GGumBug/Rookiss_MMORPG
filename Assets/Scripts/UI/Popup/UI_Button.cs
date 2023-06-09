using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup
{
    [SerializeField]
    private Text    txtScore;
    private int     score = 0;

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum Buttons
    {
        PointButton
    }

    enum GameObjects
    {
        TestObject
    }

    enum Images
    {
        ItemIcon
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        BindEvent(go, (action) => { go.transform.position = action.position; }, Define.UIEvent.Drag);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        score++;

        GetText((int)Texts.ScoreText).text = $"점수 :{score}점";
    }
}
