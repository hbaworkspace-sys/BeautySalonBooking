namespace BeautySalonBooking.Contracts.Authentication.Dtos;

public class RoleDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
}

public class CreateRoleRequest
{

    public string Code { get; set; }
    public string Title { get; set; }
}
public class UpdateRoleRequest
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
}
