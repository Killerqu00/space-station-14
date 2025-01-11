using Content.Shared.CCVar;
using Content.Shared.Inventory;
using Content.Shared.StepTrigger.Systems;
using Robust.Shared.Configuration;

namespace Content.Shared.Contraband;

/// <summary>
/// This handles ScanFrisker.
/// </summary>
public sealed class ContrabandScannerSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _configuration = default!;
    [Dependency] private readonly InventorySystem _inventory = default!;

    private bool _contrabandExamineEnabled;

    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<ContrabandScannerComponent, StepTriggeredOnEvent>(OnStepTriggered);

        Subs.CVar(_configuration, CCVars.ContrabandExamine, SetContrabandExamine, true);
    }

    private void OnStepTriggered(EntityUid uid, ContrabandScannerComponent component, ref StepTriggeredOnEvent args)
    {
        if (!_contrabandExamineEnabled)
            return;
        var searchTarget = args.Tripper;
        foreach (var item in _inventory.GetHandOrInventoryEntities(searchTarget))
        {
            CheckItemForLegality(item, searchTarget);
        }
    }

    private void SetContrabandExamine(bool val)
    {
        _contrabandExamineEnabled = val;
    }
}
