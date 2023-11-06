﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLsanpham.Models.EF;

public partial class MusicWebContext : DbContext
{
    public MusicWebContext()
    {
    }

    public MusicWebContext(DbContextOptions<MusicWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Albumuser> Albumusers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryWithSong> CategoryWithSongs { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Singer> Singers { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<Topicwithsong> Topicwithsongs { get; set; }

    public virtual DbSet<UserWebMusic> UserWebMusics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-CQUSV1J\\DUOCTRUONG;Initial Catalog=MusicWeb;Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__album__3213E83FEBFF353B");

            entity.ToTable("album");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlbumDescription).HasMaxLength(500);
            entity.Property(e => e.AlbumImg).HasMaxLength(300);
            entity.Property(e => e.AlbumName).HasMaxLength(100);
            entity.Property(e => e.Alias)
                .HasMaxLength(200)
                .HasColumnName("alias");
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.ModifiedDate).HasColumnType("date");

            entity.HasMany(d => d.IdCategories).WithMany(p => p.IdAlbums)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryWithAlbum",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CategoryW__idCat__4222D4EF"),
                    l => l.HasOne<Album>().WithMany()
                        .HasForeignKey("IdAlbum")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CategoryW__idAlb__412EB0B6"),
                    j =>
                    {
                        j.HasKey("IdAlbum", "IdCategory").HasName("PK__Category__8787315383D5252F");
                        j.ToTable("CategoryWithAlbum");
                        j.IndexerProperty<int>("IdAlbum").HasColumnName("idAlbum");
                        j.IndexerProperty<int>("IdCategory").HasColumnName("idCategory");
                    });

            entity.HasMany(d => d.IdSongs).WithMany(p => p.IdAlbums)
                .UsingEntity<Dictionary<string, object>>(
                    "AlbumWithSong",
                    r => r.HasOne<Song>().WithMany()
                        .HasForeignKey("IdSong")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AlbumWith__idSon__5070F446"),
                    l => l.HasOne<Album>().WithMany()
                        .HasForeignKey("IdAlbum")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AlbumWith__idAlb__4F7CD00D"),
                    j =>
                    {
                        j.HasKey("IdAlbum", "IdSong").HasName("PK__AlbumWit__2C6A336212C7C74E");
                        j.ToTable("AlbumWithSong");
                        j.IndexerProperty<int>("IdAlbum").HasColumnName("idAlbum");
                        j.IndexerProperty<int>("IdSong").HasColumnName("idSong");
                    });
        });

        modelBuilder.Entity<Albumuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__albumuse__3213E83FB1134762");

            entity.ToTable("albumuser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idalbum).HasColumnName("idalbum");
            entity.Property(e => e.Idcategory).HasColumnName("idcategory");
            entity.Property(e => e.Idsong).HasColumnName("idsong");

            entity.HasOne(d => d.IdalbumNavigation).WithMany(p => p.Albumusers)
                .HasForeignKey(d => d.Idalbum)
                .HasConstraintName("FK__albumuser__idalb__47A6A41B");

            entity.HasOne(d => d.IdsongNavigation).WithMany(p => p.Albumusers)
                .HasForeignKey(d => d.Idsong)
                .HasConstraintName("FK__albumuser__idson__489AC854");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__category__3213E83F3DC5CB31");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias)
                .HasMaxLength(200)
                .HasColumnName("alias");
            entity.Property(e => e.CategoryDescription).HasMaxLength(500);
            entity.Property(e => e.CategoryImg).HasMaxLength(500);
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.ModifiedDate).HasColumnType("date");
        });

        modelBuilder.Entity<CategoryWithSong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__category__3213E83F8B2338B1");

            entity.ToTable("categoryWithSong");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idcategory).HasColumnName("idcategory");
            entity.Property(e => e.Idsong).HasColumnName("idsong");

            entity.HasOne(d => d.IdcategoryNavigation).WithMany(p => p.CategoryWithSongs)
                .HasForeignKey(d => d.Idcategory)
                .HasConstraintName("FK__categoryW__idcat__625A9A57");

            entity.HasOne(d => d.IdsongNavigation).WithMany(p => p.CategoryWithSongs)
                .HasForeignKey(d => d.Idsong)
                .HasConstraintName("FK__categoryW__idson__634EBE90");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__history__3213E83F9524EAC5");

            entity.ToTable("history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Countlisten).HasColumnName("countlisten");
            entity.Property(e => e.Idsong).HasColumnName("idsong");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Listendate)
                .HasColumnType("datetime")
                .HasColumnName("listendate");
            entity.Property(e => e.Listened).HasColumnName("listened");

            entity.HasOne(d => d.IdsongNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.Idsong)
                .HasConstraintName("FK__history__idsong__3D2915A8");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK__history__iduser__3C34F16F");
        });

        modelBuilder.Entity<Singer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__singer__3213E83F9DF97DE0");

            entity.ToTable("singer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias)
                .HasMaxLength(200)
                .HasColumnName("alias");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Fileimg)
                .HasMaxLength(400)
                .HasColumnName("fileimg");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("date");
            entity.Property(e => e.SingerDescription)
                .HasMaxLength(500)
                .HasColumnName("singerDescription");
            entity.Property(e => e.SingerInfomation)
                .HasMaxLength(500)
                .HasColumnName("singerInfomation");
            entity.Property(e => e.SingerName)
                .HasMaxLength(100)
                .HasColumnName("singerName");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__song__3213E83F30E44511");

            entity.ToTable("song");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias)
                .HasMaxLength(200)
                .HasColumnName("alias");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Fileimg)
                .HasMaxLength(400)
                .HasColumnName("fileimg");
            entity.Property(e => e.Filesong)
                .HasMaxLength(500)
                .HasColumnName("filesong");
            entity.Property(e => e.IdAlbum).HasColumnName("idAlbum");
            entity.Property(e => e.IdSinger).HasColumnName("idSinger");
            entity.Property(e => e.Idcategory).HasColumnName("idcategory");
            entity.Property(e => e.Idtopic).HasColumnName("idtopic");
            entity.Property(e => e.LikeCount).HasColumnName("likeCount");
            entity.Property(e => e.ModifiedDate).HasColumnType("date");
            entity.Property(e => e.RecentListendate)
                .HasColumnType("datetime")
                .HasColumnName("recentListendate");
            entity.Property(e => e.SongDescription)
                .HasMaxLength(500)
                .HasColumnName("songDescription");
            entity.Property(e => e.SongName)
                .HasMaxLength(100)
                .HasColumnName("songName");
            entity.Property(e => e.ViewCount).HasColumnName("viewCount");

            entity.HasOne(d => d.IdAlbumNavigation).WithMany(p => p.Songs)
                .HasForeignKey(d => d.IdAlbum)
                .HasConstraintName("fk_idalbum");

            entity.HasOne(d => d.IdSingerNavigation).WithMany(p => p.Songs)
                .HasForeignKey(d => d.IdSinger)
                .HasConstraintName("FK__song__idSinger__4CA06362");

            entity.HasOne(d => d.IdcategoryNavigation).WithMany(p => p.Songs)
                .HasForeignKey(d => d.Idcategory)
                .HasConstraintName("FK__song__idcategory__4E53A1AA");

            entity.HasOne(d => d.IdtopicNavigation).WithMany(p => p.Songs)
                .HasForeignKey(d => d.Idtopic)
                .HasConstraintName("FK__song__idtopic__4D5F7D71");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__topic__3213E83F9794D3E3");

            entity.ToTable("topic");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias)
                .HasMaxLength(200)
                .HasColumnName("alias");
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.ModifiedDate).HasColumnType("date");
            entity.Property(e => e.TopicDescription).HasMaxLength(500);
            entity.Property(e => e.TopicImg).HasMaxLength(500);
            entity.Property(e => e.TopicName).HasMaxLength(100);
        });

        modelBuilder.Entity<Topicwithsong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__topicwit__3213E83FD48995C6");

            entity.ToTable("topicwithsong");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idsong).HasColumnName("idsong");
            entity.Property(e => e.Idtopic).HasColumnName("idtopic");

            entity.HasOne(d => d.IdsongNavigation).WithMany(p => p.Topicwithsongs)
                .HasForeignKey(d => d.Idsong)
                .HasConstraintName("FK__topicwith__idson__671F4F74");

            entity.HasOne(d => d.IdtopicNavigation).WithMany(p => p.Topicwithsongs)
                .HasForeignKey(d => d.Idtopic)
                .HasConstraintName("FK__topicwith__idtop__662B2B3B");
        });

        modelBuilder.Entity<UserWebMusic>(entity =>
        {
            entity.ToTable("UserWebMusic");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("firstName");
            entity.Property(e => e.Gmail)
                .HasMaxLength(50)
                .HasColumnName("gmail");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LinkAvatar)
                .HasMaxLength(200)
                .HasColumnName("linkAvatar");
            entity.Property(e => e.Note)
                .HasMaxLength(300)
                .HasColumnName("note");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
