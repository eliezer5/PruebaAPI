using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using PruebaAPI.Dal;
using System.Diagnostics.CodeAnalysis;
using PruebaAPI.Interfaces;


namespace PruebaAPI.Services;

public class ProductServices(Context _context) : IProductService
{
    
    public async Task<List<Productos>> GetAsync()
    {
        return await _context.Productos.ToListAsync();
    }
    public async Task<bool> AddAsync(Productos producto)
    {
        _context.Productos.Add(producto);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<Productos> PutAsync(Productos producto)
    {
        var existingProduct = await _context.Productos.FindAsync(producto.ProductoId) ??
               throw new KeyNotFoundException($"Compra with ID {producto.ProductoId} was not found");

        _context.Entry(existingProduct).State = EntityState.Detached;

        var result = _context.Productos.Update(producto);
        await _context.SaveChangesAsync();
        return result.Entity;

    }
    public async Task<Productos> DeleteAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id) ??
            throw new KeyNotFoundException("The product was not found");

        _context.Remove(producto);
        await _context.SaveChangesAsync();

        return producto;
    }


}
