using ChatBot.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Infrastructure.Persistence
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<UserEntity> Users => Set<UserEntity>();
		public DbSet<QuestionEntity> Questions => Set<QuestionEntity>();
		public DbSet<AnswerEntity> Answers => Set<AnswerEntity>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<QuestionEntity>()
				.HasOne(a => a.User)
				.WithMany(e => e.Questions)
				.HasForeignKey(a => a.UserEntityId);

			modelBuilder.Entity<QuestionEntity>()
				.HasOne(q => q.Answer)
				.WithOne(a => a.Question)
				.HasForeignKey<AnswerEntity>(a => a.QuestionEntityId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
