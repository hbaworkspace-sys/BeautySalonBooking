namespace BeautySalonBooking.Contracts.Permission.Dtos;

public class MenuDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string LinkAddress { get; set; } = string.Empty;
    public int ParentId { get; set; }
    public int Order { get; set; }
    public int Type { get; set; }  // menu, submenu, action
    //public string Tag1 { get; set; } = string.Empty;

    //public string Tag2 { get; set; } = string.Empty;
    public List<MenuDto> Children { get; set; } = new();
}

public class UserMenuResponse
{
    public List<MenuDto> Menus { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}

public class CreateMenu
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public int Type { get; set; }
    public int ParentId { get; set; }
    public string LinkAddress { get; set; } = string.Empty;
    public string Tag1 { get; set; } = string.Empty;

    public string Tag2 { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

}
public class ManagmentOperationResponse
{
    public List<ManagmentOperationDto> Menus { get; set; } = new();
    public List<int> ListSelectOperation { get; set; } = new();
}
public class UpdateOperationRequest
{
    public int RoleId { get; set; }
    public List<int> ListSelectOperation { get; set; } = new();
}
public class ManagmentOperationDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public int ParentId { get; set; }

    public int Type { get; set; }  // menu, submenu, action


}
