using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Extensions
{
    public struct RayHit
    {
        public Vector2 point;
        public Collider2D collider;
    }

    public static GameObject GetRandom(this List<GameObject> source)
    {
        return source[Random.Range(0, source.Count)];
    }

    public static Quaternion LookAt2D(this Transform t, Vector3 target)
    {
        Vector3 diff = target - t.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public static Quaternion LookAt2D(this Transform t, Transform target)
    {
        Vector3 diff = target.position - t.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public static Quaternion LookAt2D(this Vector3 t, Vector3 target)
    {
        Vector3 diff = target - t;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public static Quaternion LookAt2D(this Vector2 t, Vector2 target)
    {
        Vector3 diff = target - t;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public static Vector2 ToVector(this Quaternion q)
    {
        return new Vector2(Mathf.Cos(q.eulerAngles.z), Mathf.Sin(q.eulerAngles.z));
    }

    public static RayHit Raycast2D(Vector2 origin, Vector2 direction, float distance, LayerMask mask)
    {
        var ray = new RayHit();

        var hit = Physics2D.Raycast(origin, direction, distance, mask);

        if(hit.collider == null)
        {
            ray.point = direction * distance;
            Debug.Log("Ray Hit was null, Making Point at: " + ray.point);
        } else
        {
            ray.point = hit.point;
            ray.collider = hit.collider;
        }

        return ray;
    }

    public static Vector3 Z(this Vector3 vin, float num)
    {
        vin.z = num;
        return vin;
    }

    public static BoltEntity[] GetPlayersInRadius(this Vector2 point, float radius)
    {
        var r = new List<BoltEntity>();
        var colliders = Physics2D.OverlapCircleAll(point, radius);

        foreach (var item in colliders)
        {
            if(item.gameObject.tag == "Player")
            {
                r.Add(item.GetComponentInParent<BoltEntity>());
            }
        }

        return r.ToArray();
    }

    public static BoltEntity[] GetPlayersInRadius(this Vector3 point, float radius)
    {
        Vector2 f = point;

        return f.GetPlayersInRadius(radius);
    }

    public static void Damage(this BoltEntity e, BoltEntity source, int amount)
    {
        if (e.StateIs<IPlayer>())
        {
            e.GetComponent<TankHealth>().Damage(source, amount);
        }
    }

    public static int Team(this BoltEntity e)
    {
        if (e.StateIs<IPlayer>())
        {
            return e.GetState<IPlayer>().Team;
        } else
        {
            return Player.localTeam;
        }
    }

    public static string FormatSeconds(this float f)
    {
            float mins = Mathf.Round(Mathf.Floor(f / 60));
            float _seconds = Mathf.Floor(f % 60);

            string s;

            if (_seconds < 10)
            {
                s = "0" + _seconds;
            }
            else
            {
                s = _seconds.ToString();
            }

            return mins + ":" + s;
    }

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static bool Within(this float a, float b, float range)
    {
        float max = b + range;
        float min = b - range;

        return a > min && a < max;
    }

    public static bool IsFriendly(this BoltEntity e)
    {
        return e.Team() == Player.localTeam;
    }

    public static void ExplosionForce(this Rigidbody2D body, float explosionRadius, float explosionForce)
    {
        var dir = (body.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * explosionForce * wearoff);
    }

    public static GameObject Get(this GameObject[] arr, string key)
    {
        foreach (var item in arr)
        {
            if(item.name == key)
            {
                return item;
            }
        }

        return null;
    }
}
