
namespace Influence.Graphics;

public static class GlobalLight
{
    static Vector3 _position = rotation.ToEuler().normalized * 8;
    internal static Vector3 position
    {
        get { return _position; }
        private set { _position = value; }
    }

    static Quaternion _rotation = Quaternion.Euler(Vector3.Right);
    public static Quaternion rotation
    {
        get { return _rotation; }
        set
        {
            _rotation = value;
            position = value.ToEuler().normalized * 8;
        }
    }

    public static Color color = Color.Blue;
    public static float intensity = 1f;

    public static float specularStrength = 0.5f;
    public static float ambientStrength = 0.8f;
}
