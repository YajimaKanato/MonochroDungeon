using UnityEngine;
using System.Collections;

public class CloneofPlayer : MonoBehaviour
{
    [Header("HitObjectLayer")]
    [Tooltip("プレイヤーと同じ色を設定")]
    [SerializeField]
    LayerMask _hitLayer;

    SpriteRenderer _spriteRenderer;
    static RaycastHit2D _hitForward;
    public static RaycastHit2D HitForward { get { return _hitForward; } }

    [Header("Direction(Degree)")]
    [Tooltip("右を基準に反時計回りに角度をとる")]
    [SerializeField]
    float direction;
    float _moveX, _moveY;
    static bool _isMoving = false;
    public static bool IsMoving { get { return _isMoving; } }
    bool _isBlack = true;
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
        if (Input.GetMouseButtonDown(0) && _hitForward && !_isMoving && !_isColorChanging && Player.HitForward && !Player.IsMoving)
        {
            ColorChange(transform.eulerAngles.z * Mathf.Deg2Rad);
        }

        //移動入力
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");

        //方向転換
        if (_moveX == 1 && Mathf.Abs(_moveY) != 1)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (_moveX == -1 && Mathf.Abs(_moveY) != 1)
        {
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        if (_moveY == 1 && Mathf.Abs(_moveX) != 1)
        {
            transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        if (_moveY == -1 && Mathf.Abs(_moveX) != 1)
        {
            transform.localEulerAngles = new Vector3(0, 0, 270);
        }

        //LineCasts
        Debug.DrawLine(transform.position,
            transform.position + new Vector3(Mathf.Cos((transform.eulerAngles.z + direction) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + direction) * Mathf.Deg2Rad)));//接地判定に関するもの
        _hitForward = Physics2D.Linecast(transform.position,
            transform.position + new Vector3(Mathf.Cos((transform.eulerAngles.z + direction) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + direction) * Mathf.Deg2Rad)),
            _hitLayer);

        //移動
        if (!_isMoving && Mathf.Abs(_moveX) == 1 && !_hitForward && !_isColorChanging)
        {
            Debug.Log("左右移動");
            _moveY = 0;
            _isMoving = true;
            Vector3 basePos = transform.position;
            StartCoroutine(MoveCoroutine(_moveX, _moveY, basePos));
        }
        if (!_isMoving && Mathf.Abs(_moveY) == 1 && !_hitForward && !_isColorChanging)
        {
            Debug.Log("上下移動");
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
                transform.position += new Vector3(Mathf.Cos((transform.eulerAngles.z + direction) * Mathf.Deg2Rad) / 60, Mathf.Sin((transform.eulerAngles.z + direction) * Mathf.Deg2Rad) / 60, 0);
                yield return null;
            }
            else
            {
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

        transform.position += new Vector3(Mathf.Cos(theta + direction * Mathf.Deg2Rad), Mathf.Sin(theta + direction * Mathf.Deg2Rad), 0);//上下反転にともなった座標調整
        theta += Mathf.PI;
        transform.eulerAngles = new Vector3(0, 0, theta * Mathf.Rad2Deg);

        StartCoroutine(StayCoroutine(0.5f));
    }
}
