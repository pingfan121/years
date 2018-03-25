using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Years.IRepository;
using Years.Model;
using Years.Repository;

namespace Years.Repository
{
    class UserInfoRepository : BaseRepository<UserInfo>,IUserInfoRepository
    {

    }
}
