﻿using HostMaster.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HostMaster.Backend.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
{

    public DbSet<Accommodation> Accommodations { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ExtraService> ExtraServices { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomInventoryItem> RoomInventoryItems { get; set; }
    public DbSet<RoomPhoto> RoomPhotos { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<ReservationRoom> ReservationRooms { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<MaintenanceRoom> MaintenanceRooms { get; set; }
    public DbSet<Opinion> Opinions { get; set; }  
    public DbSet<ServiceAvailability> ServiceAvailabilities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Room>().HasIndex(x => new { x.AccommodationId, x.RoomNumber }).IsUnique();
        modelBuilder.Entity<Reservation>().HasIndex(x => new { x.RoomId, x.AccommodationId, x.StartDate, x.EndDate }).IsUnique();
        modelBuilder.Entity<ServiceAvailability>()
         .HasOne<ExtraService>()
         .WithMany(e => e.Availabilities)
         .HasForeignKey(sa => sa.ServiceId);

        DisableCascadingDelete(modelBuilder);
    }

    private void DisableCascadingDelete(ModelBuilder modelBuilder)
    {
        var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
        foreach (var relationship in relationships)
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}