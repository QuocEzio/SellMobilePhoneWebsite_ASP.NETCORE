using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteBanDienThoai23.Common.BLL;
using WebsiteBanDienThoai23.Common.Rsp;
using WebsiteBanDienThoai23.Common.Req;
using WebsiteBanDienThoai23.DAL.Models;
using WebsiteBanDienThoai23.DAL;

namespace WebsiteBanDienThoai23.BLL
{
    public class SanPhamSvc_Admin : GenericSvc<SanPhamRep_Admin, SanPham>
    {
        private SanPhamRep_Admin sanPhamRep_Admin;

        public SanPhamSvc_Admin()
        {
            sanPhamRep_Admin = new SanPhamRep_Admin();
        }

        public override SingleRsp Read(string maSp)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(maSp);
            return res;
        }


        public SingleRsp CreateProduct(SanPhamReq productReq)
        {
            var res = new SingleRsp();
            var product = new SanPham
            {
                MaSp = productReq.MaSp,
                TenSp = productReq.TenSp,
                Hinh = productReq.Hinh,
                MoTa = productReq.MoTa,
                TonKho = productReq.TonKho,
                SoLuong = productReq.SoLuong,
                Gia = productReq.Gia,
                ThoiGianBh = productReq.ThoiGianBh,
                _5g = productReq._5g,
                Cpu = productReq.Cpu,
                Pin = productReq.Pin,
                SacNhanh = productReq.SacNhanh,
                Ram = productReq.Ram,
                Rom = productReq.Rom,
                ManHinh = productReq.ManHinh,
                Fps = productReq.Fps,
                HeDieuHanh = productReq.HeDieuHanh,
                CameraSau = productReq.CameraSau,
                CameraTruoc = productReq.CameraTruoc,
                GiamGia = productReq.GiamGia,
                MaLoai = productReq.MaLoai,
                MayCu = productReq.MayCu

        };

            if (!_rep.IsMaLoaiExists(product.MaLoai))
            {
                res.SetError("Mã loại sản phẩm không tồn tại.");
                return res;
            }

            if (_rep.IsMaSpExists(product.MaSp))
            {
                res.SetError("Mã sản phẩm đã tồn tại.");
                return res;
            }

            try
            {
                _rep.CreateProduct(product);
                res.Data = product;
                res.SetMessage("Đã thêm thành công sản phẩm!");
            }
            catch (Exception ex)
            {
                res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
            }

            return res;
        }

        public SingleRsp PhanTrangSanPham(int pageIndex, int pageSize)
        {
            var res = new SingleRsp();

            // Gọi phương thức PhanTrangSanPham từ DAL để lấy danh sách sản phẩm phân trang
            var products = _rep.PhanTrangSanPham(pageIndex, pageSize);

            // Chuẩn bị dữ liệu trả về
            var p = new
            {
                Data = products,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            // Gán dữ liệu vào đối tượng SingleRsp
            res.Data = p;

            return res;
        }


        public SingleRsp GetAllProducts()
        {
            var res = new SingleRsp();
            var products = _rep.GetAllProducts();
            res.Data = products;
            return res;
        }

        public SingleRsp RemoveProduct(string maSp)
        {
            var res = new SingleRsp();
            var product = _rep.Read(maSp);
            if (product == null)
            {
                res.SetError("Không tìm thấy mã sản phẩm cần xóa");
            }
            else
            {
                try
                {
                    _rep.Remove(product);
                    res.SetMessage("Xóa sản phẩm thành công.");
                }
                catch (Exception ex)
                {
                    res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                }
            }
            return res;
        }

        public SingleRsp UpdateProduct(SanPhamReq productReq)
        {
            var res = new SingleRsp();
            var product = _rep.Read(productReq.MaSp);
            if (product == null)
            {
                res.SetError("EZ106", "Không tìm thấy dữ liệu cho mã sản phẩm được chỉ định.");
            }
            else
            {
                product.TenSp = productReq.TenSp;
                product.Hinh = productReq.Hinh;
                
                product.MoTa = productReq.MoTa;
                product.TonKho = productReq.TonKho;
                product.SoLuong = productReq.SoLuong;
                product.Gia = productReq.Gia;
                product.ThoiGianBh = productReq.ThoiGianBh;
                product._5g = productReq._5g;
                product.Cpu = productReq.Cpu;
                product.Pin = productReq.Pin;
                product.SacNhanh = productReq.SacNhanh;
                product.Ram = productReq.Ram;
                product.Rom = productReq.Rom;
                product.ManHinh = productReq.ManHinh;
                product.Fps = productReq.Fps;
                product.HeDieuHanh = productReq.HeDieuHanh;
                product.CameraSau = productReq.CameraSau;
                product.CameraTruoc = productReq.CameraTruoc;
                product.GiamGia = productReq.GiamGia;
                product.MaLoai = productReq.MaLoai;
                product.MayCu = productReq.MayCu;
                if (!sanPhamRep_Admin.IsMaLoaiExists(productReq.MaLoai))
                {
                    res.SetError("Mã loại sản phẩm không tồn tại.");
                    return res;
                }

                try
                {
                    _rep.UpdateProduct(product);
                    res.SetMessage("Cập nhật sản phẩm thành công");
                }
                catch (Exception ex)
                {
                    res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                }
            }
            return res;
        }
        public SingleRsp GetSanPhamByMaSp(string maSp)
        {
            var res = new SingleRsp();
            var sanPham = sanPhamRep_Admin.GetSanPhamByMaSp(maSp);

            if (sanPham == null)
            {
                res.SetError("Không tìm thấy sản phẩm với mã sản phẩm được chỉ định.");
            }
            else
            {
                res.Data = sanPham;
                res.SetMessage("Tìm thấy sản phẩm!");
                
            }
            return res;


        }
    }
}

