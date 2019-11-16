using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class RegExr
    {
        public bool SoDienThoai(string s)
        {
            return Regex.IsMatch(s, @"(09|01[2|6|8|9])+([0-9]{8})\b");
        }

        public bool DiaChi(string s)
        {
            return Regex.IsMatch(s, @"^[-a-zA-Z_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶ" +
            "ẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợ" +
            "ụủứừỬỮỰỲỴÝỶỸửữựỳýỵỷỹ\\s0-9_.,/]+$");
        }

        public bool HoTen(string s)
        {
            return Regex.IsMatch(s, @"^[a-zA-Z_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶ" +
            "ẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợ" +
            "ụủứừỬỮỰỲỴÝỶỸửữựỳýỵỷỹ\\s]+$");
        }

        public bool SoCMND(string s)
        {
            return Regex.IsMatch(s, @"^[0-9]{9,12}$");
        }

        public bool SoCMNDKH(string s)
        {
            return Regex.IsMatch(s, @"(^[0-9]{10})\b");
        }
        public bool Type(string s)
        {
            return Regex.IsMatch(s, @"(Phim)|(Nhạc)\b");
        }

        public bool MaKhachHang(string s)
        {
            return Regex.IsMatch(s, @"^[0-9]+$");
        }

        //public bool Email(string s)
        //{
        //    return Regex.IsMatch(s, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        //}
        //public bool LetterAndNumber(string s)
        //{
        //    return Regex.IsMatch(s, @"^[a-zA-Z_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶ" +
        //    "ẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợ" +
        //    "ụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\\s0-9.,]+$");
        //}

        //public bool Taxcode(string s)
        //{
        //    return Regex.IsMatch(s, @"^[0-9]{10,13}$");
        //}

        //public bool Fax(string s)
        //{
        //    return Regex.IsMatch(s, @"^(\+?\d{1,}(\s?|\-?)\d*(\s?|\-?)\(?\d{2,}\)?(\s?|\-?)\d{3,}\s?\d{3,})$");
        //}
    }
}
