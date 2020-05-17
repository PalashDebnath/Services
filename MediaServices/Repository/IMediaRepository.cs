using MediaServices.Common;
using MediaServices.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaServices.Repository
{
    public interface IMediaRepository
    {
        IEnumerable<Response> Get(int pageNumber, int pageSize);
        Response Get(int id);
        Task MapShow();
        Task MapPerson(int showId);
        Task MapShowPerson(int showId, int personId);
    }
}
