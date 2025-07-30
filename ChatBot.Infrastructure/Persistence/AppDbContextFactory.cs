using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChatBot.Infrastructure.Persistence
{
	public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
			//optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ChatBotDb;User Id=sa;Password=Test1234;TrustServerCertificate=True");
			optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ChatBot;Trusted_Connection=True;TrustServerCertificate=True;");

			return new AppDbContext(optionsBuilder.Options);
		}

	}
}