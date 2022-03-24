using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class PlanerixContext : DbContext
    {
        public PlanerixContext()
        {
        }

        public PlanerixContext(DbContextOptions<PlanerixContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abteilung> Abteilungs { get; set; }
        public virtual DbSet<AbteilungFunktion> AbteilungFunktions { get; set; }
        public virtual DbSet<AbteilungMitarbeiter> AbteilungMitarbeiters { get; set; }
        public virtual DbSet<Benutzer> Benutzers { get; set; }
        public virtual DbSet<Dokument> Dokuments { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<EmailGesendet> EmailGesendets { get; set; }
        public virtual DbSet<EmailVorlage> EmailVorlages { get; set; }
        public virtual DbSet<Ereigni> Ereignis { get; set; }
        public virtual DbSet<EreignisMitarbeiter> EreignisMitarbeiters { get; set; }
        public virtual DbSet<Funktion> Funktions { get; set; }
        public virtual DbSet<FunktionMitarbeiter> FunktionMitarbeiters { get; set; }
        public virtual DbSet<ImpMitarbeiter> ImpMitarbeiters { get; set; }
        public virtual DbSet<Kategorie> Kategories { get; set; }
        public virtual DbSet<Menupunkt> Menupunkts { get; set; }
        public virtual DbSet<MenupunktRight> MenupunktRights { get; set; }
        public virtual DbSet<Mitarbeiter> Mitarbeiters { get; set; }
        public virtual DbSet<Objekt> Objekts { get; set; }
        public virtual DbSet<PczugangVorschlag> PczugangVorschlags { get; set; }
        public virtual DbSet<Progabfunk> Progabfunks { get; set; }
        public virtual DbSet<Programm> Programms { get; set; }
        public virtual DbSet<ProgrammMitarbeiter> ProgrammMitarbeiters { get; set; }
        public virtual DbSet<ProgrammVorschlag> ProgrammVorschlags { get; set; }
        public virtual DbSet<Verleih> Verleihs { get; set; }
        public virtual DbSet<Visum> Visums { get; set; }
        public virtual DbSet<VisumArt> VisumArts { get; set; }
        public virtual DbSet<VisumRight> VisumRights { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Planerix;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Abteilung>(entity =>
            {
                entity.ToTable("Abteilung");

                entity.Property(e => e.AbteilungId)
                    .ValueGeneratedNever()
                    .HasColumnName("AbteilungID");

                entity.Property(e => e.Bezeichnung)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AbteilungFunktion>(entity =>
            {
                entity.HasKey(e => new { e.AbteilungId, e.FunktionId });

                entity.ToTable("AbteilungFunktion");

                entity.Property(e => e.AbteilungId).HasColumnName("AbteilungID");

                entity.Property(e => e.FunktionId).HasColumnName("FunktionID");

                entity.HasOne(d => d.Abteilung)
                    .WithMany(p => p.AbteilungFunktions)
                    .HasForeignKey(d => d.AbteilungId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Funktion)
                    .WithMany(p => p.AbteilungFunktions)
                    .HasForeignKey(d => d.FunktionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AbteilungMitarbeiter>(entity =>
            {
                entity.HasKey(e => new { e.MitarbeiterId, e.AbteilungId });

                entity.ToTable("AbteilungMitarbeiter");

                entity.Property(e => e.MitarbeiterId).HasColumnName("MitarbeiterID");

                entity.Property(e => e.AbteilungId).HasColumnName("AbteilungID");

                entity.HasOne(d => d.Abteilung)
                    .WithMany(p => p.AbteilungMitarbeiters)
                    .HasForeignKey(d => d.AbteilungId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Mitarbeiter)
                    .WithMany(p => p.AbteilungMitarbeiters)
                    .HasForeignKey(d => d.MitarbeiterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AbteilungMitarbeiter_Mitarbeiter_MitarbeiterI");
            });

            modelBuilder.Entity<Benutzer>(entity =>
            {
                entity.ToTable("Benutzer");

                entity.Property(e => e.BenutzerId).HasColumnName("BenutzerID");

                entity.Property(e => e.Benutzername)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Passwort)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Salt).HasColumnType("image");

                entity.Property(e => e.Vorname)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dokument>(entity =>
            {
                entity.ToTable("Dokument");

                entity.Property(e => e.DokumentId).HasColumnName("DokumentID");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.ToTable("Email");

                entity.Property(e => e.EmailId).HasColumnName("EmailID");

                entity.Property(e => e.AbteilungId).HasColumnName("AbteilungID");

                entity.Property(e => e.FunktionId).HasColumnName("FunktionID");

                entity.Property(e => e.It).HasColumnName("IT");

                entity.Property(e => e.Pa).HasColumnName("PA");

                entity.Property(e => e.ProgrammId).HasColumnName("ProgrammID");

                entity.HasOne(d => d.Abteilung)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.AbteilungId);

                entity.HasOne(d => d.Funktion)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.FunktionId);

                entity.HasOne(d => d.Programm)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.ProgrammId);
            });

            modelBuilder.Entity<EmailGesendet>(entity =>
            {
                entity.HasKey(e => e.MailitemId);

                entity.ToTable("EmailGesendet");

                entity.Property(e => e.MailitemId)
                    .ValueGeneratedNever()
                    .HasColumnName("MailitemID");

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(e => e.VisumId).HasColumnName("VisumID");

                entity.HasOne(d => d.Visum)
                    .WithMany(p => p.EmailGesendets)
                    .HasForeignKey(d => d.VisumId);
            });

            modelBuilder.Entity<EmailVorlage>(entity =>
            {
                entity.ToTable("EmailVorlage");

                entity.Property(e => e.EmailVorlageId).HasColumnName("EmailVorlageID");

                entity.Property(e => e.EreignisId).HasColumnName("EreignisID");

                entity.Property(e => e.VisumArtId).HasColumnName("VisumArtID");

                entity.HasOne(d => d.Ereignis)
                    .WithMany(p => p.EmailVorlages)
                    .HasForeignKey(d => d.EreignisId);

                entity.HasOne(d => d.VisumArt)
                    .WithMany(p => p.EmailVorlages)
                    .HasForeignKey(d => d.VisumArtId);
            });

            modelBuilder.Entity<Ereigni>(entity =>
            {
                entity.HasKey(e => e.EreignisId);

                entity.Property(e => e.EreignisId)
                    .ValueGeneratedNever()
                    .HasColumnName("EreignisID");

                entity.Property(e => e.Bezeichnung)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EreignisMitarbeiter>(entity =>
            {
                entity.ToTable("EreignisMitarbeiter");

                entity.Property(e => e.EreignisMitarbeiterId).HasColumnName("EreignisMitarbeiterID");

                entity.Property(e => e.BenutzerId).HasColumnName("BenutzerID");

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(e => e.Eguid).HasColumnName("EGuid");

                entity.Property(e => e.EreignisId).HasColumnName("EreignisID");

                entity.Property(e => e.Info)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.InfoErledigt)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.MitarbeiterId).HasColumnName("MitarbeiterID");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.HasOne(d => d.Benutzer)
                    .WithMany(p => p.EreignisMitarbeiters)
                    .HasForeignKey(d => d.BenutzerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Ereignis)
                    .WithMany(p => p.EreignisMitarbeiters)
                    .HasForeignKey(d => d.EreignisId);

                entity.HasOne(d => d.Mitarbeiter)
                    .WithMany(p => p.EreignisMitarbeiters)
                    .HasForeignKey(d => d.MitarbeiterId);
            });

            modelBuilder.Entity<Funktion>(entity =>
            {
                entity.ToTable("Funktion");

                entity.Property(e => e.FunktionId)
                    .ValueGeneratedNever()
                    .HasColumnName("FunktionID");

                entity.Property(e => e.Bezeichnung)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FunktionMitarbeiter>(entity =>
            {
                entity.HasKey(e => new { e.MitarbeiterId, e.FunktionId });

                entity.ToTable("FunktionMitarbeiter");

                entity.Property(e => e.MitarbeiterId).HasColumnName("MitarbeiterID");

                entity.Property(e => e.FunktionId).HasColumnName("FunktionID");

                entity.HasOne(d => d.Funktion)
                    .WithMany(p => p.FunktionMitarbeiters)
                    .HasForeignKey(d => d.FunktionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Mitarbeiter)
                    .WithMany(p => p.FunktionMitarbeiters)
                    .HasForeignKey(d => d.MitarbeiterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FunktionMitarbeiter_Mitarbeiter_MitarbeiterI");
            });

            modelBuilder.Entity<ImpMitarbeiter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("imp_mitarbeiter");

                entity.Property(e => e.Abteilung).HasMaxLength(255);

                entity.Property(e => e.Austritt).HasColumnType("datetime");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.Eintritt).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Funktion).HasMaxLength(255);

                entity.Property(e => e.Geburtsdatum).HasColumnType("datetime");

                entity.Property(e => e.Gruppe).HasMaxLength(255);

                entity.Property(e => e.Itzugang)
                    .HasMaxLength(255)
                    .HasColumnName("ITZugang");

                entity.Property(e => e.Kurzzeichen).HasMaxLength(255);

                entity.Property(e => e.Leitung).HasMaxLength(255);

                entity.Property(e => e.Mail).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Personalnummer).HasMaxLength(255);

                entity.Property(e => e.Vorname).HasMaxLength(255);
            });

            modelBuilder.Entity<Kategorie>(entity =>
            {
                entity.ToTable("Kategorie");

                entity.Property(e => e.KategorieId).HasColumnName("KategorieID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menupunkt>(entity =>
            {
                entity.ToTable("Menupunkt");

                entity.Property(e => e.MenupunktId)
                    .ValueGeneratedNever()
                    .HasColumnName("MenupunktID");
            });

            modelBuilder.Entity<MenupunktRight>(entity =>
            {
                entity.ToTable("MenupunktRight");

                entity.Property(e => e.MenupunktRightId).HasColumnName("MenupunktRightID");

                entity.Property(e => e.BenutzerId).HasColumnName("BenutzerID");

                entity.Property(e => e.MenupunktId).HasColumnName("MenupunktID");

                entity.HasOne(d => d.Benutzer)
                    .WithMany(p => p.MenupunktRights)
                    .HasForeignKey(d => d.BenutzerId);

                entity.HasOne(d => d.Menupunkt)
                    .WithMany(p => p.MenupunktRights)
                    .HasForeignKey(d => d.MenupunktId);
            });

            modelBuilder.Entity<Mitarbeiter>(entity =>
            {
                entity.ToTable("Mitarbeiter");

                entity.Property(e => e.MitarbeiterId).HasColumnName("MitarbeiterID");

                entity.Property(e => e.Austritt).HasColumnType("datetime");

                entity.Property(e => e.Bemerkung)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.Eintritt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Geburtsdatum).HasColumnType("datetime");

                entity.Property(e => e.InitialPasswort)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.KeinEdvzugriff).HasColumnName("KeinEDVZugriff");

                entity.Property(e => e.Kurzzeichen)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LetzterArbeitstag).HasColumnType("datetime");

                entity.Property(e => e.Mguid).HasColumnName("MGuid");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Pczugang).HasColumnName("PCZugang");

                entity.Property(e => e.Personalnummer)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Vorname)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Objekt>(entity =>
            {
                entity.ToTable("Objekt");

                entity.Property(e => e.ObjektId).HasColumnName("ObjektID");

                entity.Property(e => e.Aktiv).HasColumnName("aktiv");

                entity.Property(e => e.Begruendung)
                    .HasColumnType("text")
                    .HasColumnName("begruendung");

                entity.Property(e => e.KategorieId).HasColumnName("KategorieID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Kategorie)
                    .WithMany(p => p.Objekts)
                    .HasForeignKey(d => d.KategorieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Objekt_Kategorie");
            });

            modelBuilder.Entity<PczugangVorschlag>(entity =>
            {
                entity.ToTable("PCZugangVorschlag");

                entity.Property(e => e.PczugangVorschlagId).HasColumnName("PCZugangVorschlagID");

                entity.Property(e => e.FunktionId).HasColumnName("FunktionID");

                entity.Property(e => e.Pczugang).HasColumnName("PCZugang");

                entity.HasOne(d => d.Funktion)
                    .WithMany(p => p.PczugangVorschlags)
                    .HasForeignKey(d => d.FunktionId);
            });

            modelBuilder.Entity<Progabfunk>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("progabfunk");

                entity.Property(e => e.AbteilungId).HasColumnName("Abteilung ID");

                entity.Property(e => e.Aid).HasColumnName("AID");

                entity.Property(e => e.Fid)
                    .HasMaxLength(255)
                    .HasColumnName("FID");

                entity.Property(e => e.FunktionId)
                    .HasMaxLength(255)
                    .HasColumnName("Funktion ID");

                entity.Property(e => e.ProgrammId).HasColumnName("Programm ID");
            });

            modelBuilder.Entity<Programm>(entity =>
            {
                entity.ToTable("Programm");

                entity.Property(e => e.ProgrammId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProgrammID");

                entity.Property(e => e.Bezeichnung)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProgrammMitarbeiter>(entity =>
            {
                entity.HasKey(e => new { e.ProgrammId, e.MitarbeiterId });

                entity.ToTable("ProgrammMitarbeiter");

                entity.Property(e => e.ProgrammId).HasColumnName("ProgrammID");

                entity.Property(e => e.MitarbeiterId).HasColumnName("MitarbeiterID");

                entity.HasOne(d => d.Mitarbeiter)
                    .WithMany(p => p.ProgrammMitarbeiters)
                    .HasForeignKey(d => d.MitarbeiterId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Programm)
                    .WithMany(p => p.ProgrammMitarbeiters)
                    .HasForeignKey(d => d.ProgrammId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProgrammVorschlag>(entity =>
            {
                entity.ToTable("ProgrammVorschlag");

                entity.Property(e => e.ProgrammVorschlagId).HasColumnName("ProgrammVorschlagID");

                entity.Property(e => e.AbteilungId).HasColumnName("AbteilungID");

                entity.Property(e => e.FunktionId).HasColumnName("FunktionID");

                entity.Property(e => e.ProgrammId).HasColumnName("ProgrammID");

                entity.HasOne(d => d.Abteilung)
                    .WithMany(p => p.ProgrammVorschlags)
                    .HasForeignKey(d => d.AbteilungId);

                entity.HasOne(d => d.Funktion)
                    .WithMany(p => p.ProgrammVorschlags)
                    .HasForeignKey(d => d.FunktionId);

                entity.HasOne(d => d.Programm)
                    .WithMany(p => p.ProgrammVorschlags)
                    .HasForeignKey(d => d.ProgrammId);
            });

            modelBuilder.Entity<Verleih>(entity =>
            {
                entity.ToTable("Verleih");

                entity.Property(e => e.VerleihId).HasColumnName("VerleihID");

                entity.Property(e => e.AbgeschlossenDatum).HasColumnType("datetime");

                entity.Property(e => e.Eingetragen).HasColumnType("datetime");

                entity.Property(e => e.ErhaltDatum).HasColumnType("datetime");

                entity.Property(e => e.MitarbeiterId).HasColumnName("MitarbeiterID");

                entity.Property(e => e.ObjektId).HasColumnName("ObjektID");

                entity.Property(e => e.Unterschrift).HasColumnType("text");

                entity.HasOne(d => d.Mitarbeiter)
                    .WithMany(p => p.Verleihs)
                    .HasForeignKey(d => d.MitarbeiterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Verleih_Mitarbeiter");

                entity.HasOne(d => d.Objekt)
                    .WithMany(p => p.Verleihs)
                    .HasForeignKey(d => d.ObjektId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Verleih_Objekt1");
            });

            modelBuilder.Entity<Visum>(entity =>
            {
                entity.ToTable("Visum");

                entity.Property(e => e.VisumId).HasColumnName("VisumID");

                entity.Property(e => e.AbteilungId).HasColumnName("AbteilungID");

                entity.Property(e => e.EreignisMitarbeiterId).HasColumnName("EreignisMitarbeiterID");

                entity.Property(e => e.Info)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.ProgrammId).HasColumnName("ProgrammID");

                entity.Property(e => e.Vguid).HasColumnName("VGuid");

                entity.Property(e => e.VisiertAm).HasColumnType("datetime");

                entity.Property(e => e.VisiertVonBenutzerId).HasColumnName("VisiertVonBenutzerID");

                entity.Property(e => e.VisumArtId).HasColumnName("VisumArtID");

                entity.HasOne(d => d.Abteilung)
                    .WithMany(p => p.Visums)
                    .HasForeignKey(d => d.AbteilungId);

                entity.HasOne(d => d.EreignisMitarbeiter)
                    .WithMany(p => p.Visums)
                    .HasForeignKey(d => d.EreignisMitarbeiterId);

                entity.HasOne(d => d.Programm)
                    .WithMany(p => p.Visums)
                    .HasForeignKey(d => d.ProgrammId);

                entity.HasOne(d => d.VisiertVonBenutzer)
                    .WithMany(p => p.Visums)
                    .HasForeignKey(d => d.VisiertVonBenutzerId);

                entity.HasOne(d => d.VisumArt)
                    .WithMany(p => p.Visums)
                    .HasForeignKey(d => d.VisumArtId);
            });

            modelBuilder.Entity<VisumArt>(entity =>
            {
                entity.ToTable("VisumArt");

                entity.Property(e => e.VisumArtId)
                    .ValueGeneratedNever()
                    .HasColumnName("VisumArtID");

                entity.Property(e => e.Bezeichnung)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VisumRight>(entity =>
            {
                entity.ToTable("VisumRight");

                entity.Property(e => e.VisumRightId).HasColumnName("VisumRightID");

                entity.Property(e => e.AbteilungId).HasColumnName("AbteilungID");

                entity.Property(e => e.BenutzerId).HasColumnName("BenutzerID");

                entity.Property(e => e.ProgrammId).HasColumnName("ProgrammID");

                entity.Property(e => e.VisumArtId).HasColumnName("VisumArtID");

                entity.HasOne(d => d.Abteilung)
                    .WithMany(p => p.VisumRights)
                    .HasForeignKey(d => d.AbteilungId);

                entity.HasOne(d => d.Benutzer)
                    .WithMany(p => p.VisumRights)
                    .HasForeignKey(d => d.BenutzerId);

                entity.HasOne(d => d.Programm)
                    .WithMany(p => p.VisumRights)
                    .HasForeignKey(d => d.ProgrammId);

                entity.HasOne(d => d.VisumArt)
                    .WithMany(p => p.VisumRights)
                    .HasForeignKey(d => d.VisumArtId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
