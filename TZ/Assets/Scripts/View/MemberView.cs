using Characters;
using UnityEngine;

namespace View
{
    public class MemberView : MonoBehaviour
    {
        public MemberData memberData;

        public void Move(WorldPosition position)
        {
            memberData.WorldPosition = position;
            transform.localPosition = new Vector3(position.X * 2,
                0,
                position.Y * 2);
        }
        
    }
}
