namespace BeautySalonBooking.Application.Permission.Services
{
    //public class MenuService : CrudService<Permission, int, CreateMenu, CreateMenu, CreateMenu>, IMenuService
    //{
    //    private readonly IPermissionRepository _operationRepository;
    //    private readonly IRolePermissionRepository _roleOperationRepository;
    //    private readonly IUserRepository _userRepository;
    //    private readonly ILogger<MenuService> _logger;
    //    private readonly IAuthService _authService;

    //    public MenuService(
    //        IOperationRepository operationRepository,
    //         IMapper mapper,
    //        IUserRepository userRepository,
    //        IRoleOperationRepository roleOperationRepository,
    //        IAuthService authService,
    //        ILogger<MenuService> logger) : base(operationRepository, mapper, logger)
    //    {
    //        _operationRepository = operationRepository;
    //        _userRepository = userRepository;
    //        _logger = logger;
    //        _authService = authService;
    //        _roleOperationRepository = roleOperationRepository;
    //    }

    //    public async Task<ManagmentOperationResponse> GetManagmentOperationsAsync(int id)
    //    {
    //        try
    //        {
    //            var currentUser = await _authService.GetCurrentUserSafeAsync();
    //            if (currentUser == null)
    //            {
    //                throw new UnauthorizedAccessException("User session expired");
    //            }
    //            // دریافت نقش‌های کاربر
    //            var roleIds = await _operationRepository.GetUserRoleIdsAsync(currentUser.Id);

    //            if (!roleIds.Any())
    //            {
    //                _logger.LogWarning("No roles found for user {UserId}", currentUser.Id);
    //                return new ManagmentOperationResponse();
    //            }
    //            List<int> lists = new List<int>();
    //            lists.Add(id);
    //            // دریافت عملیات‌های مجاز
    //            var operationsSelect = await _operationRepository.GetOperationsByRoleIdsAsync(lists);

    //            var allOperations = await _operationRepository.GetAllOperationsAsync();



    //            return new ManagmentOperationResponse
    //            {
    //                Menus = allOperations.Where(x => x.IsActive && !x.IsDeleted).Select(x => new ManagmentOperationDto { Id = x.Id, Code = x.Code, ParentId = x.ParentId, Title = x.Title, Type = x.Type }).ToList(),
    //                ListSelectOperation = operationsSelect.Where(x => x.IsActive && !x.IsDeleted).Select(x => x.Id).ToList(),
    //            };
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Error getting user menus for user {UserId}");
    //            throw;
    //        }
    //    }

    //    public async Task<ApiResponse_New<bool>> UpdateRoleOperation(UpdateOperationRequest model)
    //    {
    //        try
    //        {

    //            // دریافت عملیات‌های مجاز
    //            var operationsSelect = await _roleOperationRepository.UpdateRoleOperation(model);




    //            return operationsSelect;
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Error getting user menus for user {UserId}");
    //            throw;
    //        }
    //    }
    //    public async Task<UserMenuResponse> GetUserOperationsAsync(int userId)
    //    {
    //        try
    //        {
    //            // دریافت نقش‌های کاربر
    //            var roleIds = await _operationRepository.GetUserRoleIdsAsync(userId);

    //            if (!roleIds.Any())
    //            {
    //                _logger.LogWarning("No roles found for user {UserId}", userId);
    //                return new UserMenuResponse();
    //            }

    //            // دریافت عملیات‌های مجاز
    //            var operations = await _operationRepository.GetOperationsByRoleIdsAsync(roleIds);

    //            // ساخت دسترسی درختی
    //            var menus = await BuildOperationTreeAsync(operations);

    //            // دریافت permissions
    //            var permissions = await GetUserPermissionsAsync(userId);

    //            _logger.LogInformation("Retrieved {MenuCount} menus and {PermissionCount} permissions for user {UserId}",
    //                menus.Count, permissions.Count, userId);

