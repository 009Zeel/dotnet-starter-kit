namespace FL_CRMS_ERP_WEBAPI.Application.Identity.Users;

public class UserRoleDto
{
    public string? RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? Description { get; set; }
    public string? ReportTo { get; set; }
    public bool Enabled { get; set; }
}