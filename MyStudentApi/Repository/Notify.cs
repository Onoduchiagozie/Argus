using Microsoft.EntityFrameworkCore;
using MyStudentApi.Repository.IRepo;
using MyStudentApi.Data;
using MyStudentApi.Models;

namespace MyStudentApi.Repository
{
    public class Notify : Inotify
    {
        public IEMailServices _eMailServices;
        public TendancyDbContext _context;

        public Notify(IEMailServices eMailServices, TendancyDbContext context)
        {
            _eMailServices = eMailServices;
            _context = context;
        }
        bool Inotify.Notify(SchoolClass schoolClass)
        {
            return true;
        }
    }
}
