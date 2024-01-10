using Abilities;
using Core.MessengerStatic;
using DefaultNamespace;
    
public class AbilityController
{
    private MoveAbility _moveAbility;
    private AttackAbility _attackAbility;

    private Ability _currentSelected;
    public AbilityController()
    {
        Messenger<Ability>.AddListener(Constnts.Events.Game.ActionClick, OnAbilityClick);

        _moveAbility = new MoveAbility();
        _attackAbility = new AttackAbility();
    }

    private void OnAbilityClick(Ability ability)
    {
        DeselectAll();
        _currentSelected = ability;

        switch (ability.ID)
        {
            case Constnts.Ability.Move:
                _moveAbility.SetAbility(ability);
                _moveAbility.SetSelection(true);
                break;
            case Constnts.Ability.Attack:
                _attackAbility.SetAbility(ability);
                _attackAbility.SetSelection(true);
                break;
        }
    }

    private void DeselectAll()
    {
        Messenger.Broadcast(Constnts.Events.GameField.ResetState);
        _moveAbility.SetSelection(false);
        _attackAbility.SetSelection(false);
    }
}