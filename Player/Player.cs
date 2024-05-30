using DG.Tweening;
using SCPE;
using UnityEngine;
using UnityEngine.AnimatorPro;
using UnityEngine.Rendering;
using static StageManager;

[RequireComponent(typeof(AnimatorPro))]
public class Player : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private Volume m_volume;

    // PostEffect
    private Blur p_blur;
    private bool b_blueamount = false;

    // Component
    private CharacterController _controller;
    private AnimatorPro _animatorPro;
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private Light[] _Lights;
    [SerializeField] private GameObject _parentModel;

    // Status
    private Vector3 _movement = Vector3.zero;

    private float _moveCurrentSpeed = 0.0f;
    private float _moveSpeed = 6.0f;
    private float _moveRunSpeed = 10.0f;

    private float _jumpscareEvent = 0.0f;
    private float _jumpscareEventTime = 10000.0f;

    private float _rungauge = 0.0f;
    private float _rungaugeMax = 10.0f;
    private float _rungaugeCoolTime = 2.0f;

    // Variable
    private bool _isRun = false;
    private bool _nextStage = false;
    private bool _hideEvent = false;
    private float _hideTime = 0.0f;

    public bool b_CanMove = true;
    public Transform _cameraTransform;

    private void Awake()
    {
        CharacterManager.Instance.player = this;

        _controller = gameObject.GetComponent<CharacterController>();
        _animatorPro = GetComponent<AnimatorPro>();

        _animatorPro.Init(anim);

        _cameraTransform = _playerCamera.transform;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        m_volume.profile.TryGet(typeof(Blur), out p_blur);
        p_blur.amount.overrideState = true;
        p_blur.amount.value = 0.0f;

        UIManager.Instance.HideOut(0.005f);
    }

    private void Update()
    {
        if (b_CanMove)
        {
            _animatorPro.SetParam("_IsMove", Move());
            _animatorPro.SetParam("_IsRun", _isRun);
        }

        float gage = _rungauge / _rungaugeMax;
        UIManager.Instance.Rungauge(gage);

        _parentModel.transform.localPosition = Vector3.zero;

        raycast();
        EffectVision();
        NextStage();
    }

    private void LateUpdate()
    {
        if (b_CanMove)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * 10.0f);
            anim.gameObject.transform.rotation = Quaternion.LookRotation(playerRotate);
        }
    }

    private bool Move()
    {
        bool ismove = false;

        if (Input.GetKey(KeyCode.LeftShift) && _rungauge > 0.0f)
        {
            _isRun = true;
            _moveCurrentSpeed = _moveRunSpeed;
        }
        else
        {
            _isRun = false;
            _moveCurrentSpeed = _moveSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            _movement.z = 1.0f;
            ismove = true;

            if (_isRun)
            {
                _jumpscareEvent = Random.Range(0.0f, 10002.0f);

                _rungauge -= Time.deltaTime;
                _rungaugeCoolTime = 2.0f;
            }
            else _jumpscareEvent = Random.Range(0.0f, 10000.5f);

            _jumpscareEventTime -= 0.001f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _movement.z = -1.0f;
            ismove = true;

            if (_isRun)
            {
                _jumpscareEvent = Random.Range(0.0f, 10002.0f);

                _rungauge -= Time.deltaTime;
                _rungaugeCoolTime = 2.0f;
            }
            else _jumpscareEvent = Random.Range(0.0f, 10000.5f);

            _jumpscareEventTime -= 0.001f;
        }
        else _movement.z = 0.0f;

        _controller.Move(transform.TransformDirection(_movement.normalized) * _moveCurrentSpeed * Time.deltaTime);

        if (_jumpscareEvent >= _jumpscareEventTime)
        {
            _jumpscareEventTime = 10000.0f;

            StageManager.Instance.JumpscareImageEvent();
        }

        if (_rungaugeCoolTime > 0.0f)
        {
            _rungaugeCoolTime -= Time.deltaTime;
        }
        else
        {
            if (_rungauge < _rungaugeMax)
                _rungauge += 1.5f * Time.deltaTime;

            if (_rungauge > _rungaugeMax) _rungauge = _rungaugeMax;
        }

        return ismove;
    }

    private void raycast()
    {
        Transform selection = _playerCamera.RayCastHit(12.5f).transform;

        if (selection != null)
        {
            if (selection.CompareTag("Door"))
            {
                CharacterManager.Instance.door.MaterialSwap("highlight");

                UIManager.Instance.ButtenEnable(true);

                if (Input.GetKeyDown(KeyCode.E) && !_nextStage)
                {
                    UIManager.Instance.HideIn();
                    _hideEvent = true;
                    _nextStage = true;

                    _playerCamera.b_moveCamera = false;

                    uint _lv = StageManager.Instance._stageLv;

                    if (_lv == 1) SoundManager.Instance.PlayBGM("BGM1");

                    if (_lv < 9 || (_lv >= 10 && _lv < 14))
                    {
                        StageManager.Instance.stage_map = (STAGE_MAP)Random.Range(2, 6);
                    }
                    else
                    {
                        StageManager.Instance.stage_map = STAGE_MAP.BOSS;
                    }
                }
            }
            else
            {
                Defualt();
            }
        }
        else
        {
            Defualt();
        }

        void Defualt()
        {
            CharacterManager.Instance.door.MaterialSwap("default");

            UIManager.Instance.ButtenEnable(false);
        }
    }

    private void EffectVision()
    {
        float speed = 10.0f;

        if (_rungauge <= 6.0f)
        {

            if (b_blueamount)
            {
                p_blur.amount.value -= speed * Time.deltaTime;

                if (p_blur.amount.value <= 0) b_blueamount = false;
            }
            else
            {
                p_blur.amount.value += speed * Time.deltaTime;

                if (_rungauge >= 5.0f && p_blur.amount.value >= 1.0f) b_blueamount = true;
                else if (_rungauge >= 4.0f && p_blur.amount.value >= 2.0f) b_blueamount = true;
                else if (_rungauge >= 3.0f && p_blur.amount.value >= 3.0f) b_blueamount = true;
                else if (_rungauge >= 1.5f && p_blur.amount.value >= 4.0f) b_blueamount = true;
                else if (p_blur.amount.value >= 5.0f) b_blueamount = true;
            }
        }
        else
        {
            if (p_blur.amount.value > 0.0f) p_blur.amount.value -= speed * Time.deltaTime;
        }
    }

    private void NextStage()
    {
        if (_nextStage)
        {
            if (UIManager.Instance.HideAlpha >= 1.0f && _hideEvent)
            {
                if (_hideTime < 2.0f)
                {
                    _hideTime += Time.deltaTime;

                    StageManager.Instance.StageLoadingPositionEvent();
                }
                else
                {
                    _hideEvent = false;

                    if (StageManager.Instance.stage_map == STAGE_MAP.A) StageManager.Instance.LightChangeEvent("A");
                    else if (StageManager.Instance.stage_map == STAGE_MAP.B) StageManager.Instance.LightChangeEvent("B");
                    else if (StageManager.Instance.stage_map == STAGE_MAP.C) StageManager.Instance.LightChangeEvent("C");
                    else if (StageManager.Instance.stage_map == STAGE_MAP.D) StageManager.Instance.LightChangeEvent("D");

                    StageManager.Instance.LevelUpEvent();

                    if (StageManager.Instance._stageLv == 10)
                        DOVirtual.DelayedCall(8.0f, StageManager.Instance.SpawnDog, false);

                    UIManager.Instance.HideOut();

                    foreach (var stickTrap in CharacterManager.Instance.stickTrap)
                    {
                        if (stickTrap != null)
                            stickTrap.HideObject();
                    }

                    _playerCamera.b_moveCamera = true;
                    _hideTime = 0.0f;
                }
            }
            else if (UIManager.Instance.HideAlpha <= 0.0f && !_hideEvent && _nextStage)
            {
                _nextStage = false;
            }
        }
    }

    public void SetLightIntensity(float intensity)
    {
        _Lights[0].intensity = intensity;
        _Lights[1].intensity = intensity;
    }
}
