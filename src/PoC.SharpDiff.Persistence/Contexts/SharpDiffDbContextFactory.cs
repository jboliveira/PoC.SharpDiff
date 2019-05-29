using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PoC.SharpDiff.Persistence.Contexts
{
    /// <summary> SharpDiff dbcontext factory. </summary>
    public class SharpDiffDbContextFactory : IDesignTimeDbContextFactory<SharpDiffDbContext>
    {
        /// <summary> Create DbContext Code First Helper </summary>
        /// <param name="args">Arguments</param>
        /// <returns>DbContext config options.</returns>
        public SharpDiffDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SharpDiffDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=SharpDiffDb;User=sa;Password=Priority1;");
            return new SharpDiffDbContext(optionsBuilder.Options);
        }
    }
}
