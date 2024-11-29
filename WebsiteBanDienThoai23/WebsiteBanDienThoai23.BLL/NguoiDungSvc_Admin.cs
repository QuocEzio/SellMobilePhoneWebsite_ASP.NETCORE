using System;
using System.Collections.Generic;
using System.Text;
using WebsiteBanDienThoai23.Common.BLL;
using WebsiteBanDienThoai23.Common.Req;
using WebsiteBanDienThoai23.Common.Rsp;
using WebsiteBanDienThoai23.DAL;
using WebsiteBanDienThoai23.DAL.Models;

namespace WebsiteBanDienThoai23.BLL
{
    public class NguoiDungSvc_Admin : GenericSvc<NguoiDungRep_Admin, NguoiDung>
    {
        public SingleRsp CreateUser(NguoiDungReq userReq)
        {
            var res = new SingleRsp();
            var user = new NguoiDung
            {
                UserId = userReq.UserId,
                HoTen = userReq.HoTen,
                Email = userReq.Email,
                DiaChi = userReq.DiaChi,
                Sdt = userReq.Sdt,
                TenTaiKhoan = userReq.TenTaiKhoan,
                MatKhau = userReq.MatKhau,
                IsAdmin = userReq.IsAdmin
            };


            try
            {
                _rep.CreateUser(user);
                res.Data = user;
                res.SetMessage("Đã thêm thành công người dùng!");
            }
            catch (Exception ex)
            {
                res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
            }

            return res;
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            var user = _rep.Read(id);
            if (user == null)
            {
                res.SetError("Không tìm thấy mã người dùng");
            }
            else
            {
                res.Data = user;
            }    
            
            return res;
        }

        public SingleRsp RemoveUser(int id)
        {
            var res = new SingleRsp();
            var user = _rep.Read(id);
            if (user == null)
            {
                res.SetError("Không tìm thấy mã người dùng cần xóa");
            }
            else
            {
                try
                {
                    _rep.Remove(user);
                    res.SetMessage("Xóa người dùng thành công.");
                }
                catch (Exception ex)
                {
                    res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                }
            }
            return res;
        }

        public SingleRsp UpdateUser(NguoiDungReq userReq)
        {
            var res = new SingleRsp();
            var user = _rep.Read(userReq.UserId);
            if (user == null)
            {
                res.SetError("Không tìm thấy dữ liệu cho mã người dùng được chỉ định.");
            }
            else
            {
                user.UserId = userReq.UserId;
                user.HoTen = userReq.HoTen;
                user.Email = userReq.Email;
                user.DiaChi = userReq.DiaChi;
                user.Sdt = userReq.Sdt;
                user.TenTaiKhoan = userReq.TenTaiKhoan;
                user.MatKhau = userReq.MatKhau;
                user.IsAdmin = userReq.IsAdmin;

                try
                {
                    _rep.UpdateUser(user);
                    res.SetMessage("Cập nhật người dùng thành công");
                }
                catch (Exception ex)
                {
                    res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                }
            }
            return res;
        }

        public SingleRsp GetAllUsers()
        {
            var res = new SingleRsp();
            var users = _rep.GetAllUsers();
            res.Data = users;
            return res;
        }

    }
}
