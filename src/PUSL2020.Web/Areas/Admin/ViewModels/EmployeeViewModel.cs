using System.ComponentModel.DataAnnotations;

namespace PUSL2020.Web.Areas.Admin.ViewModels;

public class EmployeeViewModel
{
    [Editable(false)]
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Office { get; set; }
    public string Username { get; set; }
}