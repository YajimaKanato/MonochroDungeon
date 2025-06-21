using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("GroundLayer")]
    [SerializeField]
    LayerMask _ground;
    bool _isBlack = false;
    SpriteRenderer _spriteRenderer;

    Rigidbody2D _rb2d;
    RaycastHit2D _hitGround;
    float _speed;
    float _jumpPower = 15.0f;

    GameObject[] _gameObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameObjects = GameObject.FindGameObjectsWithTag("Block");
    }

    // Update is called once per frame
    void Update()
    {
        //LineCast
        Debug.DrawLine(transform.position, transform.position - Vector3.up * 1.5f * transform.localScale.y);//接地判定に関するもの
        _hitGround = Physics2D.Linecast(transform.position, transform.position - Vector3.up * 1.5f * transform.localScale.y, _ground);

        //色変更
        if (Input.GetMouseButtonDown(0) && _hitGround)
        {
            ColorChange();
        }

        //Move
        _speed = Input.GetAxisRaw("Horizontal");

        //Jump
        if (Input.GetButtonDown("Jump") && _hitGround)//ジャンプの回数を制限
        {
            _rb2d.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //Move
        _rb2d.linearVelocityX = _speed * 5;
    }

    void ColorChange()
    {
        if (!_isBlack)
        {
            Debug.Log("黒に変更");
            _ground = 1 << LayerMask.NameToLayer("Black");
            _spriteRenderer.color = Color.black;
            _isBlack = true;
        }
        else
        {
            Debug.Log("白に変更");
            _ground = 1 << LayerMask.NameToLayer("White");
            _spriteRenderer.color = Color.white;
            _isBlack = false;
        }

        foreach (var go in _gameObjects)
        {
            if (go.GetComponent<StageBWObject>())//コライダー変更を全オブジェクトに適応
            {
                go.GetComponent<StageBWObject>().ColliderChange();
            }
        }
        transform.position += Vector3.down * 2 * transform.localScale.y;//上下反転にともなった座標調整
        transform.localScale = new Vector3(1, transform.localScale.y * -1, 1);//上下反転
        _rb2d.gravityScale *= -1;//重力反転
        _jumpPower *= -1;//ジャンプ方向反転
    }
}
