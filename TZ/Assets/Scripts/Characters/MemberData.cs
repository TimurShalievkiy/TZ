namespace Characters
{
    public class MemberData
    {
        public Member Member;
        public WorldPosition WorldPosition;

        public MemberData()
        {
        }

        public MemberData(MemberData member)
        {
            Member = new Member(member.Member);
            WorldPosition = member.WorldPosition;
        }
    }
}