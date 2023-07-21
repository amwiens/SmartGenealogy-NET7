using System;
using System.Collections.Generic;
using System.Linq;

using SmartGenealogy.Contracts;
using SmartGenealogy.Persistence;
using SmartGenealogy.Persistence.Models;

namespace SmartGenealogy.Services;

/// <summary>
/// Place repository
/// </summary>
public class PlaceRepository : IDataRepository<Place>
{
    private readonly SmartGenealogyContext _context;

    public PlaceRepository(SmartGenealogyContext context)
    {
        _context = context;
    }

    public IEnumerable<Place> GetAll()
    {
        return _context.Places.ToList();
    }

    public Place Get(long id)
    {
        return _context.Places
            .FirstOrDefault(p => p.Id == id)!;
    }

    public Place Add(Place entity)
    {
        entity.DateCreated = DateTime.Now;
        entity.DateUpdated = DateTime.Now;
        _context.Places.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public Place Update(Place dbEntity, Place entity)
    {
        dbEntity.Name = entity.Name;
        dbEntity.Abbreviation = entity.Abbreviation;
        dbEntity.Normalized = entity.Normalized;
        dbEntity.Latitude = entity.Latitude;
        dbEntity.Longitude = entity.Longitude;
        dbEntity.LatLongExact = entity.LatLongExact;
        dbEntity.MasterId = entity.MasterId;
        dbEntity.Note = entity.Note;
        dbEntity.Reverse = entity.Reverse;
        dbEntity.DateUpdated = DateTime.Now;

        _context.SaveChanges();
        return dbEntity;
    }

    public void Delete(Place entity)
    {
        _context.Places.Remove(entity);
        _context.SaveChanges();
    }
}