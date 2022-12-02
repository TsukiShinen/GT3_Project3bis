using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankActions: MonoBehaviour
{
    #region Variables

    [SerializeField] private Tank tank;
    [SerializeField] private GameObject _completeShell;
    public Transform shootSocket;

    #endregion

    public void Init()
    {

    }

    public void Shoot()
    {
        if (tank.isDead) return;
        if (!tank.canShoot) return;
        if (tank.isJumping) return;
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        tank.canShoot = false;

        tank.audioSO.PlaySFX("shoot");

        var bullet = Instantiate(_completeShell, shootSocket);

        bullet.transform.SetParent(null);

        var rbBullet = bullet.GetComponent<Rigidbody>();

        rbBullet.velocity = tank.tankParametersSO.ProjectileSpeed * shootSocket.forward;

        yield return new WaitForSeconds(tank.tankParametersSO.ShootCooldown);

        tank.canShoot = true;
    }

    public void SpecialJump()
    {
        if (tank.isDead) return;
        if (tank.isJumping) return;
        if (!tank.canJump) return;
        StartCoroutine(SpecialJumpCouroutine());
    }

    public IEnumerator SpecialJumpCouroutine()
    {
        tank.isJumping = true;
        tank.canJump = false;
        float startTime = Time.time;
        Vector3 holdPosition = transform.position;
        float angle = 360f / (tank.tankParametersSO.SpecialJump[tank.tankParametersSO.SpecialJump.length - 1].time * 2);
        tank.audioSO.PlaySFX("jump");

        while (Time.time - startTime < tank.tankParametersSO.SpecialJump[tank.tankParametersSO.SpecialJump.length - 1].time * 2)
        {
            transform.position = new Vector3(holdPosition.x, holdPosition.y + tank.tankParametersSO.SpecialJump.Evaluate(Time.time - startTime), holdPosition.z);
            tank.tankMesh.transform.Rotate(-angle * Time.deltaTime, 0, 0);
            yield return null;
        }

        tank.tankMesh.transform.localRotation = Quaternion.Euler(0, tank.tankMesh.transform.localRotation.y, tank.tankMesh.transform.localRotation.z);
        tank.isJumping = false;
        yield return new WaitForSeconds(tank.tankParametersSO.SpecialJumpCooldown);
        tank.canJump = true;
    }
}
