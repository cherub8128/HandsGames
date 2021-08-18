using System;
using UnityEngine;

public class PositionEventArgs : EventArgs
{
    public readonly Vector2 BeganPosition;
    public readonly Vector2 EndedPosition;
    public PositionEventArgs(){}
    public PositionEventArgs(Vector2 position1)
    {
        BeganPosition = position1;
    }
    public PositionEventArgs(Vector2 position1, Vector2 position2)
    {
        BeganPosition = position1;
        EndedPosition = position2;
    }
}

public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs: PositionEventArgs;

public class TouchEvents : MonoBehaviour
{
    static public event EventHandler<PositionEventArgs> TouchSwipeUp, TouchSwipeDown,TouchSwipeLeft, TouchSwipeRight, TouchUp, TouchDown;
    static public event EventHandler<PositionEventArgs> TouchSwipeUp2, TouchSwipeDown2;
    private Vector2 BeganPositon, EndedPositon, deltaPos;
    private Touch[] touches = new Touch[2];
    static public bool isStateIn = false;
    // int count = 0;
    protected virtual void onTouchSwipeDown(PositionEventArgs e)
    {
        TouchSwipeDown?.Invoke(this, e);
    }
    protected virtual void onTouchSwipeUp(PositionEventArgs e)
    {
        TouchSwipeUp?.Invoke(this, e);
    }
    protected virtual void onTouchSwipeLeft(PositionEventArgs e)
    {
        TouchSwipeLeft?.Invoke(this, e);
    }
    protected virtual void onTouchSwipeRight(PositionEventArgs e)
    {
        TouchSwipeRight?.Invoke(this, e);
    }
    protected virtual void onTouchUp(PositionEventArgs e)
    {
        TouchUp?.Invoke(this, e);
    }
    protected virtual void onTouchDown(PositionEventArgs e)
    {
        TouchDown?.Invoke(this, e);
    }
    protected virtual void onTouchSwipeDown2(PositionEventArgs e)
    {
        TouchSwipeDown2?.Invoke(this, e);
    }
    protected virtual void onTouchSwipeUp2(PositionEventArgs e)
    {
        TouchSwipeUp2?.Invoke(this, e);
    }
    private void Update()
    {
        if (Input.touchCount == 2)
        {
            touches[1] = Input.GetTouch(1);
            deltaPos = touches[1].deltaPosition;
            if (!isStateIn && deltaPos.y > 10f)
            {
                isStateIn = true;
                onTouchSwipeUp2(new PositionEventArgs(BeganPositon));
                // Debug.Log("멀티위쪽");
            }
            else if (!isStateIn && deltaPos.y < -10f)
            {
                isStateIn = true;
                onTouchSwipeDown2(new PositionEventArgs(BeganPositon));
                // Debug.Log("멀티아래쪽");
            }
            if (touches[1].phase == TouchPhase.Ended && touches[0].phase == TouchPhase.Ended)
            {
                isStateIn = false;
                // Debug.Log("멀티");
            }
        }
        else if (Input.touchCount == 1)
        {
            touches[0] = Input.GetTouch(0);
            deltaPos = touches[0].deltaPosition;
            if (touches[0].phase == TouchPhase.Began)
            {
                BeganPositon = touches[0].position;
                onTouchDown(new PositionEventArgs(BeganPositon));
            }
            if (deltaPos.magnitude > 10f)
            {
                deltaPos = deltaPos.normalized;
                if (!isStateIn && deltaPos.x > 0.71f)
                {
                    isStateIn = true;
                    onTouchSwipeRight(new PositionEventArgs(BeganPositon));
                    // Debug.Log("오른쪽");
                }
                else if (!isStateIn && deltaPos.x < -0.71f)
                {
                    isStateIn = true;
                    onTouchSwipeLeft(new PositionEventArgs(BeganPositon));
                    // Debug.Log("왼쪽");
                }
                else if (!isStateIn && deltaPos.y > 0.71f)
                {
                    isStateIn = true;
                    onTouchSwipeUp(new PositionEventArgs(BeganPositon));
                    // Debug.Log("위쪽");
                }
                else if (!isStateIn && deltaPos.y < -0.71f)
                {
                    isStateIn = true;
                    onTouchSwipeDown(new PositionEventArgs(BeganPositon));
                    // Debug.Log("아래쪽");
                }
            }
            if (touches[0].phase == TouchPhase.Ended)
            {
                EndedPositon = touches[0].position;
                if(!isStateIn) onTouchUp(new PositionEventArgs(BeganPositon, EndedPositon));
                isStateIn = false;
            }
        }
    }
    static public void setIsStateIn(bool b)
    {
        isStateIn = b;
    }
}
