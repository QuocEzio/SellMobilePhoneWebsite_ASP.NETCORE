using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebsiteBanDienThoai23.Common.DAL;
using WebsiteBanDienThoai23.DAL.Models;

namespace WebsiteBanDienThoai23.DAL
{
    public class NguoiDungRep_Admin : GenericRep<QLBanDienThoaiContext, NguoiDung>
    {
        public override NguoiDung Read(int id)
        {
            var res = All.FirstOrDefault(p => p.UserId == id);
            return res;
        }
        public void CreateUser(NguoiDung user)
        {
            Context.NguoiDungs.Add(user);
            Context.SaveChanges();
        }

        public void Remove(NguoiDung user)
        {
            Context.NguoiDungs.Remove(user);
            Context.SaveChanges();
        }
        public void UpdateUser(NguoiDung user)
        {
            Context.NguoiDungs.Update(user);
            Context.SaveChanges();
        }

        public List<NguoiDung> GetAllUsers()
        {
            return All.ToList();
        }
    }
}
