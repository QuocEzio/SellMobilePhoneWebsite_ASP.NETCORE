using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebsiteBanDienThoai23.Common.DAL;
using WebsiteBanDienThoai23.Common.Req;
using WebsiteBanDienThoai23.DAL.Models;

namespace WebsiteBanDienThoai23.DAL
{
	public class LoaiSpRep_Admin:GenericRep<QLBanDienThoaiContext,LoaiSp>
	{
		
		public override LoaiSp Read(string key)
		{
			var res = All.FirstOrDefault(l => l.MaLoai == key);
			return res;
		}
        public List<LoaiSp> GetAllCategory()
        {
            return All.ToList();
        }
        public void CreateCategory(LoaiSp cate)
        {
            Context.LoaiSps.Add(cate);
            Context.SaveChanges();
        }

        public void UpdateCategory(LoaiSp cate)
        {
            Context.LoaiSps.Update(cate);
            Context.SaveChanges();
        }
        public void Remove(LoaiSp cate)
        {
            Context.LoaiSps.Remove(cate);
            Context.SaveChanges();
        }
		public List<ThongKeReq> ThongKeMayMoi()
		{
			List<ThongKeReq> tk = new List<ThongKeReq>();
			List<LoaiSp> lsp = Context.LoaiSps.ToList();
			foreach (LoaiSp sp in lsp)
			{
				ThongKeReq a = new ThongKeReq();
				a.MaLoai = sp.MaLoai;
				a.TenLoai = sp.TenLoai;
				a.TonKho = Context.SanPhams.Where(s => s.MayCu == false & s.MaLoai == sp.MaLoai).Sum(sl => sl.SoLuong);
				tk.Add(a);
			}
			return tk;
		}
		public List<ThongKeReq> ThongKeMayCu()
		{
			List<ThongKeReq> tk = new List<ThongKeReq>();
			List<LoaiSp> lsp = Context.LoaiSps.ToList();
			foreach (LoaiSp sp in lsp)
			{
				ThongKeReq a = new ThongKeReq();
				a.MaLoai = sp.MaLoai;
				a.TenLoai = sp.TenLoai;
				a.TonKho = Context.SanPhams.Where(s => s.MayCu == true & s.MaLoai == sp.MaLoai).Sum(sl => sl.SoLuong);
				tk.Add(a);
			}
			return tk;
		}
	}
}
