using MediaServices.Common;
using MediaServices.Data;
using MediaServices.Helpers;
using MediaServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaServices.Repository
{
    public class SqlServerRepository : IMediaRepository
    {
        private readonly MediaDbContext _db;

        public SqlServerRepository(MediaDbContext mediaDbContext)
        {
            _db = mediaDbContext;
        }

        public IEnumerable<Response> Get(int pageNumber, int pageSize)
        {
            //No memory footprint as we know the response size in advance and allocating that much space via pagesize
            var response = new List<Response>(pageSize);
            var shows = _db.Shows.Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();

            foreach (var show in shows)
            {
                var persons = _db.Persons.Where(p => p.ShowPersons.Any(sp => sp.ShowId == show.Id))
                                         .OrderByDescending(p => p.Birthday)
                                         .ToList();
                response.Add(new Response() { ShowId = show.Id, ShowName = show.Name, Persons = persons });
            }

            return response;
        }

        public Response Get(int id)
        {
            
            var show = _db.Shows.Where(s => s.Id == id)
                                .FirstOrDefault();

            if (show == null)
                return null;

            var persons = _db.Persons.Where(p => p.ShowPersons.Any(sp => sp.ShowId == show.Id))
                                         .OrderByDescending(p => p.Birthday)
                                         .ToList();

            var response = new Response();
            response.ShowId = show.Id;
            response.ShowName = show.Name;
            response.Persons = persons;

            return response;
        }

        public async Task MapShow()
        {
            try
            {

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
            var shows = await MazeHelper.GetShows();
            using (var db = new MediaDbContext())
            {
                foreach (var show in shows)
                {
                    if (db.Shows.Find(show.Id) == null)
                    {
                        await db.AddAsync(show);
                        await db.SaveChangesAsync();
                    }
                    await MapPerson(show.Id);
                }
            }
        }

        public async Task MapPerson(int showId)
        {
            try
            {
                var casts = await MazeHelper.GetCasts(showId);
                using (var db = new MediaDbContext())
                {
                    foreach (var cast in casts)
                    {
                        if (await db.Persons.FindAsync(cast.Person.Id) == null)
                        {
                            await db.AddAsync(cast.Person);
                            await db.SaveChangesAsync();
                        }
                        await MapShowPerson(showId, cast.Person.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;                
            }            
        }

        public async Task MapShowPerson(int showId, int personId)
        {
            try
            {
                using (var db = new MediaDbContext())
                {
                    if (await db.ShowPersons.FindAsync(showId, personId) == null)
                    {
                        await db.AddAsync(new ShowPerson() { ShowId = showId, PersonId = personId });
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
