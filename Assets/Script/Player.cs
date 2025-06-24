using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("HitObjectLayer")]
    [SerializeField]
    LayerMask _hitLayer;
    static RaycastHit2D _hitForward;
    public static RaycastHit2D HitForward { get { return _hitForward; } }

    SpriteRenderer _spriteRenderer;

    float _moveX, _moveY;
    static bool _isMoving = false;
    public static bool IsMoving { get { return _isMoving; } }
    bool _isBlack = false;
    bool _isColorChanging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //色変更
        if (Input.GetMouseButtonDown(0) && _hitForward && !_isMoving && !_isColorChanging && CloneofPlayer.HitForward && !CloneofPlayer.IsMoving)
        {
            ColorChange(transform.eulerAngles.z * Mathf.Deg2Rad);
        }

        

        //方向転換
        if (!_isMoving)
        {
            //移動入力
            _moveX = Input.GetAxisRaw("Horizontal");
            _moveY = Input.GetAxisRaw("Vertical");

            if (_moveX == 1)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (_moveX == -1)
            {
                transform.localEulerAngles = new Vector3(0, 0, 180);
            }
            if (_moveY == 1)
            {
                transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            if (_moveY == -1)
            {
                transform.localEulerAngles = new Vector3(0, 0, 270);
            }
        }

        //LineCasts
        Debug.DrawLine(transform.position,
            transform.position + new Vector3(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)));//接地判定に関するもの
        _hitForward = Physics2D.Linecast(transform.position,
            transform.position + new Vector3(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)),
            _hitLayer);

        //移動
        if (!_isMoving && Mathf.Abs(_moveX) == 1 && !_hitForward && !_isColorChanging)
        {
            _moveY = 0;
            _isMoving = true;
            Vector3 basePos = transform.position;
            StartCoroutine(MoveCoroutine(_moveX, _moveY, basePos));
        }
        if (!_isMoving && Mathf.Abs(_moveY) == 1 && !_hitForward && !_isColorChanging)
        {
            _moveX = 0;
            _isMoving = true;
            Vector3 basePos = transform.position;
            StartCoroutine(MoveCoroutine(_moveX, _moveY, basePos));
        }
    }

    /// <summary>
    /// 移動を滑らかにするコルーチン
    /// </summary>
    /// <param name="moveX"></param>
    /// <param name="moveY"></param>
    /// <param name="basePos"></param>
    /// <returns></returns>
    IEnumerator MoveCoroutine(float moveX, float moveY, Vector3 basePos)
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, basePos) < 1.0f)
            {
                transform.position += new Vector3(moveX / 60, moveY / 60, 0);
                yield return null;
            }
            else
            {
                transform.position = basePos + new Vector3(_moveX, _moveY, 0);//移動による誤差の調整
                _isMoving = false;
                yield break;
            }
        }
    }

    /// <summary>
    /// 任意の時間待つコルーチン
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator StayCoroutine(float time)
    {
        _isColorChanging = true;
        yield return new WaitForSeconds(time);
        _isColorChanging = false;
        yield break;
    }

    /// <summary>
    /// 特定の操作で白黒操作を反転させるメソッド
    /// </summary>
    void ColorChange(float theta)
    {
        if (!_isBlack)
        {
            Debug.Log("黒に変更");
            _hitLayer = 1 << LayerMask.NameToLayer("Black");
            _spriteRenderer.color = Color.black;
            _isBlack = true;
        }
        else
        {
            Debug.Log("白に変更");
            _hitLayer = 1 << LayerMask.NameToLayer("White");
            _spriteRenderer.color = Color.white;
            _isBlack = false;
        }

        transform.position += new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);//上下反転にともなった座標調整
        theta += Mathf.PI;
        transform.eulerAngles = new Vector3(0, 0, theta * Mathf.Rad2Deg);

        StartCoroutine(StayCoroutine(0.5f));
    }
}
