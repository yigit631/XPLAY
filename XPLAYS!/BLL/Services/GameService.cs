using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

    public interface IGameService
    {
        public IQueryable<GameModel> Query();

        public ServiceBase Create(Game GameInfo);

        public ServiceBase Update(Game GameInfo);

        public ServiceBase Delete(int id);
    }
    public class GameService : ServiceBase, IGameService
    {
        public GameService(dataContext db) : base(db)
        {
        }

        public ServiceBase Create(Game GameInfo)
        {
            if(_db.Games.Any(x => x.Name.ToUpper() == GameInfo.Name.ToUpper().Trim()))
                    return Error("Game with this name exists");
              GameInfo.Name = GameInfo.Name?.Trim();   
           _db.Games.Add(GameInfo);
            _db.SaveChanges();  
            return Success("Added successfully");

        }

        public ServiceBase Delete(int id)
        {
            // Verilen ID'ye sahip oyun kaydını bul
            var entity = _db.Games.SingleOrDefault(x => x.Id == id);

            // Kayıt bulunamazsa hata döndür
            if (entity == null)
                return Error("Game not found.");

            // Kayıt varsa sil ve değişiklikleri kaydet
            _db.Games.Remove(entity);
            _db.SaveChanges();

            return Success("Game deleted successfully.");
        }


        public IQueryable<GameModel> Query()
        {
            return _db.Games.OrderBy(x => x.Name).Select(x => new GameModel() { GameInfo = x});
        }

        public ServiceBase Update(Game GameInfo)
        {
            if (GameInfo == null)
                return Error("Invalid game GameInfo.");

            // Verilen ID'ye sahip oyun kaydını bul
            var entity = _db.Games.SingleOrDefault(x => x.Id == GameInfo.Id);

            // Kayıt bulunamazsa hata döndür
            if (entity == null)
                return Error("Game not found.");

            // Aynı isimde başka bir oyun var mı kontrol et
            if (_db.Games.Any(x => x.Id != GameInfo.Id && x.Name.ToUpper() == GameInfo.Name.ToUpper().Trim()))
                return Error("Another game with this name already exists.");

            // Güncellemeleri uygula
            entity.Name = GameInfo.Name?.Trim();
            entity.ReleaseDate = GameInfo.ReleaseDate; // Örnek olarak ek bir alan
            entity.Price = GameInfo.Price;
            entity.Publisher = GameInfo.Publisher; // Örnek: türü güncelle

            // Değişiklikleri kaydet
            _db.SaveChanges();

            return Success("Game updated successfully.");
        }

    }
}
