﻿using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;

namespace SistemaLlavesWebAPI.Services
{
    public class WarrantyService(Context context) : IWarrantyService
    {
        private readonly Context _context = context;

        public async Task<List<Garantias>> GetAsync()
        {
            return await _context.Garantias.ToListAsync();
        }
        public async Task<bool> AddAsync(Garantias garantia)
        {
            await _context.Garantias.AddAsync(garantia);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Garantias> PutAsync(Garantias garantia)
        {
          
            var existingWarranty = await _context.Garantias.FindAsync(garantia.GarantiaId) ??
                throw new KeyNotFoundException($"Warranty with ID {garantia.GarantiaId} was not found");

            _context.Entry(existingWarranty).State = EntityState.Detached;

            var result = _context.Garantias.Update(garantia);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Garantias> DeleteAsync(int id)
        {
            var warranty = await _context.Garantias.FindAsync(id) ?? throw new KeyNotFoundException($"Warranty with ID {id} was not found.");
            _context.Garantias.Remove(warranty);
            await _context.SaveChangesAsync();

            return warranty;
        }
    }
}
