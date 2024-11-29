using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebsiteBanDienThoai23.Common.DAL;
using WebsiteBanDienThoai23.DAL.Models;

namespace WebsiteBanDienThoai23.DAL
{
    public class BaoHanhRep_Admin : GenericRep<QLBanDienThoaiContext, BaoHanh>
    {
        public override BaoHanh Read(int id)
        {
            var res = All.FirstOrDefault(l => l.MaBh == id);
            return res;
        }
        public List<BaoHanh> GetAllGuarantee()
        {
            return All.ToList();
        }
        public void CreateGuarantee(BaoHanh Gua)
        {
            Context.BaoHanhs.Add(Gua);
            Context.SaveChanges();
        }
        public void UpdateGuarantee(BaoHanh Gua)
        {
            Context.BaoHanhs.Update(Gua);
            Context.SaveChanges();
        }
        public void Remove(BaoHanh Gua)
        {
            Context.BaoHanhs.Remove(Gua);
            Context.SaveChanges();
        }

        public static implicit operator BaoHanhRep_Admin(LoaiSpRep_Admin v)
        {
            throw new NotImplementedException();
        }
    }
}
