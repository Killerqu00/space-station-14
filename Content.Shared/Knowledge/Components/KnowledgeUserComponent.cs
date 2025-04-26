using Content.Shared.Knowledge.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Knowledge.Components;

/// <summary>
/// Stores player's knowledge.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class KnowledgeUserComponent : Component
{
    /// <summary>
    /// Stores knowledge prototypes IDs that the user knows, as well as progress towards them.
    /// </summary>
    [DataField, AutoNetworkedField]
    public Dictionary<ProtoId<KnowledgePrototype>, int> KnowledgeSet;
}
