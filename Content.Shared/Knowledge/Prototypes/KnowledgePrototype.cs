using Robust.Shared.Prototypes;

namespace Content.Shared.Knowledge.Prototypes;

/// <summary>
/// Defines an abstract label on a player that tells "This person can do that thing".
/// </summary>
[Prototype()]
public sealed partial class KnowledgePrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = default!;

    /// <summary>
    /// Name of the knowledge. Visible in character menu.
    /// </summary>
    [DataField(required: true)]
    public LocId Name;

    /// <summary>
    /// Display color of the knowledge name. Useful if you have "groups" of knowledge.
    /// </summary>
    [DataField]
    public Color Color = Color.White;

    /// <summary>
    /// Display color if the knowledge is only partial - <see cref="ShowProgress"/>
    /// </summary>
    [DataField]
    public Color ProgressColor = Color.Gray;

    /// <summary>
    /// Points until knowledge is fully learned.
    /// If your progress is below that number, you don't have the knowledge.
    /// </summary>
    [DataField]
    public int ProgressPointsNeeded = 1;

    /// <summary>
    /// Whether to show partial progress towards the knowledge.
    /// </summary>
    [DataField]
    public bool ShowProgress = true;

    /// <summary>
    /// If true, shows progress as a percentage towards <see cref="ProgressPointsNeeded"/>.
    /// If false, shows raw current and needed point value instead.
    /// </summary>
    [DataField]
    public bool IsPercentageMode = true;
}