    //            return new UserMenuResponse
    //            {
    //                Menus = menus,
    //                Permissions = permissions
    //            };
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Error getting user menus for user {UserId}", userId);
    //            throw;
    //        }
    //    }
    //    public async Task<UserMenuResponse> GetUserMenusAsync(int userId)
    //    {
    //        try
    //        {
    //            // دریافت نقش‌های کاربر
    //            var roleIds = await _operationRepository.GetUserRoleIdsAsync(userId);

    //            if (!roleIds.Any())
    //            {
    //                _logger.LogWarning("No roles found for user {UserId}", userId);
    //                return new UserMenuResponse();
    //            }

    //            // دریافت عملیات‌های مجاز
    //            var operations = await _operationRepository.GetOperationsByRoleIdsAsync(roleIds);

    //            // ساخت منوی درختی
    //            var menus = await BuildMenuTreeAsync(operations);

    //            // دریافت permissions
    //            var permissions = await GetUserPermissionsAsync(userId);

    //            _logger.LogInformation("Retrieved {MenuCount} menus and {PermissionCount} permissions for user {UserId}",
    //                menus.Count, permissions.Count, userId);

    //            return new UserMenuResponse
    //            {
    //                Menus = menus,
    //                Permissions = permissions
    //            };
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Error getting user menus for user {UserId}", userId);
    //            throw;
    //        }
    //    }

    //    public async Task<List<MenuDto>> BuildMenuTreeAsync(List<Permission> operations)
    //    {
    //        var menuDtos = operations
    //            .Where(op => op.Type == 1 || op.Type == 2) // فقط منوهاv  1 menu 2 sumbenu 3 operation 4 suboperation
    //            .Select(op => new MenuDto
    //            {
    //                Id = op.Id,
    //                Title = op.Title,
    //                Code = op.Code,
    //                LinkAddress = op.LinkAddress,
    //                Icon = op.Tag1,
    //                Path = op.Tag2,// GetPathByCode(op.Code),
    //                ParentId = op.ParentId,
    //                Order = GetOrderByCode(op.Code),
    //                Type = op.Type,
    //                Children = new List<MenuDto>()
    //            })
    //            .OrderBy(m => m.Path)
    //            .ToList();

    //        // ساخت ساختار درختی
    //        return menuDtos;//BuildTree(menuDtos);
    //    }
    //    public async Task<List<MenuDto>> BuildOperationTreeAsync(List<Operation> operations)
    //    {
    //        var menuDtos = operations
    //            .Where(op => op.Type == 3 || op.Type == 4) // فقط منوهاv  1 menu 2 sumbenu 3 operation 4 suboperation
    //            .Select(op => new MenuDto
    //            {
    //                Id = op.Id,
    //                Title = op.Title,
    //                Code = op.Code,
    //                Icon = GetIconByCode(op.Code),
    //                Path = GetPathByCode(op.Code),
    //                ParentId = op.ParentId,
    //                Order = GetOrderByCode(op.Code),
    //                Type = op.Type,
    //                Children = new List<MenuDto>()
    //            })
    //            .OrderBy(m => m.Order)
    //            .ToList();

    //        // ساخت ساختار درختی
    //        return BuildTree(menuDtos);
    //    }
    //    public async Task<List<string>> GetUserPermissionsAsync(int userId)
    //    {
    //        var roleIds = await _operationRepository.GetUserRoleIdsAsync(userId);
    //        var operations = await _operationRepository.GetOperationsByRoleIdsAsync(roleIds);

    //        return operations
    //            .Where(op => op.Type == 3 || op.Type == 4) // فقط actions به عنوان permission
    //            .Select(op => op.Code)
    //            .Distinct()
    //            .ToList();
    //    }

