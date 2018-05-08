using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Years.IRepository;
using Years.Model;

namespace Years.Repository
{
    class GuestBookRepository : BaseRepository<GuestBook>, IGuestBookRepository
    {
    }
}
