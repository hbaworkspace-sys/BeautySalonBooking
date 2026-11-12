using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.ContentAggregate.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Entities;

namespace BeautySalonBooking.Domain.BranchAggregate.Entities;

public class Branch : AuditableSoftDeleteEntity<long>
{
    public long OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;

    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public string? Glyph { get; private set; }

    private readonly List<BranchMember> _members = new();
    public IReadOnlyCollection<BranchMember> Members => _members;

    private readonly List<BranchService> _services = new();
    public IReadOnlyCollection<BranchService> Services => _services;

    public ICollection<BranchMedia> Media { get; private set; }
    = new List<BranchMedia>();


    private readonly List<ContentItem> _contents = new();
    public IReadOnlyCollection<ContentItem> Contents => _contents.AsReadOnly();

    private Branch() { }

    public Branch(long organizationId, string title, string? description = null, string? glyph = null)
    {
        OrganizationId = organizationId;
        Title = title;
        Description = description;
        Glyph = glyph;
    }

    public void AddMember(BranchMember member) => _members.Add(member);
    public void AddService(BranchService service) => _services.Add(service);
    public void AddMedia(BranchMedia media) => Media.Add(media);

}