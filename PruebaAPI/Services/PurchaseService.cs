﻿using Microsoft.EntityFrameworkCore;
using Shared.Models;
using PruebaAPI.Dal;
using PruebaAPI.Interfaces;

namespace PruebaAPI.Services
{
    public class PurchaseService(Context context) : IPuchaseService
    {
        private readonly Context _context = context;

        public async Task<List<Compras>> GetAsync()
        {
            return await _context.Compras.ToListAsync();
        }
        

        public async Task<bool> AddAsync(Compras compra)
        {
            await _context.Compras.AddAsync(compra);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Compras> DeleteAsync(int id)
        {
            var puchase = await _context.Compras.FindAsync(id) ?? throw new KeyNotFoundException($"Puchase with ID {id} was no found");

            _context.Compras.Remove(puchase);
            await _context.SaveChangesAsync();

            return puchase;



        }

        public async Task<Compras> PutAsync(Compras compra)
        {
            var existingCompra = await _context.Compras.FindAsync(compra.CompraId) ??
                throw new KeyNotFoundException($"Compra with ID {compra.CompraId} was not found");

            _context.Entry(existingCompra).State = EntityState.Detached;

            var result = _context.Compras.Update(compra);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
