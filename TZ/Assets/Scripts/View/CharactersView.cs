using System.Collections.Generic;
using System.Linq;
using Characters;
using Core.MessengerStatic;
using DefaultNamespace;
using UnityEngine;

namespace View
{
    public class CharactersView : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private List<MemberView> _teamMembers;

        private void Start()
        {
            Messenger<List<Team>>.AddListener(Constnts.Events.GameField.CreateTeams, CreateTeams);
            Messenger<MemberData>.AddListener(Constnts.Events.Game.MemberUpdate, MemberUpdate);
        }

        private void MemberUpdate(MemberData obj)
        {
            var member = _teamMembers.FirstOrDefault(x => x.memberData.Member.ID.Equals(obj.Member.ID));

            if (member == null)
            {
                return;
            }

            if (member.memberData.Member.Stats[Constnts.Stats.HealthPoints].Value <= 0)
            {
                Destroy(member.gameObject, 1);
                member.gameObject.SetActive(false);
                _teamMembers.Remove(member);
                return;
            }
            member.Move(obj.WorldPosition);
        }

        private async void CreateTeams(List<Team> teams)
        {
            _teamMembers = new List<MemberView>();
            foreach (var team in teams)
            {
                foreach (var memberData in team.MembersData)
                {
                    var charPrefab = await AssetProvider.LoadAssetAsync<GameObject>(memberData.Member.AddressableKey);
                    var charView = charPrefab.GetComponent<MemberView>();

                    if (charView == null)
                    {
                        return;
                    }

                    var newBox = Instantiate(charView, _root);
                    newBox.memberData = memberData;

                    newBox.Move(memberData.WorldPosition);
                    _teamMembers.Add(newBox);
                }
            }
        }
    }
}