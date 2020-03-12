using UnityEngine;

namespace BrickBreaker
{
    [CreateAssetMenu(menuName = "BrickBreaker/StitcherSettings")]
    public class StitcherSettings : ScriptableObject
    {
        public PeaceSet peaceSetProto;
        public int splits;
    }
}