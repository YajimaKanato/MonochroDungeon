using UnityEngine;
using System.Collections;

public class CloneofPlayer : MonoBehaviour
{
    [Header("HitObjectLayer")]
    [Tooltip("�v���C���[�Ɠ����F��ݒ�")]
    [SerializeField]
    LayerMask _hitLayer;
    RaycastHit2D _hitWall;
    static RaycastHit2D _hitBW;
    public static RaycastHit2D HitBW { get { return _hitBW; } }

    SpriteRenderer _spriteRenderer;

    [Header("Direction(Degree)")]
    [Tooltip("�E����ɔ����v���Ɋp�x���Ƃ�")]
    [SerializeField]
    float direction;

    float _moveX, _moveY;
    static bool _isMoving = false;
    public static bool IsMoving { get { return _isMoving; } }
    bool _isBlack = true;
    bool _isColorChanging = false;

    //Debug.Log("C:");�f�o�b�O�p

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //�����]��
        if (!_isMoving)
        {
            //�ړ�����
            _moveX = Input.GetAxisRaw("Horizontal");
            _moveY = Input.GetAxisRaw("Vertical");

            if (_moveX == 1)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if (_moveX == -1)
            {
                transform.localEulerAngles = new Vector3(0, 0, 180);
            }
            else if (_moveY == 1)
            {
                transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            else if (_moveY == -1)
            {
                transform.localEulerAngles = new Vector3(0, 0, 270);
            }
        }

        //LineCasts
        Debug.DrawLine(transform.position,
            transform.position + new Vector3(Mathf.Cos((transform.eulerAngles.z + direction) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + direction) * Mathf.Deg2Rad)));//�ڒn����Ɋւ������
        _hitWall = Physics2D.Linecast(transform.position,
            transform.position + new Vector3(Mathf.Cos((transform.eulerAngles.z + direction) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + direction) * Mathf.Deg2Rad)),
            _hitLayer);
        Debug.DrawLine(transform.position,
            transform.position + new Vector3(Mathf.Cos((transform.eulerAngles.z + direction) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + direction) * Mathf.Deg2Rad)));//�ڒn����Ɋւ������
        _hitBW = Physics2D.Linecast(transform.position,
            transform.position + new Vector3(Mathf.Cos((transform.eulerAngles.z + direction) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + direction) * Mathf.Deg2Rad)),
            _hitLayer & ~(1 << LayerMask.NameToLayer("Wall")));

        //�ړ�
        if (!_isMoving && Mathf.Abs(_moveX) == 1 && !_hitWall && !_isColorChanging)
        {
            Debug.Log("C:���E�ړ�");
            _moveY = 0;
            _isMoving = true;
            Vector3 basePos = transform.position;
            StartCoroutine(MoveCoroutine(basePos));
        }
        else if (!_isMoving && Mathf.Abs(_moveY) == 1 && !_hitWall && !_isColorChanging)
        {
            Debug.Log("C:�㉺�ړ�");
            _moveX = 0;
            _isMoving = true;
            Vector3 basePos = transform.position;
            StartCoroutine(MoveCoroutine(basePos));
        }
        //�F�ύX
        else if (Input.GetMouseButtonDown(0) && _hitBW && !_isMoving && !_isColorChanging && Player.HitBW && !Player.IsMoving)
        {
            ColorChange(transform.eulerAngles.z * Mathf.Deg2Rad);
        }
    }

    /// <summary>
    /// �ړ������炩�ɂ���R���[�`��
    /// </summary>
    /// <param name="basePos"></param>
    /// <returns></returns>
    IEnumerator MoveCoroutine(Vector3 basePos)
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
                transform.position = basePos + new Vector3(Mathf.Cos((transform.eulerAngles.z + direction) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + direction) * Mathf.Deg2Rad), 0);
                //�ړ��ɂ��덷�̒���
                _isMoving = false;
                yield break;
            }
        }
    }

    /// <summary>
    /// �C�ӂ̎��ԑ҂R���[�`��
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator ColorChangeCoroutine(float time)
    {
        _isColorChanging = true;
        yield return new WaitForSeconds(time);
        _isColorChanging = false;
        yield break;
    }

    /// <summary>
    /// ����̑���Ŕ�������𔽓]�����郁�\�b�h
    /// </summary>
    void ColorChange(float theta)
    {
        if (!_isBlack)
        {
            Debug.Log("C:���ɕύX");
            _hitLayer &= ~(1 << LayerMask.NameToLayer("White"));
            _hitLayer |= (1 << LayerMask.NameToLayer("Black"));
            _spriteRenderer.color = Color.black;
            _isBlack = true;
        }
        else
        {
            Debug.Log("C:���ɕύX");
            _hitLayer &= ~(1 << LayerMask.NameToLayer("Black"));
            _hitLayer |= (1 << LayerMask.NameToLayer("White"));
            _spriteRenderer.color = Color.white;
            _isBlack = false;
        }

        transform.position += new Vector3(Mathf.Cos(theta + direction * Mathf.Deg2Rad), Mathf.Sin(theta + direction * Mathf.Deg2Rad), 0);//�㉺���]�ɂƂ��Ȃ������W����
        theta += Mathf.PI;
        transform.eulerAngles = new Vector3(0, 0, theta * Mathf.Rad2Deg);

        StartCoroutine(ColorChangeCoroutine(0.5f));
    }
}
