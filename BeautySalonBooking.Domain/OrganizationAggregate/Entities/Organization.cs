using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Enums;

namespace BeautySalonBooking.Domain.OrganizationAggregate.Entities
{

    public class Organization : AuditableSoftDeleteEntity<long>
    {
        public string Title { get; private set; } = null!;
        public string LicenseNumber { get; private set; } = null!;

        public OrganizationType Type { get; private set; }

        private readonly List<OrganizationOwner> _owners = new();
        public IReadOnlyCollection<OrganizationOwner> Owners => _owners;


        private readonly List<OrganizationMedia> _media = new();
        public IReadOnlyCollection<OrganizationMedia> Media => _media;


        private readonly List<Branch> _branches = new();
        public IReadOnlyCollection<Branch> Branches => _branches;

        public string? Signature { get; private set; }
        public string? Slogan { get; private set; }


        public string? Glyph { get; private set; }
        public OrganizationDetail Detail { get; private set; }

        private Organization() { }

        public Organization(string title, string licenseNumber, OrganizationType type, string? slogan = null)
        {
            Title = title;
            LicenseNumber = licenseNumber;
            Type = type;
            Slogan = slogan;
        }

        public void AddOwner(OrganizationOwner owner) => _owners.Add(owner);
        public void AddMedia(OrganizationMedia media) => _media.Add(media);
        public void AddBranch(Branch branch) => _branches.Add(branch);

        //private readonly List<OrganizationDetail> _details = new();
        public void SetDetail(OrganizationDetail detail) => Detail = detail;

        //public void AddDetail(OrganizationDetail detail) => _details.Add(detail);


    }
}

