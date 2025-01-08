using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : VCNVMonoBehaviour
{
    [SerializeField] protected Button btnLeft;
    [SerializeField] protected Button btnRight;
    [SerializeField] protected Button btnJump;
    [SerializeField] protected PlayerCtrl playerCtrl;
    private static TouchController instance;
    public static TouchController Instance => instance;
    public bool isMovingLeft = false;
    public bool isMovingRight = false;
    public bool isJumping = false;
    public bool hasJumped = false;
    public int jumpCount = 1;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.Log("Just only 1 TouchController allow exists");
        instance = this;
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadBtnLeft();
        this.LoadBtnRight();
        this.LoadBtnJump();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GameObject.FindObjectOfType<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl");
    }

    protected virtual void LoadBtnLeft()
    {
        if (this.btnLeft != null) return;
        this.btnLeft = transform.Find("ButtonLeft").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnLeft");
    }

    protected virtual void LoadBtnRight()
    {
        if (this.btnRight != null) return;
        this.btnRight = transform.Find("ButtonRight").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnRight");
    }

    protected virtual void LoadBtnJump()
    {
        if (this.btnJump != null) return;
        this.btnJump = transform.Find("ButtonJump").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnJump");
    }

    protected override void Start()
    {
        base.Start();
        this.ListenEventClick();
    }

    protected override void Update()
    {
        base.Update();
        this.HoldToMove();
    }

    protected virtual void ListenEventClick()
    {
        EventTrigger triggerLeft = btnLeft.GetComponent<EventTrigger>();
        EventTrigger triggerRight = btnRight.GetComponent<EventTrigger>();
        EventTrigger triggerJump = btnJump.GetComponent<EventTrigger>();
        if (triggerLeft != null && triggerRight != null)
        {
            // Sự kiện khi nhấn nút trái
            EventTrigger.Entry entryLeftDown = new EventTrigger.Entry();
            entryLeftDown.eventID = EventTriggerType.PointerDown;
            entryLeftDown.callback.AddListener((eventData) => { StartMovingLeft(); });
            triggerLeft.triggers.Add(entryLeftDown);

            EventTrigger.Entry entryLeftUp = new EventTrigger.Entry();
            entryLeftUp.eventID = EventTriggerType.PointerUp;
            entryLeftUp.callback.AddListener((eventData) => { StopMovingLeft(); });
            triggerLeft.triggers.Add(entryLeftUp);

            // Sự kiện khi nhấn nút phải
            EventTrigger.Entry entryRightDown = new EventTrigger.Entry();
            entryRightDown.eventID = EventTriggerType.PointerDown;
            entryRightDown.callback.AddListener((eventData) => { StartMovingRight(); });
            triggerRight.triggers.Add(entryRightDown);

            EventTrigger.Entry entryRightUp = new EventTrigger.Entry();
            entryRightUp.eventID = EventTriggerType.PointerUp;
            entryRightUp.callback.AddListener((eventData) => { StopMovingRight(); });
            triggerRight.triggers.Add(entryRightUp);

            //Sự kiện khi nhấn nút nhảy
            /*
             EventTrigger.Entry entryJumpDown = new EventTrigger.Entry();
            entryJumpDown.eventID = EventTriggerType.PointerDown;
            entryJumpDown.callback.AddListener((eventData) => { Jump(); });
            triggerJump.triggers.Add(entryJumpDown);

            EventTrigger.Entry entryJumpUp = new EventTrigger.Entry();
            entryJumpUp.eventID = EventTriggerType.PointerUp;
            entryJumpUp.callback.AddListener((eventData) => { EndJump(); });
            triggerJump.triggers.Add(entryJumpUp);*/
        }

        btnJump.onClick.AddListener(Jump);
    }
    public void Jump()
    {
        if (!hasJumped) // Nếu chưa nhảy lần nào
        {
            Debug.Log("Người chơi đã ấn nút nhảy");
            this.isJumping = true;
            hasJumped = true; // Đặt biến hasJumped thành true để chỉ ra đã nhảy một lần
        }
        else // Nếu đã nhảy lần đầu tiên và nhấn nút nhảy lần nữa
        {
            Debug.Log("Người chơi đã ấn nút nhảy lần thứ 2");
            // Thực hiện hành động khi nhấn nút nhảy lần thứ 2 ở đây
            this.DoubleJump();
        }
    }

    public virtual void DoubleJump()
    {
        Animator playerAnimator = this.playerCtrl.Animator;
        int maxJumpCount = 2;
        if (this.jumpCount < maxJumpCount)
        {
            playerAnimator.SetBool("isDoubleJump", true);
            this.playerCtrl.Jump();
            this.jumpCount++;
        }
    }

    public void EndJump()
    {
        // Khi người chơi ngừng nhấn nút nhảy, reset lại biến hasJumped để cho phép nhảy lần tiếp theo
        hasJumped = false;
    }

    public void StartMovingLeft()
    {
        this.isMovingLeft = true;
    }

    public void StopMovingLeft()
    {
        this.isMovingLeft=false;
    }
    public void StartMovingRight()
    {
        this.isMovingRight = true;
    }

    public void StopMovingRight()
    {
        this.isMovingRight = false;
    }

    protected virtual void HoldToMove()
    {
        if(this.isMovingLeft)
        {
            Debug.Log("Người chơi đang di chuyển qua trái");
        }else if (this.isMovingRight)
        {
            Debug.Log("Người chơi đang di chuyển qua phải");
        }
    }

   
}
