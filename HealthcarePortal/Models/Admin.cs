using System;
using System.Collections.Generic;
using HealthcarePortal.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace HealthcarePortal.Models;

public partial class Admin
{
    public int AId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? Dob { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }
}


public static class AdminEndpoints
{
	public static void MapAdminEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Admin").WithTags(nameof(Admin));

        group.MapGet("/", async (HealthcareContext db) =>
        {
            return await db.Admins.ToListAsync();
        })
        .WithName("GetAllAdmins")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Admin>, NotFound>> (int aid, HealthcareContext db) =>
        {
            return await db.Admins.AsNoTracking()
                .FirstOrDefaultAsync(model => model.AId == aid)
                is Admin model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAdminById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int aid, Admin admin, HealthcareContext db) =>
        {
            var affected = await db.Admins
                .Where(model => model.AId == aid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.AId, admin.AId)
                  .SetProperty(m => m.Name, admin.Name)
                  .SetProperty(m => m.Dob, admin.Dob)
                  .SetProperty(m => m.Phone, admin.Phone)
                  .SetProperty(m => m.Email, admin.Email)
                  .SetProperty(m => m.Address, admin.Address)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAdmin")
        .WithOpenApi();

        group.MapPost("/", async (Admin admin, HealthcareContext db) =>
        {
            db.Admins.Add(admin);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Admin/{admin.AId}",admin);
        })
        .WithName("CreateAdmin")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int aid, HealthcareContext db) =>
        {
            var affected = await db.Admins
                .Where(model => model.AId == aid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAdmin")
        .WithOpenApi();
    }
}