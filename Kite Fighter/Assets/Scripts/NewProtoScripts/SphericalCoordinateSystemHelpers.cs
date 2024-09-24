using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SphericalCoordinateSystemHelpers
{

    public static Vector3 SphericalToCartesian(Vector3 sphericalCoord)
    {
        return SphericalToCartesian(sphericalCoord.x, sphericalCoord.y, sphericalCoord.z);
    }

    public static Vector3 SphericalToCartesian(float radius, float azimuth, float elevation)
    {
        float a = radius * Mathf.Cos(elevation);

        Vector3 result = new Vector3();
        result.x = a * Mathf.Cos(azimuth);
        result.y = radius * Mathf.Sin(elevation);
        result.z = a * Mathf.Sin(azimuth);

        return result;
    }

    /// <summary>
    /// Return Vector3 is x = radius, y = polar(azimuth), z = elevation.
    /// </summary>
    /// <param name="cartCoords"></param>
    /// <returns></returns>
    public static Vector3 CartesianToSpherical(Vector3 cartCoords)
    {
        float _radius, _azimuth, _elevation;

        if (cartCoords.x == 0)
            cartCoords.x = Mathf.Epsilon;

        _radius = Mathf.Sqrt((cartCoords.x * cartCoords.x)
        + (cartCoords.y * cartCoords.y)
        + (cartCoords.z * cartCoords.z));

        _azimuth = Mathf.Atan(cartCoords.z / cartCoords.x);

        if (cartCoords.x < 0)
            _azimuth += Mathf.PI;
        _elevation = Mathf.Asin(cartCoords.y / _radius);

        Vector3 result = new Vector3(_radius, _azimuth, _elevation);
        return result;
    }
}
