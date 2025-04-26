using Content.Shared.Knowledge;
using Content.Shared.Knowledge.Components;
using Content.Shared.Knowledge.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server.Kitchen.EntitySystems;

/// <summary>
/// API for knowledge.
/// </summary>
public sealed class KnowledgeSystem : SharedKnowledgeSystem
{
    [Dependency] private readonly IPrototypeManager _protoMan = default!;
    /// <summary>
    /// Grant an entity knowledge fully, ignoring progress.
    /// </summary>
    public void LearnKnowledge(EntityUid uid, ProtoId<KnowledgePrototype> knowledgeId)
    {
        var comp = EnsureComp<KnowledgeUserComponent>(uid);
        comp.KnowledgeSet[knowledgeId] = Int32.MaxValue;
        Dirty(uid, comp);
    }

    /// <summary>
    /// Removes the knowledge from an entity entirely.
    /// </summary>
    /// <returns>Whether entity had this knowledge.</returns>
    public bool RemoveKnowledge(EntityUid uid, ProtoId<KnowledgePrototype> knowledgeId)
    {
        var comp = EnsureComp<KnowledgeUserComponent>(uid);
        var ret = comp.KnowledgeSet.Remove(knowledgeId);
        Dirty(uid, comp);
        return ret;
    }

    /// <summary>
    /// Grants an entity points towards the knowledge.
    /// points can be negative, but the knowledge won't go below zero.
    /// </summary>
    public void ChangeKnowledgeProgress(EntityUid uid, ProtoId<KnowledgePrototype> knowledgeId, int points)
    {
        var comp = EnsureComp<KnowledgeUserComponent>(uid);
        comp.KnowledgeSet.TryGetValue(knowledgeId, out var progress);
        comp.KnowledgeSet[knowledgeId] = Math.Max(0, progress);
    }

    /// <summary>
    /// Checks if the entity has specified knowledge.
    /// </summary>
    public bool HasKnowledge(EntityUid uid, ProtoId<KnowledgePrototype> knowledgeId)
    {
        var comp = EnsureComp<KnowledgeUserComponent>(uid);
        // you cannot know what does not exist
        if (!_protoMan.TryIndex(knowledgeId, out var proto))
            return false;
        comp.KnowledgeSet.TryGetValue(knowledgeId, out var progress);
        return progress >= proto.ProgressPointsNeeded;
    }
}
