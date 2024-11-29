using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebsiteBanDienThoai23.Common.DAL;
using WebsiteBanDienThoai23.Common.Rsp;
using WebsiteBanDienThoai23.DAL.Models;

namespace WebsiteBanDienThoai23.DAL
{
	public class SanPhamRep_Admin:GenericRep<QLBanDienThoaiContext, SanPham>
	{


        public override SanPham Read(string key)
        {
            var res = All.FirstOrDefault(p => p.MaSp == key);
            return res;
        }
        public SanPham GetSanPhamByMaSp(string maSp)
        {
            SanPham sanPham = null;
            var conn = (SqlConnection)Context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "sp_GetSanPhamByMaSp";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MaSp", maSp));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sanPham = new SanPham
                            {
                                MaSp = reader["MaSp"].ToString(),
                                TenSp = reader["TenSp"].ToString(),
                                Hinh = reader["Hinh1"].ToString(),
                                
                                MoTa = reader["MoTa"].ToString(),
                                TonKho = (bool)reader["TonKho"],
                                SoLuong = (short)reader["SoLuong"],
                                Gia = (decimal)reader["Gia"],
                                ThoiGianBh = (byte)reader["ThoiGianBh"],
                                _5g = (int)reader["_5g"],
                                Cpu = reader["Cpu"].ToString(),
                                Pin = (short)reader["Pin"],
                                SacNhanh = (short)reader["SacNhanh"],
                                Ram = (short)reader["Ram"],
                                Rom = (short)reader["Rom"],
                                ManHinh = (double)reader["ManHinh"],
                                Fps = (short)reader["Fps"],
                                HeDieuHanh = reader["HeDieuHanh"].ToString(),
                                CameraSau = (short)reader["CameraSau"],
                                CameraTruoc = (short)reader["CameraTruoc"],
                                GiamGia = (decimal)reader["GiamGia"],
                                MaLoai = reader["MaLoai"].ToString()
                            };
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return sanPham;
        }

        public void Remove(SanPham product)
        {
            Context.SanPhams.Remove(product);
            Context.SaveChanges();
        }

        public void CreateProduct(SanPham product)
        {
            Context.SanPhams.Add(product);
            Context.SaveChanges();
        }

        public void UpdateProduct(SanPham product)
        {
            Context.SanPhams.Update(product);
            Context.SaveChanges();
        }

        public bool IsMaSpExists(string maSp)
        {
            return Context.SanPhams.Any(p => p.MaSp == maSp);
        }

        public bool IsMaLoaiExists(string maLoai)
        {
            return Context.LoaiSps.Any(l => l.MaLoai == maLoai);
        }

        public List<SanPham> PhanTrangSanPham(int pageIndex, int pageSize)
        {
            
            int offset = (pageIndex - 1) * pageSize;

            
            return All.Skip(offset).Take(pageSize).ToList();
        }

        public List<SanPham> GetAllProducts()
        {
            return All.ToList();
        }
    }
}
