namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class TreekeeperJaradine : Citizen
{
    public override void Created()
    {
        MySelf.AddMoveSuperPointDesire(MySelf.Sm.Name, 0, 2000);
        MySelf.ChangeMoveType(0);
        base.Created();
    }
}