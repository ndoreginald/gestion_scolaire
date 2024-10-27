using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Atelier3.Models.ViewModels;

namespace Atelier3.Models
{
    public class StudentContext : IdentityDbContext<IdentityUser>
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Atelier3.Models.ViewModels.EditRoleViewModel> EditRoleViewModel { get; set; } = default!;

    }
}
