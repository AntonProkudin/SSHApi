using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SSHApi.Entities;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    { }
    public virtual DbSet<vclTBL> TBLvcl { get; set; } = null!;
    public virtual DbSet<UserTBL> TBLUser { get; set; } = null!;
}