    //    private List<MenuDto> BuildTree(List<MenuDto> menus, int parentId = 0)
    //    {
    //        return menus
    //            .Where(m => m.ParentId == parentId)
    //            .Select(m => new MenuDto
    //            {
    //                Id = m.Id,
    //                Title = m.Title,
    //                Code = m.Code,
    //                LinkAddress = m.LinkAddress,
    //                Icon = m.Icon,
    //                Path = m.Path,
    //                ParentId = m.ParentId,
    //                Order = m.Order,
    //                Type = m.Type,
    //                Children = BuildTree(menus, m.Id),

    //            })
    //            .OrderBy(m => m.Order)
    //            .ToList();
    //    }

    //    private string GetIconByCode(string code)
    //    {
    //        return code.ToLower() switch
    //        {
    //            "dashboard" => "mdi-view-dashboard",
    //            "users" => "mdi-account-group",
    //            "reports" => "mdi-chart-bar",
    //            "settings" => "mdi-cog",
    //            "products" => "mdi-package-variant",
    //            "orders" => "mdi-cart",
    //            _ => "mdi-circle-small"
    //        };
    //    }

    //    private string GetPathByCode(string code)
    //    {
    //        return code.ToLower() switch
    //        {
    //            "dashboard" => "/dashboard",
    //            "users" => "/users",
    //            "reports" => "/reports",
    //            "settings" => "/settings",
    //            "products" => "/products",
    //            "orders" => "/orders",
    //            _ => $"/{code.ToLower()}"
    //        };
    //    }

    //    private int GetOrderByCode(string code)
    //    {
    //        return code.ToLower() switch
    //        {
    //            "dashboard" => 1,
    //            "users" => 2,
    //            "products" => 3,
    //            "orders" => 4,
    //            "reports" => 5,
    //            "settings" => 6,
    //            _ => 99
    //        };
    //    }
    //    // حذف آبشاری منوها
    //    public async Task<ApiResponse_New<bool>> DeleteMenuCascadeAsync(int id)
    //    {
    //        try
    //        {
    //            var menu = await _operationRepository.GetByIdAsync(id);
    //            if (menu == null || menu.IsDeleted)
    //            {
    //                return new ApiResponse_New<bool>
    //                {
    //                    Success = false,
    //                    Message = "منوی مورد نظر یافت نشد",
    //                    Code = 404
    //                };
    //            }

    //            // پیدا کردن همه ساب‌منوها به صورت بازگشتی
    //            var allChildrenIds = await GetMenuChildrenIdsRecursive(id);

    //            // حذف همه ساب‌منوها
    //            foreach (var childId in allChildrenIds)
    //            {
    //                await _operationRepository.DeleteAsync(childId);
    //            }

    //            // حذف منوی اصلی
    //            var result = await _operationRepository.DeleteAsync(id);

    //            if (result)
    //            {
    //                return new ApiResponse_New<bool>
    //                {
    //                    Success = true,
    //                    Message = $"منو و {allChildrenIds.Count} زیرمنو با موفقیت حذف شدند",
    //                    Code = 200,
    //                    Payload = true
    //                };
    //            }
    //            else
    //            {
    //                return new ApiResponse_New<bool>
    //                {
    //                    Success = false,
    //                    Message = "خطا در حذف منو",
    //                    Code = 500
    //                };
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Error cascade deleting menu with ID {MenuId}", id);
    //            return new ApiResponse_New<bool>
    //            {
    //                Success = false,
    //                Message = "خطا در حذف منو",
    //                Code = 500
    //            };
    //        }
    //    }

    //    // تابع کمکی برای پیدا کردن ID همه ساب‌منوها
    //    private async Task<List<int>> GetMenuChildrenIdsRecursive(int parentId)
    //    {
    //        var children = await _operationRepository.GetChildByParents(parentId);


    //        var allChildrenIds = new List<int>();

    //        foreach (var childId in children)
    //        {
    //            allChildrenIds.Add(childId.Id);
    //            var grandChildrenIds = await GetMenuChildrenIdsRecursive(childId.Id);
    //            allChildrenIds.AddRange(grandChildrenIds);
    //        }

    //        return allChildrenIds;
    //    }
    //}
}
