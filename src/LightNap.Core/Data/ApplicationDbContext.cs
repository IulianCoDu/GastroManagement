using LightNap.Core.Data.Comparers;
using LightNap.Core.Data.Converters;
using LightNap.Core.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LightNap.Core.Data
{
    /// <summary>
    /// Represents the application database context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        /// <summary>
        /// Notifications in the DB.
        /// </summary>
        public DbSet<Notification> Notifications { get; set; } = null!;

        /// <summary>
        /// Refresh tokens in the DB.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        /// <summary>
        /// Static content in the DB.
        /// </summary>
        public DbSet<StaticContent> StaticContents { get; set; } = null!;

        /// <summary>
        /// Static content language variants in the DB.
        /// </summary>
        public DbSet<StaticContentLanguage> StaticContentLanguages { get; set; } = null!;

        /// <summary>
        /// User settings in the DB.
        /// </summary>
        public DbSet<UserSetting> UserSettings { get; set; } = null!;

        /// <summary>
        /// Doctors/Physicians in the DB.
        /// </summary>
        public DbSet<Medic> Medici { get; set; } = null!;

        /// <summary>
        /// Medical equipment/systems in the DB.
        /// </summary>
        public DbSet<Sistem> Sisteme { get; set; } = null!;

        /// <summary>
        /// Ultrasound/Echo reports in the DB.
        /// </summary>
        public DbSet<BuletinEco> BuletineEco { get; set; } = null!;

        /// <summary>
        /// Upper GI endoscopy reports in the DB.
        /// </summary>
        public DbSet<BuletinEds> BuletineEds { get; set; } = null!;

        /// <summary>
        /// Colonoscopy reports in the DB.
        /// </summary>
        public DbSet<BuletinEdi> BuletineEdi { get; set; } = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The DbContext options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext() { }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Notification>()
                .Property(n => n.Data)
                    .HasConversion(new DictionaryStringObjectConverter())
                    .Metadata.SetValueComparer(new DictionaryStringObjectValueComparer());

            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .IsRequired();

            builder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .IsRequired();

            builder.Entity<StaticContent>()
                .HasIndex(sc => sc.Key)
                .IsUnique();

            builder.Entity<StaticContentLanguage>()
                .HasIndex(scl => scl.LanguageCode);

            // Configure Medic entity
            builder.Entity<Medic>()
                .ToTable("Medici")
                .HasKey(m => m.Id);

            builder.Entity<Medic>()
                .Property(m => m.MedicName)
                .HasColumnName("Medic")
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<Medic>()
                .Property(m => m.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            // Configure ApplicationUser to Medic relationship (one-to-one)
            // The foreign key is on Medic table pointing to ApplicationUser
            builder.Entity<Medic>()
                .HasOne(m => m.User)
                .WithOne(u => u.Medic)
                .HasForeignKey<Medic>(m => m.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Medic>()
                .HasIndex(m => m.UserId)
                .IsUnique();

            // Configure Sistem entity
            builder.Entity<Sistem>()
                .ToTable("Sistem")
                .HasKey(s => s.Numarsistem);

            builder.Entity<Sistem>()
                .Property(s => s.Sistem1)
                .HasColumnName("Sistem")
                .HasMaxLength(50);

            // Configure BuletinEco entity
            builder.Entity<BuletinEco>()
                .ToTable("Buletine_ECO")
                .HasKey(b => b.Id);

            builder.Entity<BuletinEco>()
                .Property(b => b.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Entity<BuletinEco>()
                .Property(b => b.Nr)
                .HasColumnName("nr")
                .IsRequired();

            builder.Entity<BuletinEco>()
                .Property(b => b.Nume)
                .HasColumnName("nume")
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<BuletinEco>()
                .Property(b => b.Prenume)
                .HasColumnName("prenume")
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<BuletinEco>()
                .Property(b => b.Virsta)
                .HasColumnName("virsta")
                .IsRequired();

            builder.Entity<BuletinEco>()
                .Property(b => b.Cnp)
                .HasColumnName("CNP")
                .HasMaxLength(13)
                .IsRequired();

            builder.Entity<BuletinEco>()
                .Property(b => b.SeriaCs)
                .HasColumnName("seriaCS")
                .HasMaxLength(6);

            builder.Entity<BuletinEco>()
                .Property(b => b.Domiciliu)
                .HasColumnName("domiciliu")
                .HasMaxLength(125)
                .HasDefaultValue("CRAIOVA, DOLJ");

            builder.Entity<BuletinEco>()
                .Property(b => b.Telefon)
                .HasColumnName("telefon")
                .HasMaxLength(50);

            builder.Entity<BuletinEco>()
                .Property(b => b.Diagnostic)
                .HasColumnName("diagnostic");

            builder.Entity<BuletinEco>()
                .Property(b => b.Ficat)
                .HasColumnName("ficat")
                .HasDefaultValue("DIAMETRU ANTEROPOSTERIOR LOB STING 0 cm, PRERENAL LOB DREPT 0 cm, ECOSTRUCTURA OMOGENA, CONTUR REGULAT, FARA PROCESE LOCALIZATE");

            builder.Entity<BuletinEco>()
                .Property(b => b.Colecist)
                .HasColumnName("colecist")
                .HasDefaultValue("PERETI NORMALI, FARA CALCULI");

            builder.Entity<BuletinEco>()
                .Property(b => b.Vp)
                .HasColumnName("VP");

            builder.Entity<BuletinEco>()
                .Property(b => b.Vs)
                .HasColumnName("VS");

            builder.Entity<BuletinEco>()
                .Property(b => b.Cbp)
                .HasColumnName("CBP");

            builder.Entity<BuletinEco>()
                .Property(b => b.Pancreas)
                .HasColumnName("pancreas")
                .HasDefaultValue("OMOGEN, DIMENSIUNI NORMALE");

            builder.Entity<BuletinEco>()
                .Property(b => b.Splina)
                .HasColumnName("splina")
                .HasDefaultValue("0 cm AX LUNG");

            builder.Entity<BuletinEco>()
                .Property(b => b.Rd)
                .HasColumnName("rd")
                .HasDefaultValue("0 cm AX LUNG, IP 0 cm, FARA CALCULI, FARA DILATATII");

            builder.Entity<BuletinEco>()
                .Property(b => b.Rs)
                .HasColumnName("rs")
                .HasDefaultValue("0 cm AX LUNG, IP 0 cm, FARA CALCULI, FARA DILATATII");

            builder.Entity<BuletinEco>()
                .Property(b => b.Vu)
                .HasColumnName("vu")
                .HasDefaultValue("GOLITA");

            builder.Entity<BuletinEco>()
                .Property(b => b.Prostata)
                .HasColumnName("prostata")
                .HasDefaultValue("NORMALA");

            builder.Entity<BuletinEco>()
                .Property(b => b.Ogi)
                .HasColumnName("ogi")
                .HasDefaultValue("NORMALE");

            builder.Entity<BuletinEco>()
                .Property(b => b.Obs)
                .HasColumnName("obs");

            builder.Entity<BuletinEco>()
                .Property(b => b.Data)
                .HasColumnName("data")
                .HasColumnType("datetime2(0)");

            builder.Entity<BuletinEco>()
                .Property(b => b.Ora)
                .HasColumnName("ora")
                .HasColumnType("datetime2(0)");

            builder.Entity<BuletinEco>()
                .Property(b => b.Medic)
                .HasColumnName("medic")
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<BuletinEco>()
                .Property(b => b.Fig1)
                .HasColumnName("Fig1");

            builder.Entity<BuletinEco>()
                .Property(b => b.Fig2)
                .HasColumnName("Fig2");

            builder.Entity<BuletinEco>()
                .Property(b => b.Fig3)
                .HasColumnName("Fig3");

            builder.Entity<BuletinEco>()
                .Property(b => b.Film1)
                .HasColumnName("Film1");

            builder.Entity<BuletinEco>()
                .Property(b => b.Ceus)
                .HasColumnName("CEUS");

            builder.Entity<BuletinEco>()
                .Property(b => b.SsmaTimeStamp)
                .HasColumnName("SSMA_TimeStamp")
                .IsRowVersion();

            // Configure BuletinEds entity
            builder.Entity<BuletinEds>()
                .ToTable("Buletine_EDS")
                .HasKey(b => b.Id);

            builder.Entity<BuletinEds>()
                .Property(b => b.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Entity<BuletinEds>()
                .Property(b => b.Nr)
                .HasColumnName("nr");

            builder.Entity<BuletinEds>()
                .Property(b => b.Nume)
                .HasColumnName("nume")
                .HasMaxLength(50);

            builder.Entity<BuletinEds>()
                .Property(b => b.Prenume)
                .HasColumnName("prenume")
                .HasMaxLength(50);

            builder.Entity<BuletinEds>()
                .Property(b => b.Virsta)
                .HasColumnName("virsta");

            builder.Entity<BuletinEds>()
                .Property(b => b.Cnp)
                .HasColumnName("CNP")
                .HasMaxLength(13);

            builder.Entity<BuletinEds>()
                .Property(b => b.Domiciliu)
                .HasColumnName("domiciliu")
                .HasMaxLength(125)
                .HasDefaultValue("CRAIOVA, DOLJ");

            builder.Entity<BuletinEds>()
                .Property(b => b.Diagnostic)
                .HasColumnName("diagnostic");

            builder.Entity<BuletinEds>()
                .Property(b => b.Sistem)
                .HasColumnName("Sistem")
                .HasMaxLength(50)
                .IsRequired()
                .HasDefaultValue("OLYMPUS EXERA");

            builder.Entity<BuletinEds>()
                .Property(b => b.SedareT)
                .HasColumnName("sedare_t")
                .HasMaxLength(50);

            builder.Entity<BuletinEds>()
                .Property(b => b.Medicatie)
                .HasColumnName("medicatie")
                .HasMaxLength(50);

            builder.Entity<BuletinEds>()
                .Property(b => b.Esofag)
                .HasColumnName("Esofag")
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEds>()
                .Property(b => b.Jonctiune)
                .HasColumnName("Jonctiune")
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEds>()
                .Property(b => b.Stomac)
                .HasColumnName("Stomac")
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEds>()
                .Property(b => b.Pilor)
                .HasColumnName("Pilor")
                .HasMaxLength(250)
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEds>()
                .Property(b => b.Bulb)
                .HasColumnName("Bulb")
                .HasMaxLength(250)
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEds>()
                .Property(b => b.Duoden)
                .HasColumnName("Duoden")
                .HasMaxLength(250)
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEds>()
                .Property(b => b.BiopsiiL)
                .HasColumnName("biopsii_l")
                .HasMaxLength(50);

            builder.Entity<BuletinEds>()
                .Property(b => b.BiopsiiN)
                .HasColumnName("biopsii_n");

            builder.Entity<BuletinEds>()
                .Property(b => b.Nrap)
                .HasColumnName("nrap");

            builder.Entity<BuletinEds>()
                .Property(b => b.BiopsiiR)
                .HasColumnName("biopsii_r");

            builder.Entity<BuletinEds>()
                .Property(b => b.Tratament)
                .HasColumnName("Tratament");

            builder.Entity<BuletinEds>()
                .Property(b => b.Data)
                .HasColumnName("data")
                .HasColumnType("datetime2(0)");

            builder.Entity<BuletinEds>()
                .Property(b => b.Ora)
                .HasColumnName("ora")
                .HasColumnType("datetime2(0)");

            builder.Entity<BuletinEds>()
                .Property(b => b.Medic)
                .HasColumnName("medic")
                .HasMaxLength(50);

            builder.Entity<BuletinEds>()
                .Property(b => b.SsmaTimeStamp)
                .HasColumnName("SSMA_TimeStamp")
                .IsRowVersion();

            // Configure BuletinEdi entity
            builder.Entity<BuletinEdi>()
                .ToTable("Buletine_EDI")
                .HasKey(b => b.Id);

            builder.Entity<BuletinEdi>()
                .Property(b => b.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Entity<BuletinEdi>()
                .Property(b => b.Nr)
                .HasColumnName("nr");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Nume)
                .HasColumnName("nume")
                .HasMaxLength(50);

            builder.Entity<BuletinEdi>()
                .Property(b => b.Prenume)
                .HasColumnName("prenume")
                .HasMaxLength(50);

            builder.Entity<BuletinEdi>()
                .Property(b => b.Virsta)
                .HasColumnName("virsta");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Cnp)
                .HasColumnName("CNP")
                .HasMaxLength(13);

            builder.Entity<BuletinEdi>()
                .Property(b => b.Domiciliu)
                .HasColumnName("domiciliu")
                .HasMaxLength(125)
                .HasDefaultValue("CRAIOVA, DOLJ");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Diagnostic)
                .HasColumnName("diagnostic");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Sistem)
                .HasColumnName("Sistem")
                .HasMaxLength(50)
                .IsRequired()
                .HasDefaultValue("OLYMPUS EXERA");

            builder.Entity<BuletinEdi>()
                .Property(b => b.SedareT)
                .HasColumnName("sedare_t")
                .HasMaxLength(50);

            builder.Entity<BuletinEdi>()
                .Property(b => b.Medicatie)
                .HasColumnName("medicatie")
                .HasMaxLength(50);

            builder.Entity<BuletinEdi>()
                .Property(b => b.Ileon)
                .HasColumnName("Ileon")
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Cec)
                .HasColumnName("Cec")
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Ascendent)
                .HasColumnName("Ascendent")
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Transvers)
                .HasColumnName("Transvers")
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Descendent)
                .HasColumnName("Descendent")
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Sigmoid)
                .HasColumnName("Sigmoid")
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Rect)
                .HasColumnName("Rect")
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Jar)
                .HasColumnName("JAR")
                .HasMaxLength(250)
                .HasDefaultValue("NORMAL");

            builder.Entity<BuletinEdi>()
                .Property(b => b.BiopsiiL)
                .HasColumnName("biopsii_l")
                .HasMaxLength(50);

            builder.Entity<BuletinEdi>()
                .Property(b => b.BiopsiiN)
                .HasColumnName("biopsii_n");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Nrap)
                .HasColumnName("nrap");

            builder.Entity<BuletinEdi>()
                .Property(b => b.BiopsiiR)
                .HasColumnName("biopsii_r");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Tratament)
                .HasColumnName("Tratament");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Data)
                .HasColumnName("data")
                .HasColumnType("datetime2(0)");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Ora)
                .HasColumnName("ora")
                .HasColumnType("datetime2(0)");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Medic)
                .HasColumnName("medic")
                .HasMaxLength(50);

            builder.Entity<BuletinEdi>()
                .Property(b => b.BiopsiiL1)
                .HasColumnName("biopsii_l1")
                .HasMaxLength(255);

            builder.Entity<BuletinEdi>()
                .Property(b => b.BiopsiiN1)
                .HasColumnName("biopsii_n1");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Nrap1)
                .HasColumnName("nrap1");

            builder.Entity<BuletinEdi>()
                .Property(b => b.BiopsiiR1)
                .HasColumnName("biopsii_r1");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Data1)
                .HasColumnName("data1")
                .HasColumnType("datetime2(0)");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Ora1)
                .HasColumnName("ora1")
                .HasColumnType("datetime2(0)");

            builder.Entity<BuletinEdi>()
                .Property(b => b.SedareT1)
                .HasColumnName("sedare_t1")
                .HasMaxLength(255);

            builder.Entity<BuletinEdi>()
                .Property(b => b.DozaS)
                .HasColumnName("doza_s");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Medicatie1)
                .HasColumnName("medicatie1")
                .HasMaxLength(255);

            builder.Entity<BuletinEdi>()
                .Property(b => b.DozaM)
                .HasColumnName("doza_m");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Fig1)
                .HasColumnName("Fig1");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Fig2)
                .HasColumnName("Fig2");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Fig3)
                .HasColumnName("Fig3");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Film1)
                .HasColumnName("Film1");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Consumabile)
                .HasColumnName("Consumabile");

            builder.Entity<BuletinEdi>()
                .Property(b => b.Materiale)
                .HasColumnName("Materiale");

            builder.Entity<BuletinEdi>()
                .Property(b => b.SsmaTimeStamp)
                .HasColumnName("SSMA_TimeStamp")
                .IsRowVersion();
        }

        /// <inheritdoc />
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // Make sure all DateTime properties are stored as UTC.
            configurationBuilder.Properties<DateTime>().HaveConversion<UtcValueConverter>();
        }
    }
}
