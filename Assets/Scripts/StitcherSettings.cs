using UnityEngine;

namespace BrickBreaker
{
    [CreateAssetMenu(menuName = "BrickBreaker/StitcherSettings")]
    public class StitcherSettings : ScriptableObject
    {
        public GameObject peaceSetProto;
        public int splits;
    }
}