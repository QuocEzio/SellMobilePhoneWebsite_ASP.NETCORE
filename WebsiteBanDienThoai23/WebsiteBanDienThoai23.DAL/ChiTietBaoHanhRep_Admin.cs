using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebsiteBanDienThoai23.Common.DAL;
using WebsiteBanDienThoai23.DAL.Models;

namespace WebsiteBanDienThoai23.DAL
{
    public class ChiTietBaoHanhRep_Admin : GenericRep<QLBanDienThoaiContext, ChiTietBaoHanh>
    {
        public override ChiTietBaoHanh Read(int id)
        {
            var res = All.FirstOrDefault(l => l.MaBh == id);
            return res;
        }
        public List<ChiTietBaoHanh> GetAllGuarantee()
        {
            return All.ToList();
        }
        public void CreateGuarantee(ChiTietBaoHanh Gua)
        {
            Context.ChiTietBaoHanhs.Add(Gua);
            Context.SaveChanges();
        }
        public void UpdateGuarantee(ChiTietBaoHanh Gua)
        {
            Context.ChiTietBaoHanhs.Update(Gua);
            Context.SaveChanges();
        }
        public void Remove(ChiTietBaoHanh Gua)
        {
            Context.ChiTietBaoHanhs.Remove(Gua);
            Context.SaveChanges();
        }

        public static implicit operator ChiTietBaoHanhRep_Admin(LoaiSpRep_Admin v)
        {
            throw new NotImplementedException();
        }
    }
}
