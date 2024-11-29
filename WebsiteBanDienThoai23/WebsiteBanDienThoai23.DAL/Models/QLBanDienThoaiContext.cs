using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebsiteBanDienThoai23.DAL.Models
{
    public partial class QLBanDienThoaiContext : DbContext
    {
        public QLBanDienThoaiContext()
        {
        }

        public QLBanDienThoaiContext(DbContextOptions<QLBanDienThoaiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaoHanh> BaoHanhs { get; set; }
        public virtual DbSet<ChiTietBaoHanh> ChiTietBaoHanhs { get; set; }
        public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<DanhGia> DanhGia { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<LoaiSp> LoaiSps { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QLBanDienThoai;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BaoHanh>(entity =>
            {
                entity.HasKey(e => e.MaBh);

                entity.ToTable("BaoHanh");

                entity.Property(e => e.MaBh).HasColumnName("MaBH");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.BaoHanhs)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_BaoHanh_NguoiDung");
            });

            modelBuilder.Entity<ChiTietBaoHanh>(entity =>
            {
                entity.HasKey(e => new { e.MaBh, e.MaSp });

                entity.ToTable("ChiTietBaoHanh");

                entity.Property(e => e.MaBh).HasColumnName("MaBH");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaSP")
                    .IsFixedLength(true);

                entity.Property(e => e.NgayBatDau).HasColumnType("datetime");

                entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

                entity.HasOne(d => d.MaBhNavigation)
                    .WithMany(p => p.ChiTietBaoHanhs)
                    .HasForeignKey(d => d.MaBh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietBaoHanh_BaoHanh");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietBaoHanhs)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietBaoHanh_SanPham");
            });

            modelBuilder.Entity<ChiTietGioHang>(entity =>
            {
                entity.HasKey(e => new { e.MaGh, e.MaSp })
                    .HasName("PK_ChiTietGioHang_1");

                entity.ToTable("ChiTietGioHang");

                entity.Property(e => e.MaGh).HasColumnName("MaGH");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaSP")
                    .IsFixedLength(true);

                entity.HasOne(d => d.MaGhNavigation)
                    .WithMany(p => p.ChiTietGioHangs)
                    .HasForeignKey(d => d.MaGh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietGioHang_GioHang1");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietGioHangs)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietGioHang_SanPham");
            });

            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasKey(e => new { e.MaSp, e.MaHd })
                    .HasName("PK_ChiTietHoaDon_1");

                entity.ToTable("ChiTietHoaDon");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaSP")
                    .IsFixedLength(true);

                entity.Property(e => e.MaHd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaHD")
                    .IsFixedLength(true);

                entity.Property(e => e.DonGia).HasColumnType("money");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaHd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietHoaDon_HoaDon");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietHoaDon_SanPham");
            });

            modelBuilder.Entity<DanhGia>(entity =>
            {
                entity.HasKey(e => new { e.MaSp, e.MaKh });

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaSP")
                    .IsFixedLength(true);

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.BinhLuan).HasMaxLength(50);

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DanhGia_NguoiDung");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DanhGia_SanPham");
            });

            modelBuilder.Entity<GioHang>(entity =>
            {
                entity.HasKey(e => e.MaGh);

                entity.ToTable("GioHang");

                entity.Property(e => e.MaGh).HasColumnName("MaGH");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.GioHangs)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_GioHang_NguoiDung");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.ToTable("HoaDon");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaHD")
                    .IsFixedLength(true);

                entity.Property(e => e.DiaChiGiaoHang).HasMaxLength(50);

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.NgayDatHang).HasColumnType("datetime");

                entity.Property(e => e.NgayNhanHang).HasColumnType("datetime");

                entity.Property(e => e.TongGiaTri).HasColumnType("money");

                entity.Property(e => e.TrangThaiDh).HasColumnName("TrangThaiDH");

                entity.Property(e => e.TrangThaiTt).HasColumnName("TrangThaiTT");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.HoaDonMaKhNavigations)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_HoaDon_NguoiDung1");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.HoaDonMaNvNavigations)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_HoaDon_NguoiDung");
            });

            modelBuilder.Entity<LoaiSp>(entity =>
            {
                entity.HasKey(e => e.MaLoai);

                entity.ToTable("LoaiSP");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Hinh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenLoai).HasMaxLength(30);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_NhanVien");

                entity.ToTable("NguoiDung");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen).HasMaxLength(30);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenTaiKhoan)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaSP")
                    .IsFixedLength(true);

                entity.Property(e => e.Cpu).HasMaxLength(20);

                entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.GiamGia).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.HeDieuHanh).HasMaxLength(20);

                entity.Property(e => e.Hinh)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TenSp)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("TenSP");

                entity.Property(e => e.ThoiGianBh).HasColumnName("ThoiGianBH");

                entity.Property(e => e._5g).HasColumnName("_5G");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK_SanPham_LoaiSP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
