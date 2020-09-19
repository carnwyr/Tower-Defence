using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool _isMoving = false;
    private GameObject _target;
    private readonly float _speed = 30f;
    private int _damage = 1;

    private void OnDisable()
    {
        _isMoving = false;
    }

    public void Shoot(GameObject target, int damage)
    {
        _target = target;
        _damage = damage;
        _isMoving = true;
    }

    void Update()
    {
        if (_isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
            if (transform.position == new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z))
            {
                _target.GetComponent<Enemy>().GetAttacked(_damage);
                gameObject.SetActive(false);
            }
        }
    }
}
