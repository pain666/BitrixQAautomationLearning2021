using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace atFrameWork2.Models
{
    public partial class atf2_logContext : DbContext
    {
        public atf2_logContext()
        {
        }

        public atf2_logContext(DbContextOptions<atf2_logContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CaseResult> CaseResults { get; set; }
        public virtual DbSet<LogContent> LogContents { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=192.168.2.222;database=atf2_log;userid=atf2;password=%TGB3edc!QAZ;convertzerodatetime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.24-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<CaseResult>(entity =>
            {
                entity.ToTable("case_result");

                entity.HasIndex(e => e.SessionId, "case_sess_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CaseFinishTime)
                    .HasColumnType("datetime")
                    .HasColumnName("case_finish_time");

                entity.Property(e => e.CaseStartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("case_start_time");

                entity.Property(e => e.CaseTitle)
                    .HasMaxLength(150)
                    .HasColumnName("case_title");

                entity.Property(e => e.ErrorCount).HasColumnName("error_count");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.CaseResults)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("case_sess_id");
            });

            modelBuilder.Entity<LogContent>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PRIMARY");

                entity.ToTable("log_content");

                entity.HasIndex(e => e.MessageCaseId, "msg_case_id_idx");

                entity.HasIndex(e => e.MessageSessionId, "msg_sess_id_idx");

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.MessageCaseId).HasColumnName("message_case_id");

                entity.Property(e => e.MessageSessionId).HasColumnName("message_session_id");

                entity.Property(e => e.MessageText).HasColumnName("message_text");

                entity.Property(e => e.MessageTime)
                    .HasColumnType("datetime")
                    .HasColumnName("message_time");

                entity.Property(e => e.MessageType)
                    .HasMaxLength(45)
                    .HasColumnName("message_type");

                entity.HasOne(d => d.MessageCase)
                    .WithMany(p => p.LogContents)
                    .HasForeignKey(d => d.MessageCaseId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("msg_case_id");

                entity.HasOne(d => d.MessageSession)
                    .WithMany(p => p.LogContents)
                    .HasForeignKey(d => d.MessageSessionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("msg_sess_id");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("session");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.SessionStarttime)
                    .HasColumnType("datetime")
                    .HasColumnName("session_starttime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
