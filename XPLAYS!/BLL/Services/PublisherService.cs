using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BLL.Services
{
    public interface IPublisherService
    {
        IQueryable<PublisherModel> Query();
        ServiceBase Create(Publisher GameInfo);
        ServiceBase Update(Publisher GameInfo);
        ServiceBase Delete(int id);
    }

    public class PublisherService : ServiceBase, IPublisherService
    {
        public PublisherService(dataContext db) : base(db)
        {
        }

        public ServiceBase Create(Publisher GameInfo)
        {
            if (_db.Publishers.Any(x => x.Name.ToUpper() == GameInfo.Name.ToUpper().Trim()))
                return Error("Publisher with this name exists");
            GameInfo.Name = GameInfo.Name?.Trim();
            _db.Publishers.Add(GameInfo);
            _db.SaveChanges();
            return Success("Added successfully");
        }

        public ServiceBase Delete(int id)
        {
            var publisher = _db.Publishers.Find(id);

            if (publisher == null)
                return Error("Publisher not found");

            _db.Publishers.Remove(publisher);
            _db.SaveChanges();
            return Success("Publisher deleted successfully");
        }


        public IQueryable<PublisherModel> Query()
        {
            return _db.Publishers.OrderBy(x => x.Name).Select(x => new PublisherModel() { GameInfo = x });
        }

        public ServiceBase Update(Publisher GameInfo)
        {
            if (GameInfo == null)
                return Error("Invalid publisher GameInfo.");

            var existingPublisher = _db.Publishers.FirstOrDefault(x => x.Id == GameInfo.Id);

            if (existingPublisher == null)
                return Error("Publisher not found.");

            if (_db.Publishers.Any(x => x.Id != GameInfo.Id && x.Name.ToUpper() == GameInfo.Name.ToUpper().Trim()))
                return Error("Another publisher with this name already exists.");

            existingPublisher.Name = GameInfo.Name?.Trim();
            _db.Entry(existingPublisher).State = EntityState.Modified;
            _db.SaveChanges();

            return Success("Updated successfully.");
        }

    }

}